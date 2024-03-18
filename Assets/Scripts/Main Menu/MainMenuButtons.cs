using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public CameraController cameraController;
    public ShopAssetManager shopAssetManager;
    public float transitionTime = 3.0f;
    private float transitionAmount = 0.0f;
    public Transform mainMenuButtonsParent;
    public Transform backButtonParent;
    public Transform jukeboxButtonsParent;
    public Transform shopParent;
    public Transform shopStylesParent;

    private Transform previousMenu;
    private Transform currentMenu;
    private bool menuTransitioning;

    private bool moveBackButtonIn = false;
    private bool moveBackButtonOut = false;
    private bool backButtonIn = false;
    private bool enableBackButtonAfterTransition = false;

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
                moveBackButtonDown(lerpAmount);
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

                if (enableBackButtonAfterTransition)
                {
                    backButtonParent.GetComponent<Button>().interactable = true;
                    enableBackButtonAfterTransition = false;
                }
            }
        }
    }

    private void ensureBackButtonIn(bool inside)
    {
        if (backButtonIn == inside)
        {
            if (backButtonIn)
            {
                enableBackButtonAfterTransition = true;
                backButtonParent.GetComponent<Button>().interactable = false;
            }
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
        backButtonParent.localPosition = new Vector3(backButtonParent.localPosition.x, Mathf.Lerp(1080.0f, 0.0f, amount), backButtonParent.localPosition.z);
    }
    private void moveBackButtonDown(float amount)
    {
        backButtonParent.localPosition = new Vector3(backButtonParent.localPosition.x, Mathf.Lerp(0.0f, -1080.0f, amount), backButtonParent.localPosition.z);
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



    // Back Button
    public void backButton()
    {
        if (currentMenu == jukeboxButtonsParent || currentMenu == shopParent)
        {
            cameraController.changeTarget(CameraController.CameraPoses.MAIN, transitionTime);
            transitionTo(mainMenuButtonsParent);
            ensureBackButtonIn(false);
        }
        else if (currentMenu == shopStylesParent)
        {
            cameraController.changeTarget(CameraController.CameraPoses.MAIN, transitionTime);
            transitionTo(shopParent);
            ensureBackButtonIn(true);
        }
    }



    // Main Menu Buttons
    public void MainPlay()
    {
        cameraController.changeTarget(CameraController.CameraPoses.JUKEBOX, transitionTime);
        transitionTo(jukeboxButtonsParent);
        ensureBackButtonIn(true);
    }
    public void MainShop()
    {
        cameraController.changeTarget(CameraController.CameraPoses.MAIN, transitionTime);
        transitionTo(shopParent);
        ensureBackButtonIn(true);
    }
    public void MainOptions()
    {
        Debug.Log("TODO: Options");
    }
    public void MainCredits()
    {
        Debug.Log("TODO: Credits");
    }
    public void MainExit()
    {
        Application.Quit();
    }



    // Jukebox Buttons
    public void JukeboxLeft()
    {
        Debug.Log("TODO: Jukebox Left");
    }
    public void JukeboxRight()
    {
        Debug.Log("TODO: Jukebox Right");
    }
    public void JukeboxPlay()
    {
        Debug.Log("TODO: Jukebox Play");
    }



    // Shop Buttons
    public void ShopWalls()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Walls");
    }
    public void ShopTable()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Table");
    }
    public void ShopBar()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Bar");
    }
    public void ShopLights()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Lights");
    }
    public void ShopWallDiamonds()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Wall Diamonds");
    }
    public void ShopDish()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Dish");
    }
    public void ShopGriddle()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Griddle");
    }
    public void ShopPrep()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Prep");
    }
    public void ShopFloors()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Floors");
    }
    public void ShopBooth()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Booth");
    }
    public void ShopStools()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Stools");
    }
    public void ShopWindow()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Window");
    }
    public void ShopDecor()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Decor");
    }
    public void ShopArt()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Art");
    }
    public void ShopExterior()
    {
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        Debug.Log("TODO: Exterior");
    }



    // Shop Styles Parent
    public void ShopStylesPink()
    {
        Debug.Log("TODO: Pink");
    }
    public void ShopStylesOrange()
    {
        Debug.Log("TODO: Orange");
    }
    public void ShopStylesGreen()
    {
        Debug.Log("TODO: Green");
    }
    public void ShopStylesPurple()
    {
        Debug.Log("TODO: Purple");
    }
    public void ShopStylesCyberpunk()
    {
        Debug.Log("TODO: Cyberpunk");
    }
    public void ShopStylesSpace()
    {
        Debug.Log("TODO: Space");
    }
    public void ShopStylesSea()
    {
        Debug.Log("TODO: Sea");
    }
}
