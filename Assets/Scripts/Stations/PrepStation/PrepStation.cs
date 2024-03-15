using UnityEngine;
using Orders;
using TMPro;
using System.Collections.Generic;

public class PrepStation : Station
{
    [SerializeField] private GameObject prepStationUI;
    [SerializeField] private GameObject orderPrefab;
    private Transform orderList;
    private Order preppedOrder;
    private TextMeshProUGUI selectedToppingLabel;
    private TextMeshProUGUI requiredToppingLabel;

    private bool setPancake;
    private bool setWaffle;

    [SerializeField] private GameObject syrupContainer;
    //[SerializeField] private GameObject butter;
    [SerializeField] private GameObject whipCream;
    [SerializeField] private GameObject chocolateChip;

    private Vector3 initialSyrupPosition;
    private Vector3 initialButterPosition;
    private Vector3 initialWhipPosition;
    private Vector3 initialChocoPosition;

    private Vector3 containerPos;

    private ToppingCoordinateGenerator toppingCoordinateGenerator;
    private bool even = false;
    private Topping selectedTopping = Topping.NONE;

    
    //protected bool running = false;

    //butter is hold button down and click to drop
    //all other toppings are hold button down and click and hold
    //requirements for how long pour lasts are time based around beat? not following pattern


    public void Start()
    {
        orderList = prepStationUI.transform.GetChild(0);
        selectedToppingLabel = prepStationUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        requiredToppingLabel = prepStationUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        prepStationUI.SetActive(false);
        preppedOrder = null;

        initialSyrupPosition = syrupContainer.transform.position;
        //initialButterPosition = syrupContainer.transform.position;
        initialWhipPosition = whipCream.transform.position;
        initialChocoPosition = chocolateChip.transform.position;

        toppingCoordinateGenerator = coordinateGenerator as ToppingCoordinateGenerator;

        Composer.Instance.onMeasure.AddListener(NewMeasure);

        //Test code
        ReservoirManager.GetPancakes().Add(new ReservoirPancake(1, new GameObject()));
        ReservoirManager.GetPancakes().Add(new ReservoirPancake(1, new GameObject()));
        ReservoirManager.GetWaffles().Add(new ReservoirWaffle(1));
    }

    public void Update()
    {
        //if(running == false) {return;}

        containerPos = Input.mousePosition;

        prepStationUI.SetActive(running);
        if (running)
        {
            requiredToppingLabel.text = lineManager.GetCurrentTopping().ToString();
            selectedToppingLabel.text = selectedTopping.ToString();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(setPancake == true){ AddPancake(); }
            
            else if(setWaffle == true){ AddWaffle(); }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScrapOrder();
        }

        ToppingsControl();
        ToppingsRelease();

    }

    public void NewMeasure()
    {
        if (even)
        {
            toppingCoordinateGenerator.NewPlate();
            lineManager.DrawLine();
            even = false;
        }
        else
        {
            even = true;
        }

        selectedTopping = Topping.NONE;
    }

    public override void pathUpdate(Vector2 offset)
    {
        float distance = offset.magnitude;

        if ((distance < distanceMinimum || distance > 0.25F) && selectedTopping == lineManager.GetCurrentTopping())
        {
            Composer.Instance.PitchChange(0);
        }
        else
        {
            Composer.Instance.PitchChange(-1);
        }
    }

    public void UpdateCustomerOrders()
    {
        foreach (Transform child in orderList)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<Order, CustomerBehavior> kvp in CustomerManager.Instance.GetAllActiveOrders())
        {
            GameObject newOrder = Instantiate(orderPrefab, orderList);
            OrderButton newOrderButton = newOrder.GetComponentInChildren<OrderButton>();
            newOrderButton.SetAssociatedOrder(kvp.Key);
            newOrderButton.SetAssociatedCustomer(kvp.Value);
            newOrder.GetComponentInChildren<TextMeshProUGUI>().text = kvp.Key.ToString();
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

        /*else if(String.Equals(topping.ToString(), "BUTTER"))
        {
        if butter == 3 return, else add butter

        }*/

        if (preppedOrder.GetToppings().Contains(topping))
        {
            return;
        }

        else
        {
            preppedOrder.AddTopping(topping);
        }
    }

    public void AddPancake() { SelectMainCourse(MainCourse.PANCAKE); }
    public void AddWaffle() { SelectMainCourse(MainCourse.WAFFLE); }
    public void ToggleChocolateChips() {ToggleTopping(Topping.CHOCOLATE_CHIP);}


    public void SelectPancake()
    {
        setWaffle = false;
        setPancake = true;
    }

    public void SelectWaffle()
    {
        setPancake = false;
        setWaffle = true;
    }

    public void NullifyPreppedOrder()
    {
        preppedOrder = null;
        selectedToppingLabel.text = "";
    }
    public Order GetPreppedOrder() { return preppedOrder; }
    public void SetRunning(bool running) { this.running = running; }

    public void ToppingsControl()
    {
        //Butter
        if (Input.GetKey(KeyCode.W))
        {
            if (selectedTopping == Topping.NONE || selectedTopping == Topping.BUTTER)
            {
                //butter.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 1f));

                if (Input.GetMouseButtonDown(0))
                {
                    //check if on beat
                    ToggleTopping(Topping.BUTTER);
                }
            }
            selectedTopping = Topping.BUTTER;
        }

        //Syrup
        if (Input.GetKey(KeyCode.A))
        {
            if (selectedTopping == Topping.NONE || selectedTopping == Topping.SYRUP_OLD_FASHIONED)
            {
                syrupContainer.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 1f));

                if (Input.GetMouseButton(0))
                {
                    ToggleTopping(Topping.SYRUP_OLD_FASHIONED);
                    //activate pour effect   
                }
            }
            selectedTopping = Topping.SYRUP_OLD_FASHIONED;
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
        if (Input.GetKeyUp(KeyCode.W)){
            //butter.transform.position = initialButterPosition
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

}
