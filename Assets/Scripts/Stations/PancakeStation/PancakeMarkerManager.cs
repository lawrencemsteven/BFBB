using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PancakeMarkerManager : MonoBehaviour
{
    /*public int segments = 50;
    public float radiusIncrease = 0.0001f;
    public float rotationSpeed = 5f;
    public float colliderRadius = 0.01f;

    public GameObject pancake, trail;
    public float markerSize = 0.1f;
    public float bpm = 135f;
    public TextMeshProUGUI pancakeCountdown;

    private LineRenderer lineRenderer;
    private GameObject marker;
    private float elapsedTime, elapsedTimeTotal, timeToComplete, timeToFlash, timeToStart;
    private bool markerVisible = false, startSpiraling = false, spiralCountdownStarted = false;
    private int startCount = 1;
    private IsShapeCovered isShapeCovered;*/

    public GameObject pancake;
    public TextMeshProUGUI pancakeCountdown;
    public float bpm = 135f;
    public int segments;

    [SerializeField] private IsShapeCovered isShapeCovered;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject markerPrefab;
    
    private float elapsedTime, elapsedTimeTotal, timeToComplete, timeToFlash, timeToStart;
    private bool countdownStarted = false;
    private GameObject marker;
    private bool startShape = false;

    void Start()
    {
        timeToComplete = (8 / bpm) * 60;
        timeToStart = (8 / bpm) * 60;
        CreateMarker();
    }

    void CreateMarker()
    {
        marker = Instantiate(markerPrefab, transform);
//        UpdateMarkerPosition();
    }

    void UpdateMarkerPosition()
    {
        float progress = elapsedTime / timeToComplete;
        int index = Mathf.FloorToInt(progress * (segments - 1));

        Vector3 targetPoint = lineRenderer.GetPosition(index);
        marker.transform.localPosition = new Vector3(targetPoint.x, marker.transform.localPosition.y, targetPoint.z);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (((timeToStart - elapsedTime) <= (4 / bpm) * 60) && (!countdownStarted))
        {
            countdownStarted = true;
            StartCoroutine(CountBeatsPancake());
        }
        if ((elapsedTime >= timeToStart) && !startShape)
        {
            startShape = true;
            elapsedTime = 0;
        }
        if (startShape)
        {
            if (elapsedTime >= timeToComplete)
            {
                if (isShapeCovered.GetNumOfColliders() < 3)
                {
                    pancake.SetActive(true);

                }
                
                //marker.SetActive(false);
                //gameObject.SetActive(false);
            }
            else
            {
                UpdateMarkerPosition();
            }
        }
    }

    private IEnumerator CountBeatsPancake()
    {
        float beatInterval = 60f / bpm;

        for (int count = 4; count >= 1; count--)
        {
            if ((4 - count) > 0)
            {
                pancakeCountdown.text = count.ToString();
            }
            else
            {
                pancakeCountdown.text = "";
            }

            yield return new WaitForSeconds(beatInterval);
        }
    }
}
