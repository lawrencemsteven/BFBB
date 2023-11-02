using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public AudioSource music;
    //GlobalVariables globals;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalVariables.missCounter >= 10 || GlobalVariables.notesMissed >= 10)
        {
            SceneManager.LoadScene("LoseScreen");
        }
        
        if(music.time >= 54)
        {
            SceneManager.LoadScene("EndGameScreen");
        }
    }
}
