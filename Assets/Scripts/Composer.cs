using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composer : MonoBehaviour
{
    private float countdownTimer = 10f;
    public bool debugTimer = true;
    private bool isFading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Decrements timer
        if (countdownTimer > 0f && debugTimer == true)
        {
            countdownTimer -= Time.deltaTime;
        }


        //Simulates event triggers for music fade in/fade out
        if (Input.GetKeyDown(KeyCode.DownArrow) || countdownTimer <= 0f)  //Event that will be triggered when internal timer hits 0 (10 seconds), or if we trigger with down arrow
        {
            Debug.Log("Volume is now fading out");
            isFading = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) && isFading == true)  //Holding the key simulates being active on the station
        {
            Debug.Log("Volume is now fading in");
            countdownTimer += 2 * Time.deltaTime;  // Timer increases by 1 second for each second held, can be done for volume as well
            Debug.Log("Reset Timer to " + countdownTimer);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && isFading == true)  //When the player stopsworking on the station or station is back to full volume
        {
            Debug.Log("Volume has stopped fading in");
            countdownTimer = 10f;
            isFading = false;
        }


        //Low pass eq on master or component tracks
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Low pass eq");
        }

        //Pour batter on left click hold
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pouring Batter");
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Stopped Pouring Batter");
        }


    }
}
