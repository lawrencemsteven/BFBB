using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PancakeScorer : MonoBehaviour
{
    public int segments = 50;

    public GameObject markerPrefab, shape;
    public float markerSize = 0.1f;
    public float bpm = 135f;

    private LineRenderer lineRenderer;
    private GameObject marker;
    private float elapsedTime, timeToComplete, timeToFlash;
    private bool markerVisible = false;
    private int startCount = 1;
    public IsShapeCovered isShapeCovered;

    void Start()
    {
        lineRenderer = shape.GetComponent<LineRenderer>();
        timeToComplete = (8 / bpm) * 60;
        timeToFlash = (1 / bpm) * 60;
        lineRenderer.positionCount = segments;
        CreateMarker();
    }

    void CreateMarker()
    {
        marker = Instantiate(markerPrefab, shape.transform);
        UpdateMarkerPosition();
    }

    void UpdateMarkerPosition()
    {
        float progress = elapsedTime / timeToComplete;
        int index = Mathf.FloorToInt(progress * (segments - 1));

        Vector3 spiralPoint = lineRenderer.GetPosition(index);
        marker.transform.localPosition = new Vector3(spiralPoint.x, marker.transform.localPosition.y, spiralPoint.z);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if ((elapsedTime >= timeToFlash) && (startCount <= 6))
        {
            elapsedTime = 0f;
            markerVisible = !markerVisible;
            marker.SetActive(markerVisible);
            startCount++;
        }

        if (startCount > 6)
        {
            lineRenderer = shape.GetComponent<LineRenderer>();
            lineRenderer.positionCount = segments;
            if (!markerVisible)
            {
                marker.SetActive(true);
            }
            if (elapsedTime >= timeToComplete)
            {
                if (isShapeCovered.GetNumOfColliders() < 3)
                {

                }
                elapsedTime = 0f;
            }
            else
            {
                UpdateMarkerPosition();
            }
        }
    }
}
