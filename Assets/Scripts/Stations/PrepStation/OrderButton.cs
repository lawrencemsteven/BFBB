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
        if (Order.Equals(Stations.Prep.GetPreppedOrder(), associatedOrder))
        {
            associatedCustomer.DeactivateCustomer();
            Stations.Prep.NullifyPreppedOrder();
            Stations.Prep.UpdateCustomerOrders();
            if (associatedOrder.GetMainCourse() == MainCourse.PANCAKE)
            {
                ReservoirManager.GetPancakes().PopMany(associatedOrder.GetMainCourseCount());
            }
            
            if (associatedOrder.GetMainCourse() == MainCourse.WAFFLE)
            {
                ReservoirManager.GetWaffles().PopMany(associatedOrder.GetMainCourseCount());
            }
            Destroy(this);
        }
    }
}