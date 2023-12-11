using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaffleStationManager : MonoBehaviour
{

    public Camera stationCamera;
    public Animator waffleMakerAnim, pancakeAnim;
    public float bpm = 135f;
    public GameObject pancake, waffle, waffleBatter;
    private int flashCount = 1, countdownNum  = 4;
    private float timeToStartWaffles, elapsedTime, totalElapsed, timeToFlash, timeToFlipPancake, timeToFlipWaffle, timeToFinishPancake, timeToFinishWaffle;
    private bool waffleMakerOpen = false, circleIsVisible = false, pancakeFlipped = false, waffleFlipped = false, flipWaffleTimerStarted = false, flipPancakeTimerStarted = false, finishWaffleTimerStarted = false, finishPancakeTimerStarted = false;
    public TextMeshProUGUI countdown, pancakeCountdown;
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
            countdown.text = countdownNum.ToString();
            countdownNum -= 1;

        }
        if ((elapsedTime >= timeToFlash) && (waffleMakerOpen) && (flashCount <= 4))
        {
            countdown.text = countdownNum.ToString();
            countdownNum -= 1;
            flashCount++;
            elapsedTime = 0;
        }

        if ((timeToFlipWaffle - totalElapsed) <= ((4 / bpm) * 60) && (!flipWaffleTimerStarted))
        {
            flipWaffleTimerStarted = true;
            StartCoroutine(CountBeatsToWaffleFlip());
        }

        if ((timeToFlipPancake - totalElapsed) <= ((4 /bpm) * 60) && (!flipPancakeTimerStarted))
        {
            flipPancakeTimerStarted = true;
            StartCoroutine(CountBeatsPancake());
        }

        if ((timeToFinishPancake - totalElapsed) <= ((4 / bpm) * 60) && (!finishPancakeTimerStarted))
        {
            finishPancakeTimerStarted = true;
            StartCoroutine(CountBeatsPancake());
        }

        if ((timeToFinishWaffle - totalElapsed) <= ((4 / bpm) * 60) && (!finishWaffleTimerStarted))
        {
            finishWaffleTimerStarted = true;
            StartCoroutine(CountBeatsToWaffleFinish());
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
            if (Mathf.Abs(totalElapsed - timeToFinishWaffle) < .4)
            {
                GlobalVariables.score += 1;
            }
        }

        IEnumerator CountBeatsToWaffleFlip()
        {
            float beatInterval = 60f / bpm;

            for (int count = 1; count <= 4; count++)
            {
                countdown.text = (4 - count).ToString();
                Debug.Log($"Beat {count}");
                yield return new WaitForSeconds(beatInterval);
            }

            Debug.Log("Counting finished!");
        }

        IEnumerator CountBeatsToWaffleFinish()
        {
            float beatInterval = 60f / bpm;

            for (int count = 1; count <= 4; count++)
            {
                countdown.text = (4 - count).ToString();
                Debug.Log($"Beat {count}");
                yield return new WaitForSeconds(beatInterval);
            }

            Debug.Log("Counting finished!");
        }

        IEnumerator CountBeatsPancake()
        {
            float beatInterval = 60f / bpm;

            for (int count = 1; count <= 4; count++)
            {
                pancakeCountdown.text = (4 - count).ToString();
                Debug.Log($"Beat {count}");
                yield return new WaitForSeconds(beatInterval);
            }

            Debug.Log("Counting finished!");
        }

    }
}
