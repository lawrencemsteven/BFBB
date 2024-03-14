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
    private bool selected = false;
    private TextMeshProUGUI selectionKeyUI;
    
    public void SetAssociatedOrder(Order order) { associatedOrder = order; }
    public void SetAssociatedCustomer(CustomerBehavior customerBehavior) { associatedCustomer = customerBehavior; }
    public void SetKeyCode(KeyCode keyCode)
    { 
        if (selectionKeyUI is null)
        {
            selectionKeyUI = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        this.keyCode = keyCode;
        selectionKeyUI.text = keyCode.ToString();
    }
    public void SetKeyCodeByIndex(int index) { SetKeyCode(keyCodesByIndex[index]); }

    public void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            SelectOrder();
        }
    }

    public void SelectOrder()
    {
        if (!Stations.Prep.IsOrderSelected())
        {
            Stations.Prep.SetSelectedOrder(associatedOrder);
            selected = true;
            transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
    }
}