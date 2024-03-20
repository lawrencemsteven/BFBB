using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enumerations;

public class StationTrackerUI : MonoBehaviour
{
    [SerializeField] private SwitchCamera switchCamera;
    
    void Update()
    {
        foreach (Transform button in transform)
        {
            if (button.GetSiblingIndex() == (int)switchCamera.GetSelectedStationType())
            {
                button.GetComponent<Image>().color = Color.white;
            }
            else if (button.GetSiblingIndex() == (int)switchCamera.GetQueuedStationType())
            {
                button.GetComponent<Image>().color = Color.red;
            }
            else
            {
                button.GetComponent<Image>().color = Color.grey;
            }
        }
    }
}
