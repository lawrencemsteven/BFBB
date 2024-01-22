using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCommands : MonoBehaviour
{
    List<string> commands = new List<string>();
    List<string> commandBuffer = new List<string>();
    private bool executing = false;
    public GameObject Customer;
    public GameObject currentCustomer;
    public Vector3 entrance = new Vector3(10f, 2f, -4f);
    public Vector3 counter1 = new Vector3(6f, 2f, -1f);
    public Quaternion spawnRotation = new Quaternion(0f, 0f, 0f, 0f);

    private List<GameObject> existingCustomers = new List<GameObject>();

    //Command List
    //Commands are entered commandName/Arg1/Arg2/ect.
    //createCustomer Arguments: none
    //moveCustomer Arguments: customerName, location

    // Start is called before the first frame update
    void Start()
    {
        commands.Add("createCustomer");
        commands.Add("moveCustomer/0");
    }

    // Update is called once per frame
    void Update()
    {
        if (!executing) {
            if (commandBuffer.Count != 0)
            {
                commands = new List<string>(commandBuffer);
                commandBuffer = new List<string>();
            }
            if (commands.Count != 0)
            {
            executeCommands();
            }
        }
    }

    void executeCommands()
    {
        executing = true;
        int i = 0;
        foreach (string command in commands)
        {
            string[] commandSplit = command.Split("/");
            switch (commandSplit[0])
            {
                case "createCustomer":
                currentCustomer = Instantiate(Customer, entrance, spawnRotation);
                existingCustomers.Add(currentCustomer);
                break;
                case "moveCustomer":
                int customerNum = int.Parse(commandSplit[1]);
                existingCustomers[customerNum].transform.position = counter1;
                break;
            }
        }
        commands = new List<string>();
        executing = false;
    }
}
