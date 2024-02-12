using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;
using Enumerations;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject countdown1, countdown2;
    [SerializeField] private fmodTimer timer;
    [SerializeField] private GameObject overheadCamera;
    private bool waitingToSwitch;
    private int switchReqBar;
    private FMOD.Studio.EventInstance eventInstance;
    private string eventObjectName;
    private StationType switchToStation;
    private StationType selectedStationType;

    void Start()
    {
        if (eventObjectName == null || eventObjectName == "") eventObjectName = "FMOD Music Event";
        
        countdown1.SetActive(false);
        countdown2.SetActive(false);

        switchCamera(StationType.DISH);

        GlobalVariables.camState = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GlobalVariables.camState != 0)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);

            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToStation = StationType.DISH;
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GlobalVariables.camState != 1)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToStation = StationType.PANCAKE;
            countdown1.SetActive(true);
            countdown2.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && GlobalVariables.camState != 2)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToStation = StationType.COFFEE;
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && GlobalVariables.camState != 3)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToStation = StationType.PREP;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && GlobalVariables.camState != 4)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToStation = StationType.OVERHEAD_VIEW;
        }

        if(waitingToSwitch && timer.bar != switchReqBar)
        {
            waitingToSwitch = false;
            GlobalVariables.camState = (int)switchToStation;
            switchCamera(switchToStation);
        }

    }

    private void switchCamera(StationType stationType)
    {
        Station station = getStationByEnum(stationType);
        Station selectedStation = getStationByEnum(selectedStationType);

        if (selectedStation is null)
        {
            overheadCamera.SetActive(false);
        }
        else
        {
            selectedStation.Deactivate();
        }

        Debug.Log($"Deactivating {selectedStation}");

        if (station is null)
        {
            overheadCamera.SetActive(true);
        }
        else
        {
            station.Activate();
        }

        Debug.Log($"Activating {station}");
        
        selectedStationType = stationType;
    }

    private Station getStationByEnum(StationType stationType)
    {
        switch (stationType)
        {
            case StationType.DISH:
                return Stations.Dish;
            
            case StationType.PANCAKE:
                return Stations.Pancake;

            case StationType.COFFEE:
                return Stations.Coffee;

            case StationType.PREP:
                return Stations.Prep;
            
            default:
                return null;
        }
    }
}
