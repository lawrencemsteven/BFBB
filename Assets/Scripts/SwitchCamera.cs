using UnityEngine;
using UnityEngine.UI;
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
    private ScoreAndStreakManager scoreManager;
    private Station station;

    void Start()
    {       
        scoreManager = GetComponent<ScoreAndStreakManager>();
        if (eventObjectName == null || eventObjectName == "") eventObjectName = "FMOD Music Event";
        
        countdown1.SetActive(false);
        countdown2.SetActive(false);

        switchCamera(StationType.DISH);
        getStationByEnum(StationType.PANCAKE).Deactivate();
        getStationByEnum(StationType.PREP).Deactivate();
        getStationByEnum(StationType.COFFEE).Deactivate();

        GlobalVariables.camState = 0;
        GlobalVariables.currentStation = "Dish";
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GlobalVariables.camState != 0)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToStation = StationType.DISH;
            GlobalVariables.currentStation = "Dish";
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GlobalVariables.camState != 1)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            GlobalVariables.currentStation = "Pancake";
            switchToStation = StationType.PANCAKE;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && GlobalVariables.camState != 2)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            GlobalVariables.currentStation = "Coffee";
            switchToStation = StationType.COFFEE;
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("SongSection", 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && GlobalVariables.camState != 3)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            GlobalVariables.currentStation = "Prep";
            switchToStation = StationType.PREP;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && GlobalVariables.camState != 4)
        {
            switchReqBar = timer.bar;
            waitingToSwitch = true;
            switchToStation = StationType.OVERHEAD_VIEW;
        }

        if(waitingToSwitch && timer.bar != switchReqBar)
        {
            waitingToSwitch = false;
            GlobalVariables.camState = (int)switchToStation;
            scoreManager.resetStreak();
            switchCamera(switchToStation);
        }

    }

    private void switchCamera(StationType stationType)
    {
        station = getStationByEnum(stationType);
        Station selectedStation = getStationByEnum(selectedStationType);
        if (selectedStation is null)
        {
            overheadCamera.SetActive(false);
        }
        else
        {
            selectedStation.Deactivate();
        }

        if (station is null)
        {
            overheadCamera.SetActive(true);
        }
        else
        {
            station.Activate();
        }
        
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



    public Station getCurrentStation()
    {
        return station;
    }
}
