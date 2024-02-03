using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;
    [SerializeField] private GameObject countdown1, countdown2;
    [SerializeField] private fmodTimer timer;
    [SerializeField] private GameObject dishStation, waffleStation, batterPourTool;
    private bool waitingToSwitch;
    private int switchReqBar;
    private FMOD.Studio.EventInstance eventInstance;
    private int switchToCam;
    private string eventObjectName;

    void Start()
    {

        if (eventObjectName == null || eventObjectName == "") eventObjectName = "FMOD Music Event";
        
        if(dishStation == null) dishStation = GameObject.Find("Plate Manager (1)");
        countdown1.SetActive(false);
        countdown2.SetActive(false);

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
            switchToCam = 0;
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GlobalVariables.camState != 1)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToCam = 1;
            countdown1.SetActive(true);
            countdown2.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && GlobalVariables.camState != 2)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToCam = 2;
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && GlobalVariables.camState != 3)
        {
            Debug.Log("i run!");
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToCam = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && GlobalVariables.camState != 4)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToCam = 4;
        }

        Debug.Log(waitingToSwitch);
        Debug.Log(switchToCam);

        if(waitingToSwitch && timer.bar != switchReqBar)
        {
            waitingToSwitch = false;
            GlobalVariables.camState = switchToCam;
            switchCamera(switchToCam);
        }

    }

    private void switchCamera(int cameraIndex)
    {
        foreach (GameObject camera in cameras)
        {
            camera.SetActive(false);
        }

        cameras[cameraIndex].SetActive(true);

        //this sucks and ill fix it later but i'd like to get clearance on overhauling the stations first
        dishStation.SetActive(false);
        waffleStation.SetActive(false);
        batterPourTool.SetActive(false);
        PrepStationManager.Instance.SetRunning(false);

        if (cameraIndex == 0)
        {
            dishStation.SetActive(true);
        }
        else if (cameraIndex == 1)
        {
            waffleStation.SetActive(true);
            batterPourTool.SetActive(true);
        }
        else if (cameraIndex == 3)
        {
            PrepStationManager.Instance.SetRunning(true);
        }
    }
}
