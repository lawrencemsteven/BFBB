using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PancakeStation : Station
{

    public Animator waffleMakerAnim, pancakeAnim;
    public float bpm = 135f;
    public Vector3 mousePosition;
    public GameObject waffle, waffleBatter;
    private int flashCount = 1, countdownNum = 4;
    private float timeToStartWaffles, elapsedTime, totalElapsed, timeToFlash, timeToFlipPancake, timeToFlipWaffle, timeToFinishPancake, timeToFinishWaffle;
    private bool waffleMakerOpen = false, circleIsVisible = false, pancakeFlipped = false, waffleFlipped = false, flipWaffleTimerStarted = false, flipPancakeTimerStarted = false, finishWaffleTimerStarted = false, finishPancakeTimerStarted = false;
    public TextMeshProUGUI countdown, pancakeCountdown;
    private PancakeParticleObject pancakeParticleObject;
    public LineManager lineManager2, lineManager3;
    private bool readyForNewMeasure;
    public EnableBatterArea enableBatterArea;

    private bool pathToScore;
    private ScoreAndStreakManager scoreManager;


    // Start is called before the first frame update
    public override void Initialize()
    {
        scoreManager = GetComponent<ScoreAndStreakManager>();
        pathToScore = false;
        timeToStartWaffles = (16 / bpm) * 60;
        timeToFlipPancake = (26 / bpm) * 60;
        timeToFlipWaffle = (30 / bpm) * 60;
        timeToFinishPancake = (34 / bpm) * 60;
        timeToFinishWaffle = (38 / bpm) * 60;
        timeToFlash = (1 / (bpm)) * 60;

        Composer.Instance.onBeat.AddListener(NewBeat);
        Composer.Instance.onMeasure.AddListener(NewMeasure);
        NewMeasure();
    }

    public void NewBeat()
    {
        if (pathToScore) {
            scoreManager.scoreUpdate(1);
            pathToScore = false;
        }
    }

    public void NewMeasure()
    {
        if (readyForNewMeasure)
        {
            readyForNewMeasure = false;
            lineManager.DrawLine();
            lineManager2.DrawLine();
            lineManager3.DrawLine();
        }
        else
        {
            readyForNewMeasure = true;
        }
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

        if ((timeToFlipPancake - totalElapsed) <= ((4 / bpm) * 60) && (!flipPancakeTimerStarted))
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

        if (Input.GetMouseButton(0) && Stations.Pancake.IsRunning())
        {
            List<Vector2> markerSpaces = new List<Vector2>();
            markerSpaces.Add(lineManager.GetMarkerSpace());
            markerSpaces.Add(lineManager2.GetMarkerSpace());
            markerSpaces.Add(lineManager3.GetMarkerSpace());
            Vector2 offset;
            string currentArea = enableBatterArea.getCurrentPancakeArea().name;
            offset = markerSpaces[0];
            if (currentArea == "PancakeAreaDetector2")
            {
                offset = markerSpaces[1];
            }
            else if (currentArea == "PancakeAreaDetector3")
            {
                offset = markerSpaces[2];
            }
            Station.HandlePathUpdate(offset);
        }
        else if (Input.GetMouseButtonUp(0) && Stations.Pancake.IsRunning()) {
            Composer.Instance.PitchChange(0);
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

    public override void pathUpdate(Vector2 offset)
    {
        float distance = offset.magnitude;

        if (distance < distanceMinimum)
        {
            distance = 0;
            pathToScore = true;
        }
        
        else {
            scoreManager.resetStreak();
        }

        //Composer.Instance.VolumeChange(1, volume);
        Composer.Instance.PitchChange(-distance);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Composer.Instance.PitchChange(0);
    }

    public PancakeParticleObject GetPancakeParticleObject() { return pancakeParticleObject; }
    public void SetPancakeParticleObject(PancakeParticleObject ppo) { pancakeParticleObject = ppo; }
}