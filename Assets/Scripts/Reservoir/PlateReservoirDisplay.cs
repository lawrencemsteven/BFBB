using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateReservoirDisplay : MonoBehaviour
{
    void Start()
    {
        ReservoirManager.GetPlates().onReservoirUpdated.AddListener(RedoDisplay);
    }

    void RedoDisplay()
    {
        Debug.Log("I can't do that, Dave.");
    }
}
