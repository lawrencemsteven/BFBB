using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enumerations;

public class TutorialMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private PauseMenuScript pauseMenu;
    [SerializeField] private List<Sprite> tutorialImages = new List<Sprite>();
    bool MenuScreenOn = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !pauseMenu.IsOpen())
        {
            if (MenuScreenOn == false)
            {
                Time.timeScale = 0;
                MenuScreenOn = true;
                menuScreen.SetActive(true);
                updateTutorialImage();
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                MenuScreenOn = false;
                menuScreen.SetActive(false);
                Cursor.visible = false;
            }
        }
    }

    private void updateTutorialImage()
    {
        //yknow 3 hours ago i mightve tried to make this less dogshit but whatever man
        if (Stations.Dish.IsRunning())
        {
            menuScreen.transform.GetChild(0).GetComponent<Image>().sprite = tutorialImages[0];
        }
        else if (Stations.Pancake.IsRunning())
        {
            menuScreen.transform.GetChild(0).GetComponent<Image>().sprite = tutorialImages[1];
        }
        else if (Stations.Coffee.IsRunning())
        {
            menuScreen.transform.GetChild(0).GetComponent<Image>().sprite = tutorialImages[2];
        }
        else if (Stations.Prep.IsRunning())
        {
            menuScreen.transform.GetChild(0).GetComponent<Image>().sprite = tutorialImages[3];
        }
        else
        {
            menuScreen.transform.GetChild(0).GetComponent<Image>().sprite = tutorialImages[4];
        }
    }

    public void ForceClose()
    {
        Debug.Log("CLICKED");
        Time.timeScale = 1f;
        MenuScreenOn = false;
        menuScreen.SetActive(false);
        Cursor.visible = false;
    }

    public bool IsOpen() { return MenuScreenOn; }
}

