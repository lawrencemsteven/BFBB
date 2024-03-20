using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReservoirTrackerUI : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"Dishes: {ReservoirManager.GetPlates().Count()}\nPancakes: {ReservoirManager.GetPancakes().Count()}";
    }
}
