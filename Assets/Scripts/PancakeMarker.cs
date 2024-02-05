using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PancakeMarker : MonoBehaviour
{
    private int segments = 50;

    public GameObject markerPrefab;
    public float markerSize = 0.1f;
    public float bpm = 135f;

    private LineRenderer lineRenderer;
    private GameObject marker;
    private float elapsedTime, timeToComplete;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        timeToComplete = (8 / bpm) * 60;
        lineRenderer.positionCount = segments;
        CreateMarker();
        GlobalVariables.pancakeStationActive = true;
    }

    void CreateMarker()
    {
        marker = Instantiate(markerPrefab, transform);
        UpdateMarkerPosition();
    }

    void UpdateMarkerPosition()
    {
        float progress = elapsedTime / timeToComplete;
        int index = Mathf.FloorToInt(progress * (segments - 1));

        Vector3 spiralPoint = lineRenderer.GetPosition(index);
        marker.transform.localPosition = new Vector3(spiralPoint.x, marker.transform.localPosition.y, spiralPoint.z);
    }

    public Vector3 getMarkerPosition()
    {
        return marker.transform.localPosition;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeToComplete)
        {
            elapsedTime = 0f;
        }
        else
        {
            UpdateMarkerPosition();
        }
    }
}
