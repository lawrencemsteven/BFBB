using UnityEngine;
using Orders;

public class OrderButton : MonoBehaviour
{
    private Order associatedOrder;
    private CustomerBehavior associatedCustomer;
    
    public void SetAssociatedOrder(Order order) { associatedOrder = order; }
    public void SetAssociatedCustomer(CustomerBehavior customerBehavior) { associatedCustomer = customerBehavior; }

    public void CheckOrder()
    {
        if (Order.Equals(PrepStationManager.Instance.GetPreppedOrder(), associatedOrder))
        {
            associatedCustomer.DeactivateCustomer();
            PrepStationManager.Instance.NullifyPreppedOrder();
            PrepStationManager.Instance.UpdateCustomerOrders();
            Destroy(this);
        }
    }
}