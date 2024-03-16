using UnityEngine;
using Orders;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class OrderButton : MonoBehaviour
{
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
    
    public void SetAssociatedOrder(Order order) { associatedOrder = order; }
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

        patienceMeter.value = associatedCustomer.GetPatiencePercent();
        Color meterColor = new Color(1 - associatedCustomer.GetPatiencePercent(), associatedCustomer.GetPatiencePercent(), 0);
        patienceMeter.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = meterColor;
    }

    public void SelectOrder()
    {
        if (!Stations.Prep.IsOrderSelected())
        {
            Debug.Log(ReservoirManager.GetPlates().Count());
            Debug.Log(ReservoirManager.GetPancakes().Count());

            if (ReservoirManager.GetPlates().Count() < 1)
            {
                Debug.Log($"Need a clean plate in reservoir");
                return;
            }
            
            if (ReservoirManager.GetPancakes().Count() < associatedOrder.GetMainCourseCount())
            {
                Debug.Log($"Need {associatedOrder.GetMainCourseCount() - ReservoirManager.GetPancakes().Count()} more pancakes in reservoir");
                return;
            }

            Stations.Prep.SetSelectedOrderAndCustomer(associatedOrder, associatedCustomer);
            associatedOrder.SetSelected(true);
        }
    }
}