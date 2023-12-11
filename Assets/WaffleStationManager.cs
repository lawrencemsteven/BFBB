using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaffleStationManager : MonoBehaviour
{

    public Camera stationCamera;
    public Animator waffleMakerAnim, pancakeAnim;
    public float bpm = 135f;
    public GameObject uiCircle, pancake, waffle, waffleBatter;
    private int flashCount = 1, countdownNum  = 4;
    private float timeToStartWaffles, elapsedTime, totalElapsed, timeToFlash, timeToFlipPancake, timeToFlipWaffle, timeToFinishPancake, timeToFinishWaffle;
    private bool waffleMakerOpen = false, circleIsVisible = false, startFlashing = false, pancakeFlipped = false, waffleFlipped=false;
    public TextMeshProUGUI countdown;
    // Start is called before the first frame update
    void Start()
    {
        timeToStartWaffles  = (16 / bpm) * 60;
        timeToFlipPancake = (26 / bpm) * 60;
        timeToFlipPancake = (28 / bpm) * 60;
        timeToFlipWaffle = (30 / bpm) * 60;
        timeToFinishPancake = (32 / bpm) * 60;
        timeToFinishWaffle = (34 / bpm) * 60;
        timeToFlash = (1 / (bpm)) * 60;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        totalElapsed += Time.deltaTime;
        if ((elapsedTime >= timeToStartWaffles) && (!waffleMakerOpen))
        {
            waffleMakerOpen = true;
            waffleMakerAnim.SetTrigger("WaffleStationCameraActive");
            elapsedTime = 0;
            circleIsVisible = !circleIsVisible;
            uiCircle.SetActive(circleIsVisible);
            countdown.text = countdownNum.ToString();
            countdownNum -= 1;

        }
        if ((elapsedTime >= timeToFlash) && (waffleMakerOpen) && (flashCount <= 4))
        {
            countdown.text = countdownNum.ToString();
            countdownNum -= 1;
            flashCount++;
            circleIsVisible = !circleIsVisible;
            uiCircle.SetActive(circleIsVisible);
            elapsedTime = 0;
        }
        if ((Input.GetKeyDown(KeyCode.F) && (!pancakeFlipped)))
        {
            pancakeFlipped = true;
            pancakeAnim.SetTrigger("FlipTrigger");
            pancakeAnim.SetBool("isFlipped", pancakeFlipped);
            if (Mathf.Abs(totalElapsed - timeToFlipPancake) < .4)
            {
                GlobalVariables.score += 1;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.F)) && ((pancakeFlipped)))
        {
            pancakeFlipped = false;
            pancakeAnim.SetTrigger("FlipTrigger");
            pancakeAnim.SetBool("isFlipped", pancakeFlipped);
            if (Mathf.Abs(totalElapsed - timeToFinishPancake) < .4)
            {
                GlobalVariables.score += 1;
            }
        }
        if ((Input.GetKeyDown(KeyCode.Space) && (!waffleFlipped)))
        {
            waffleFlipped = true;
            waffleMakerAnim.SetTrigger("Flip");
            if (Mathf.Abs(totalElapsed - timeToFlipWaffle) < .4)
            {
                GlobalVariables.score += 1;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.Space)) && (waffleFlipped))
        {
            waffleBatter.SetActive(false);
            waffle.SetActive(true);
            waffleFlipped = false;
            waffleMakerAnim.SetTrigger("FlipBack");
            pancake.SetActive(false);
            if (Mathf.Abs(totalElapsed - timeToFinishWaffle) < .4)
            {
                GlobalVariables.score += 1;
            }
        }



    }
}
