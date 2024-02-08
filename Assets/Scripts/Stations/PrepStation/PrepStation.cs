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
    private TextMeshProUGUI orderLabel;


    public void Start()
    {
        orderList = prepStationUI.transform.GetChild(0);
        orderLabel = prepStationUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        prepStationUI.SetActive(false);
        preppedOrder = null;

        //Test code
        ReservoirManager.GetPancakes().Add(new ReservoirPancake(1));
        ReservoirManager.GetPancakes().Add(new ReservoirPancake(1));
        ReservoirManager.GetWaffles().Add(new ReservoirWaffle(1));
    }

    public void Update()
    {
        prepStationUI.SetActive(running);
        if (running && preppedOrder is not null)
        {
            orderLabel.text = preppedOrder.ToString();
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

        else if (preppedOrder.GetToppings().Contains(topping))
        {
            preppedOrder.RemoveTopping(topping);
        }

        else
        {
            preppedOrder.AddTopping(topping);
        }
    }

    public void AddPancake() { SelectMainCourse(MainCourse.PANCAKE); }
    public void AddWaffle() { SelectMainCourse(MainCourse.WAFFLE); }
    public void ToggleChocolateChips() { ToggleTopping(Topping.CHOCOLATE_CHIP); }

    public void NullifyPreppedOrder()
    {
        preppedOrder = null;
        orderLabel.text = "";
    }
    public Order GetPreppedOrder() { return preppedOrder; }
    public void SetRunning(bool running) { this.running = running; }
}
