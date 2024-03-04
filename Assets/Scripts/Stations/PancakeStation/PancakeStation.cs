using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PancakeStation : Station
{

    public Animator waffleMakerAnim, pancakeAnim;
    public float bpm = 135f;
    public Vector3 mousePosition;
    public GameObject waffle, waffleBatter;
    private int flashCount = 1, countdownNum  = 4;
    private float timeToStartWaffles, elapsedTime, totalElapsed, timeToFlash, timeToFlipPancake, timeToFlipWaffle, timeToFinishPancake, timeToFinishWaffle;
    private bool waffleMakerOpen = false, circleIsVisible = false, pancakeFlipped = false, waffleFlipped = false, flipWaffleTimerStarted = false, flipPancakeTimerStarted = false, finishWaffleTimerStarted = false, finishPancakeTimerStarted = false;
    public TextMeshProUGUI countdown, pancakeCountdown;
    private PancakeParticleObject pancakeParticleObject;
    // Start is called before the first frame update
    void Start()
    {
        timeToStartWaffles  = (16 / bpm) * 60;
        timeToFlipPancake = (26 / bpm) * 60;
        timeToFlipWaffle = (30 / bpm) * 60;
        timeToFinishPancake = (34 / bpm) * 60;
        timeToFinishWaffle = (38 / bpm) * 60;
        timeToFlash = (1 / (bpm)) * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousePosition = Input.mousePosition;
        }
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

        if (Input.GetMouseButtonDown(1) && (!pancakeFlipped))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float swipeDistance = currentMousePosition.y - mousePosition.y;

            // Check if the swipe is upwards
            if (swipeDistance > 1)
            {

                pancakeParticleObject.Flip();
            }
        }
        else if (Input.GetMouseButtonDown(1) && pancakeFlipped)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float swipeDistance = currentMousePosition.y - mousePosition.y;
            Debug.Log(swipeDistance);
            // Check if the swipe is upwards
            if (swipeDistance > 1)
            {
                pancakeParticleObject.Flip();
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

            for (int count = 4; count >= 1; count--)
            {
                if ((4 - count) > 0)
                {
                    //countdown.text = $"SPACE {count}";
                }
                else
                {
                    //countdown.text = "";
                }
                yield return new WaitForSeconds(beatInterval);
            }
        }

        IEnumerator CountBeatsToWaffleFinish()
        {
            float beatInterval = 60f / bpm;

            for (int count = 4; count >= 1; count--)
            {
                if ((4 - count) > 0)
                {
                    //countdown.text = $"SPACE {count}";
                }
                else
                {
                    //countdown.text = "";
                }
                yield return new WaitForSeconds(beatInterval);
            }
        }

        IEnumerator CountBeatsPancake()
        {
            float beatInterval = 60f / bpm;

            for (int count = 4; count >= 1; count--)
            {
                if ((4 - count) > 0)
                {
                    //pancakeCountdown.text = $"F {count}";
                }
                else
                {
                    //pancakeCountdown.text = "";
                }
                yield return new WaitForSeconds(beatInterval);
            }
        }

    }

    public PancakeParticleObject GetPancakeParticleObject() { return pancakeParticleObject; }
    public void SetPancakeParticleObject(PancakeParticleObject ppo) { pancakeParticleObject = ppo; }
}
