using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enumerations;

public class StationTrackerUI : MonoBehaviour
{
    [SerializeField] private SwitchCamera switchCamera;
    
    List<GameObject> selectedTabs = new List<GameObject>();
    List<GameObject> unselectedTabs = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            unselectedTabs.Add(child.gameObject);
        }
        
        foreach (Transform child in transform.GetChild(1))
        {
            selectedTabs.Add(child.gameObject);
        }
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == (int)switchCamera.GetSelectedStationType())
            {
                unselectedTabs[i].GetComponent<Image>().enabled = false;
                selectedTabs[i].GetComponent<Image>().enabled = true;
            }
            else if (i == (int)switchCamera.GetQueuedStationType())
            {
                unselectedTabs[i].GetComponent<Image>().enabled = true;
                selectedTabs[i].GetComponent<Image>().enabled = false;
                unselectedTabs[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                unselectedTabs[i].GetComponent<Image>().enabled = true;
                selectedTabs[i].GetComponent<Image>().enabled = false;
                unselectedTabs[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}