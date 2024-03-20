using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private SwitchCamera switchCamera;

    public ScriptUsageTimeline pauser;
    bool MenuScreenOn = false;
    
    // Update is called once per frame
    void Update()
    {     
        if (!cameraController.UsingGameCameras())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuScreenOn == false)
            {
                Time.timeScale = 0;
                MenuScreenOn = true;
                menuScreen.SetActive(true);
                Cursor.visible = true;
                pauser.Pause();
            }
            else
            {
                Time.timeScale = 1f;
                MenuScreenOn = false;
                menuScreen.SetActive(false);
                Cursor.visible = false;
                pauser.Unpause();
            }
        }
    }

    public void ExitToMainMenu()
    {
        pauser.Unpause();
        pauser.StopMusic();
        Time.timeScale = 1f;
        MenuScreenOn = false;
        menuScreen.SetActive(false);
        cameraController.SetUseGameCameras(false);
        mainMenuButtons.SetActive(true);
        switchCamera.DeactivateAll();
        Cursor.visible = true;
    }
}

