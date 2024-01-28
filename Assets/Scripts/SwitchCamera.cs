using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject Camera_3;
    public GameObject Camera_4;
    public GameObject Camera_5;
    public GameObject countdown1, countdown2;
    public int CamState;
    public fmodTimer timer;
    public bool waitingToSwitch;
    public GameObject dishStation, waffleStation, batterPourTool;
    public int switchReqBar;
    public FMOD.Studio.EventInstance eventInstance;
    public int switchToCam;
    public string eventObjectName;

    void Start()
    {

        if (eventObjectName == null || eventObjectName == "") eventObjectName = "FMOD Music Event";
        
        if(dishStation == null) dishStation = GameObject.Find("Plate Manager (1)");
        countdown1.SetActive(false);
        countdown2.SetActive(false);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GlobalVariables.camState != 1)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToCam = 1;
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 1);
            countdown1.SetActive(true);
            countdown2.SetActive(true);

            // Cam_1();
            GlobalVariables.camState = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GlobalVariables.camState != 2)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            Cam_2();
            GlobalVariables.camState = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && GlobalVariables.camState != 3)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToCam = 3;
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 0);
            //Cam_3();
            GlobalVariables.camState = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && GlobalVariables.camState != 4)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            Cam_4();
            GlobalVariables.camState = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && GlobalVariables.camState != 5)
        {
            countdown1.SetActive(false);
            countdown2.SetActive(false);
            Cam_5();
            GlobalVariables.camState = 4;
        }

        if(waitingToSwitch && timer.bar != switchReqBar)
        {
            waitingToSwitch = false;
            
            
            if(switchToCam == 3)
            {
                Cam_3();
                CamState = 3;   
            }
            else if(switchToCam == 1)
            {
                Cam_1();
                CamState = 1;
            }

        }

    }

    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }
    public void ManageCamera()
    {
        if(GlobalVariables.camState == 1)
        {
            Cam_1();
            GlobalVariables.camState = 1;
        }
        else if(GlobalVariables.camState == 2)
        {
            Cam_2();
            GlobalVariables.camState = 2;
        }
        else if (GlobalVariables.camState == 3)
        {
            Cam_3();
            GlobalVariables.camState = 3;
        }
        else if (GlobalVariables.camState == 4)
        {
            Cam_4();
            GlobalVariables.camState  = 4;
        }
        else
        {
            Cam_5();
            GlobalVariables.camState = 1;
        }
    }

    void Cam_1()
    {
        Camera_1.SetActive(true);
        Camera_2.SetActive(false);
        Camera_3.SetActive(false);
        Camera_4.SetActive(false);
        Camera_5.SetActive(false);
        dishStation.SetActive(false);
        waffleStation.SetActive(true);
        batterPourTool.SetActive(true);
    }

    void Cam_2()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(true);
        Camera_3.SetActive(false);
        Camera_4.SetActive(false);
        Camera_5.SetActive(false);
        dishStation.SetActive(false);
        waffleStation.SetActive(false);
        batterPourTool.SetActive(false);
    }

    void Cam_3()
    {
        
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        Camera_3.SetActive(true);
        Camera_4.SetActive(false);
        dishStation.SetActive(true);
        waffleStation.SetActive(false);
        batterPourTool.SetActive(false);
        Camera_5.SetActive(false);

    }

    void Cam_4()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        Camera_3.SetActive(false);
        Camera_4.SetActive(true);
        Camera_5.SetActive(false);
        dishStation.SetActive(false);
        waffleStation.SetActive(false);
        batterPourTool.SetActive(false);
    }

    void Cam_5()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        Camera_3.SetActive(false);
        Camera_4.SetActive(false);
        Camera_5.SetActive(true);
        dishStation.SetActive(false);
        waffleStation.SetActive(false);
        batterPourTool.SetActive(false);
    }
}
