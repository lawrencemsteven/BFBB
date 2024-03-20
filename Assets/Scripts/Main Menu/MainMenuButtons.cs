using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enumerations;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private SwitchCamera switchCamera;
    public CameraController cameraController;
    public ShopAssetManager shopAssetManager;
    public float transitionTime = 3.0f;
    private float transitionAmount = 0.0f;
    public Transform mainMenuButtonsParent;
    public Transform backButtonParent;
    public Transform jukeboxButtonsParent;
    public Transform shopParent;
    public Transform shopStylesParent;
    public Transform endOfDayParent;

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
        gameObject.SetActive(true);
        switchCamera.transform.parent.GetComponent<Canvas>().enabled = false;
    }

    public void Update()
    {
        if (menuTransitioning)
        {
            transitionAmount += Time.deltaTime;
            float lerpAmount = Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Slow, Interpolawrence.InterpolawrenceSpeeds.Slow, Mathf.Clamp(transitionAmount / transitionTime, 0.0f, 1.0f));

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
                    if (currentMenu == shopStylesParent)
                    {
                        shopAssetManager.activateButtons();
                    }
                    else
                    {
                        // Activate all buttons
                        Button button = child.GetComponent<Button>();
                        if (button != null)
                        {
                            button.interactable = true;
                        }
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

                if (backButtonIn)
                {
                    moveBackButton(1.0f);
                    backButtonParent.GetComponent<Button>().interactable = true;
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
                backButtonParent.GetComponent<Button>().interactable = true;
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
            backButtonParent.GetComponent<Button>().interactable = true;
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

    public void backToMainMenu()
    {
        transitionTo(mainMenuButtonsParent);
        cameraController.changeTarget(CameraController.CameraPoses.MAIN, 2.0f);
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
        cameraController.SetUseGameCameras(true);
        switchCamera.Switch(StationType.DISH);
        Cursor.visible = false;
        switchCamera.transform.parent.GetComponent<Canvas>().enabled = true;
        gameObject.SetActive(false);
    }



    // Shop Buttons
    public void ShopWalls()
    {
        cameraController.changeTarget(CameraController.CameraPoses.WALLS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Walls);
    }
    public void ShopTable()
    {
        cameraController.changeTarget(CameraController.CameraPoses.TABLE, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Tables);
    }
    public void ShopBar()
    {
        cameraController.changeTarget(CameraController.CameraPoses.COUNTER, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Counter);
    }
    public void ShopLights()
    {
        cameraController.changeTarget(CameraController.CameraPoses.LIGHTS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Lights);
    }
    public void ShopWallDiamonds()
    {
        cameraController.changeTarget(CameraController.CameraPoses.WALLS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.WallDiamonds);
    }
    public void ShopDish()
    {
        cameraController.changeTarget(CameraController.CameraPoses.DISH_STATION, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Sink);
    }
    public void ShopGriddle()
    {
        cameraController.changeTarget(CameraController.CameraPoses.GRIDDLE_STATION, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.GriddleStation);
    }
    public void ShopPrep()
    {
        cameraController.changeTarget(CameraController.CameraPoses.PREP_STATION, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.PrepStation);
    }
    public void ShopFloors()
    {
        cameraController.changeTarget(CameraController.CameraPoses.FLOORS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Floor);
    }
    public void ShopBooth()
    {
        cameraController.changeTarget(CameraController.CameraPoses.BOOTH, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Seats);
    }
    public void ShopStools()
    {
        cameraController.changeTarget(CameraController.CameraPoses.STOOLS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Stools);
    }
    public void ShopWindow()
    {
        cameraController.changeTarget(CameraController.CameraPoses.WINDOWS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Windows);
    }
    public void ShopDecor()
    {
        cameraController.changeTarget(CameraController.CameraPoses.TABLE_ACCESSORIES, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Condiments);
    }
    public void ShopArt()
    {
        cameraController.changeTarget(CameraController.CameraPoses.WALLS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.WallArt);
    }
    public void ShopExterior()
    {
        cameraController.changeTarget(CameraController.CameraPoses.WINDOWS, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.Background);
    }
    public void ShopAll()
    {
        cameraController.changeTarget(CameraController.CameraPoses.MAIN, transitionTime);
        transitionTo(shopStylesParent);
        ensureBackButtonIn(true);
        shopAssetManager.setSection(ShopAssetManager.ShopSections.All);
    }

    public void ShowShiftComplete()
    {
        cameraController.changeTarget(CameraController.CameraPoses.CASH_REGISTER, transitionTime);
        transitionTo(endOfDayParent);
        ensureBackButtonIn(true);
    }



    // Shop Styles Parent
    public void ShopStylesPink()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Pink);
    }
    public void ShopStylesOrange()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Orange);
    }
    public void ShopStylesGreen()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Green);
    }
    public void ShopStylesPurple()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Purple);
    }
    public void ShopStylesDefault()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Default);
    }
    public void ShopStylesCyberpunk()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Cyberpunk);
    }
    public void ShopStylesSpace()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Space);
    }
    public void ShopStylesSea()
    {
        shopAssetManager.setStyle(ShopAssetManager.ShopStyles.Sea);
    }
}
