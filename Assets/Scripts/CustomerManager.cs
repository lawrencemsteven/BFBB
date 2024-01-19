using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<Material> moodMaterials = new List<Material>();
    private List<CustomerBehavior> customers = new List<CustomerBehavior>();
    private float customerActivationTimer = 5f;
    private float currentCustomerActivationTimer;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            CustomerBehavior customer = child.GetComponent<CustomerBehavior>();
            if (customer is not null)
            {
                customers.Add(customer);
            }
        }

        currentCustomerActivationTimer = customerActivationTimer;
    }

    private void Update()
    {
        currentCustomerActivationTimer -= Time.deltaTime;

        if (currentCustomerActivationTimer <= 0)
        {
            ActivateRandomCustomer();
            currentCustomerActivationTimer = customerActivationTimer;
        }
    }

    private void ActivateRandomCustomer()
    {
        List<CustomerBehavior> disabledCustomers = customers.Where(customer => customer.IsActive() == false ).ToList();
        
        if (disabledCustomers.Count < 1)
        {
            return;
        }

        CustomerBehavior selectedCustomer = disabledCustomers[Random.Range(0, disabledCustomers.Count)];
        
        selectedCustomer.ActivateCustomer();
    }

    public Material GetMaterialFromMood(int mood)
    { 
        return moodMaterials[Mathf.Max(0, mood - 1)];
    }
}