using UnityEngine;
using Orders;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class OrderSlip : MonoBehaviour
{
    [SerializeField] private List<Sprite> toppingSprites = new List<Sprite>();
    private Order associatedOrder;
    private CustomerBehavior associatedCustomer;
    private KeyCode keyCode;
    private List<KeyCode> keyCodesByIndex = new List<KeyCode>
    {
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C,
        KeyCode.V,
        KeyCode.B
    };
    private TextMeshProUGUI selectionKeyUI;
    private Slider patienceMeter;
    private float insufficientTimer = 0.5f;
    private float currentInsufficientTimer;
    

    public void SetAssociatedOrder(Order order)
    {
        associatedOrder = order;
        UpdateSlipVisual();
    }
    public void SetAssociatedCustomer(CustomerBehavior customerBehavior) { associatedCustomer = customerBehavior; }
    public void SetKeyCode(KeyCode keyCode)
    { 
        if (selectionKeyUI is null)
        {
            selectionKeyUI = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        this.keyCode = keyCode;
        selectionKeyUI.text = keyCode.ToString();
    }
    public void SetKeyCodeByIndex(int index) { SetKeyCode(keyCodesByIndex[index]); }

    public void Start()
    {
        patienceMeter = transform.GetChild(1).GetComponent<Slider>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            SelectOrder();
        }

        if (associatedOrder is not null && associatedOrder.IsSelected())
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }

        if (currentInsufficientTimer > 0)
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.red;
            currentInsufficientTimer -= Time.deltaTime;
            if (currentInsufficientTimer <= 0)
            {
                transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }

        if (associatedCustomer is not null)
        {
            patienceMeter.value = associatedCustomer.GetPatiencePercent();
            Color meterColor = new Color(1 - associatedCustomer.GetPatiencePercent(), associatedCustomer.GetPatiencePercent(), 0);
            patienceMeter.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = meterColor;
        }
    }

    public void UpdateSlipVisual()
    {
        if (associatedOrder is null)
        {
            return;
        }

        TextMeshProUGUI pancakeAmountText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        pancakeAmountText.text = $"x{associatedOrder.GetMainCourseCount()}";

        Transform toppingDisplay = transform.GetChild(0).GetChild(2);
        for (int i = 0; i < associatedOrder.GetToppings().Count; i++)
        {
            toppingDisplay.GetChild(i).GetComponent<Image>().sprite = toppingSprites[(int)associatedOrder.GetTopping(i) - 1];
        }
    }

    public void SelectOrder()
    {
        if (!Stations.Prep.IsOrderSelected())
        {
            if (ReservoirManager.GetPlates().Count() < 1)
            {
                currentInsufficientTimer = insufficientTimer;
                return;
            }
            
            if (ReservoirManager.GetPancakes().Count() < associatedOrder.GetMainCourseCount())
            {
                currentInsufficientTimer = insufficientTimer;
                return;
            }

            Stations.Prep.SetSelectedOrderAndCustomer(associatedOrder, associatedCustomer);
            associatedOrder.SetSelected(true);
        }
    }
}