using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public CameraController cameraController;
    public float transitionTime = 3.0f;
    private float transitionAmount = 0.0f;
    public Transform mainMenuButtonsParent;
    public Transform backButtonParent;
    public Transform jukeboxButtonsParent;

    private Transform previousMenu;
    private Transform currentMenu;
    private bool menuTransitioning;

    private bool moveBackButtonIn = false;
    private bool moveBackButtonOut = false;
    private bool backButtonIn = false;

    public void Start()
    {
        previousMenu = mainMenuButtonsParent;
        currentMenu = mainMenuButtonsParent;
    }

    public void Update()
    {
        if (menuTransitioning)
        {
            transitionAmount += Time.deltaTime;
            float lerpAmount = Stevelation.Lerp(Stevelation.StevelationSpeeds.Slow, Stevelation.StevelationSpeeds.Slow, Mathf.Clamp(transitionAmount / transitionTime, 0.0f, 1.0f));

            previousMenu.localPosition = new Vector3(previousMenu.localPosition.x, Mathf.Lerp(0.0f, -1080.0f, lerpAmount), previousMenu.localPosition.z);
            currentMenu.localPosition = new Vector3(currentMenu.localPosition.x, Mathf.Lerp(1080.0f, 0.0f, lerpAmount), currentMenu.localPosition.z);

            if (moveBackButtonIn)
            {
                moveBackButton(lerpAmount);
            }
            else if (moveBackButtonOut)
            {
                moveBackButton(1.0f - lerpAmount);
            }

            if (transitionAmount >= transitionTime)
            {
                menuTransitioning = false;
                foreach (Transform child in currentMenu)
                {
                    // Activate all buttons
                    Button button = child.GetComponent<Button>();
                    if (button != null)
                    {
                        button.interactable = true;
                    }
                }

                if (moveBackButtonIn)
                {
                    backButtonParent.GetComponent<Button>().interactable = true;
                    moveBackButtonIn = false;
                }
                else if (moveBackButtonOut)
                {
                    moveBackButtonOut = true;
                }
            }
        }
    }

    private void ensureBackButtonIn(bool inside)
    {
        if (backButtonIn == inside)
        {
            return;
        }

        if (inside)
        {
            moveBackButtonIn = true;
            backButtonIn = true;
        }
        else
        {
            moveBackButtonOut = true;
            backButtonParent.GetComponent<Button>().interactable = false;
            backButtonIn = false;
        }
    }

    private void moveBackButton(float amount)
    {
        backButtonParent.localPosition = new Vector3(backButtonParent.localPosition.x, Mathf.Lerp(194.0f, 0.0f, amount), backButtonParent.localPosition.z);
    }

    public void transitionTo(Transform newMenu)
    {
        menuTransitioning = true;
        transitionAmount = 0.0f;
        previousMenu = currentMenu;
        currentMenu = newMenu;
        foreach (Transform child in previousMenu)
        {
            // Deactivate all buttons
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;
            }
        }
    }
    public void backButton()
    {
        cameraController.changeTarget(CameraController.CameraPoses.MAIN, transitionTime);
        transitionTo(mainMenuButtonsParent);
        ensureBackButtonIn(false);
    }


    public void Play()
    {
        cameraController.changeTarget(CameraController.CameraPoses.JUKEBOX, transitionTime);
        transitionTo(jukeboxButtonsParent);
        ensureBackButtonIn(true);
    }

    public void Shop()
    {
        Debug.Log("TODO: Shop");
    }

    public void Options()
    {
        Debug.Log("TODO: Options");
    }

    public void Credits()
    {
        Debug.Log("TODO: Credits");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
