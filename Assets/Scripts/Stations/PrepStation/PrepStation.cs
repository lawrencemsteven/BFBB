using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Orders;
using TMPro;

public class PrepStation : Station
{
    [SerializeField] private GameObject prepStationUI;
    [SerializeField] private GameObject orderPrefab;
    [SerializeField] private GameObject platePrefab;
    private Transform orderDisplay;
    private Dictionary<Order, CustomerBehavior> orders = new Dictionary<Order, CustomerBehavior>();
    private Order preppedOrder;
    private Order selectedOrder;
    private TextMeshProUGUI selectedToppingLabel;
    private TextMeshProUGUI requiredToppingLabel;

    [SerializeField] private GameObject fruitTongs;
    [SerializeField] private GameObject syrupContainer;
    [SerializeField] private GameObject whipCream;
    [SerializeField] private GameObject chocolateChip;

    private Vector3 initialFruitPosition;
    private Vector3 initialSyrupPosition;
    private Vector3 initialWhipPosition;
    private Vector3 initialChocoPosition;

    private Vector3 containerPos;

    private ToppingCoordinateGenerator toppingCoordinateGenerator;
    private bool evenMeasure = false;
    private bool evenBeat = false;
    private int currentBeat = 0;
    private Topping selectedTopping = Topping.NONE;
    private Topping requiredTopping;
    private CustomerBehavior selectedCustomer;
    private bool makingOrder = false;
    private int orderProgress = 0;
    private ScoreAndStreakManager scoreManager;
    private int pathToScore;

    [SerializeField] private List<Sprite> toppingIcons = new List<Sprite>();

    

    //butter is hold button down and click to drop
    //all other toppings are hold button down and click and hold
    //requirements for how long pour lasts are time based around beat? not following pattern

    public void Start()
    {
        scoreManager = GetComponent<ScoreAndStreakManager>();
        orderDisplay = prepStationUI.transform.GetChild(0);
        prepStationUI.SetActive(false);
        preppedOrder = null;

        initialFruitPosition = fruitTongs.transform.position;
        initialSyrupPosition = syrupContainer.transform.position;
        initialWhipPosition = whipCream.transform.position;
        initialChocoPosition = chocolateChip.transform.position;

        toppingCoordinateGenerator = coordinateGenerator as ToppingCoordinateGenerator;

        Composer.Instance.onBeat.AddListener(NewBeat);
        Composer.Instance.onMeasure.AddListener(NewMeasure);
    }

    public void Update()
    {
        prepStationUI.SetActive(running);

        if (running == false)
        {
            return;
        }

        containerPos = Input.mousePosition;

        if (!makingOrder && requiredTopping != Topping.NONE)
        {
            requiredTopping = Topping.NONE;
        }

        prepStationUI.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = toppingIcons[(int)requiredTopping];

        ToppingsControl();
        ToppingsRelease();

        if (pathToScore == 100) {
            pathToScore = 0;
            scoreManager.scoreUpdate(1);
        }
    }

    public void NewBeat()
    {
        if (evenBeat)
        {
            currentBeat++;
            if (currentBeat > 3)
            {
                currentBeat = 0;
            }
            if (selectedOrder is not null && makingOrder)
            {
                requiredTopping = selectedOrder.GetToppings()[currentBeat]; 
                prepStationUI.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = toppingIcons[(int)requiredTopping];
            }
            evenBeat = false;
        }
        else
        {
            evenBeat = true;
        }
    }

    public void NewMeasure()
    {
        if (!makingOrder && IsOrderSelected())
        {
            makingOrder = true;
            evenMeasure = true;
            orderProgress = 0;
            ReservoirManager.GetPlates().Pop();
            Transform spawnedPlate = Instantiate(platePrefab, lineManager.transform).transform;
            spawnedPlate.localPosition = new Vector3(0.014f, -0.35f, 0.01f);
        }

        if (makingOrder)
        {
            if (evenMeasure)
            {
                if (orderProgress < selectedOrder.GetMainCourseCount())
                {
                    toppingCoordinateGenerator.NewPlate();
                    lineManager.DrawLine();
                    evenMeasure = false;
                    currentBeat = 0;

                    float height = (selectedOrder.GetMainCourseCount() - orderProgress) * -0.01f;
                    ReservoirPancake pancake = ReservoirManager.GetPancakes().Pop();
                    Transform spawnedPancake = Instantiate(pancake.GetPancake(), lineManager.transform).transform;
                    spawnedPancake.localPosition = new Vector3(-0.2f, height, 0.15f);
                    spawnedPancake.gameObject.SetActive(true);

                    orderProgress++;
                    
                    requiredTopping = selectedOrder.GetToppings()[currentBeat]; 
                }
                else
                {
                    selectedCustomer.DeactivateCustomer();
                    selectedCustomer = null;
                    selectedOrder = null;
                    makingOrder = false;

                    foreach (Transform child in lineManager.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    UpdateCustomerOrders();
                }
            }
            else
            {
                evenMeasure = true;
            }
        }
    }

    public override void pathUpdate(Vector2 offset)
    {
        float distance = offset.magnitude;

        if (!makingOrder || ((distance < distanceMinimum || distance > 0.25F) && selectedTopping == requiredTopping))
        {
            Composer.Instance.PitchChange(0);
            pathToScore += 1;
        }
        else
        {
            Composer.Instance.PitchChange(-1);
            scoreManager.resetStreak();
        }
    }

    public void AddOrder(Order order, CustomerBehavior customer)
    {
        orders.Add(order, customer);
    }

    public void RemoveOrder(Order order)
    {
        if (order is null)
        {
            return;
        }

        if (selectedOrder == order)
        {
            selectedCustomer = null;
            selectedOrder = null;
            makingOrder = false;

            toppingCoordinateGenerator.RemoveShape();
            lineManager.UpdateLine();

            foreach (Transform child in lineManager.transform)
            {
                Destroy(child.gameObject);
            }

            UpdateCustomerOrders();
        }

        orders.Remove(order);
    }

    public void UpdateCustomerOrders()
    {
        foreach (Transform child in orderDisplay)
        {
            Destroy(child.gameObject);
        }

        int i = 0;

        foreach (KeyValuePair<Order, CustomerBehavior> kvp in orders)
        {
            GameObject newOrder = Instantiate(orderPrefab, orderDisplay);
            OrderSlip newOrderSlip = newOrder.GetComponentInChildren<OrderSlip>();
            newOrderSlip.SetAssociatedOrder(kvp.Key);
            newOrderSlip.SetAssociatedCustomer(kvp.Value);
            newOrderSlip.SetKeyCodeByIndex(i);
            i++;
        }
    }

    public void SelectMainCourse(MainCourse mainCourse)
    {
        if (mainCourse == MainCourse.PANCAKE && ReservoirManager.GetPancakes().Count() <= 0 ||
            mainCourse == MainCourse.WAFFLE && ReservoirManager.GetWaffles().Count() <= 0)
        {
            return;
        }
        if (preppedOrder is null)
        {
            preppedOrder = new Order(mainCourse, 1, new List<Topping>());
        }
        else if (preppedOrder.GetMainCourse() != mainCourse)
        {
            preppedOrder.SetMainCourse(mainCourse);
        }
        else
        {
            if (mainCourse == MainCourse.PANCAKE && ReservoirManager.GetPancakes().Count() >= preppedOrder.GetMainCourseCount() + 1 ||
            mainCourse == MainCourse.WAFFLE && ReservoirManager.GetWaffles().Count() >= preppedOrder.GetMainCourseCount() + 1)
            {
                preppedOrder.AddMainCourseAmount(1);
                
            }
        }
    }

    public void ToggleTopping(Topping topping)
    {
        if (preppedOrder is null)
        {
            return;
        }

        if (preppedOrder.GetToppings().Contains(topping))
        {
            return;
        }

        else
        {
            preppedOrder.AddTopping(topping);
        }
    }

    public void SetRunning(bool running) { this.running = running; }

    public void ToppingsControl()
    {
        //foot
        if (Input.GetKey(KeyCode.W))
        {
            if (selectedTopping == Topping.NONE || selectedTopping == Topping.FRUIT)
            {
                fruitTongs.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 1f));

                if (Input.GetMouseButton(0))
                {
                    ToggleTopping(Topping.FRUIT);
                    //activate pour effect   
                }
            }
            selectedTopping = Topping.FRUIT;
        }

        //Syrup
        if (Input.GetKey(KeyCode.A))
        {
            if (selectedTopping == Topping.NONE || selectedTopping == Topping.SYRUP)
            {
                syrupContainer.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 1f));

                if (Input.GetMouseButton(0))
                {
                    ToggleTopping(Topping.SYRUP);
                    //activate pour effect   
                }
            }
            selectedTopping = Topping.SYRUP;
        }

        //Choccy Chippos
        if (Input.GetKey(KeyCode.S))
        {
            if (selectedTopping == Topping.NONE || selectedTopping == Topping.CHOCOLATE_CHIP)
            {
                chocolateChip.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 1f));

                //set cursor and choccies to follow mouse movement
                if (Input.GetMouseButton(0))
                {
                    ToggleTopping(Topping.CHOCOLATE_CHIP);
                }
            }
            selectedTopping = Topping.CHOCOLATE_CHIP;
        }

        //Whip
        if (Input.GetKey(KeyCode.D))
        {
            if (selectedTopping == Topping.NONE || selectedTopping == Topping.WHIPPED_CREAM)
            {
                whipCream.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 1f));

                //cursor and strawbs should follow mouse
                if (Input.GetMouseButton(0))
                {
                    ToggleTopping(Topping.WHIPPED_CREAM);
                }
            }
            selectedTopping = Topping.WHIPPED_CREAM;
        }

        //FUCK you i dont care anymore
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            selectedTopping = Topping.NONE;
        }
    }

    public void ToppingsRelease()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            fruitTongs.transform.position = initialFruitPosition;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            syrupContainer.transform.position = initialSyrupPosition;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            chocolateChip.transform.position = initialChocoPosition;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            whipCream.transform.position = initialWhipPosition;
        }
        
    }

    public void ScrapOrder()
    {
        preppedOrder.ClearOrder();
    }

    public bool IsOrderSelected() { return selectedOrder != null; }
    public void SetSelectedOrderAndCustomer(Order selectedOrder, CustomerBehavior selectedCustomer)
    {
        this.selectedOrder = selectedOrder;
        this.selectedCustomer = selectedCustomer;
    }

}
