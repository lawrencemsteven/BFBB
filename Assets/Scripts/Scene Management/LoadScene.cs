using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string scene = "";
    public bool spaceBarTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!spaceBarTrigger && Input.GetKeyDown(KeyCode.R) || spaceBarTrigger && Input.GetKeyDown(KeyCode.Space))
        //{
            //SceneManager.LoadScene(scene);
        //}
    }

    public void startGame() {
        SceneManager.LoadScene(scene);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(scene);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
