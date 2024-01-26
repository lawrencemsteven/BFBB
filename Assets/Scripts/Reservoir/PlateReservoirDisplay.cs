using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateReservoirDisplay : MonoBehaviour
{
    [SerializeField] private DishManager dishManager;

    void Start()
    {
        dishManager.reservoir.onReservoirUpdated.AddListener(RedoDisplay);
    }

    void RedoDisplay()
    {
        Debug.Log("I can't do that, Dave.");
    }
}
