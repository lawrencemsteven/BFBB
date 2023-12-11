using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SpiralGenerator : MonoBehaviour
{
    public int segments = 50;
    public float radiusIncrease = 0.0001f;
    public float rotationSpeed = 5f;
    public float colliderRadius = 0.01f;

    public GameObject markerPrefab, pancake, trail;
    public float markerSize = 0.1f;
    public float bpm = 135f; 

    private LineRenderer lineRenderer;
    private GameObject marker;
    private float elapsedTime, elapsedTimeTotal, timeToComplete, timeToFlash, timeToStart;
    private bool markerVisible = false, startSpiraling = false;
    private int startCount = 1;
    private IsShapeCovered isShapeCovered;

    void Start()
    {
        isShapeCovered = trail.GetComponent<IsShapeCovered>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        timeToComplete = (8 / bpm) * 60;
        timeToStart = (8 / bpm) * 60;
        GenerateSpiral();
        CreateMarker();
    }

    void GenerateSpiral()
    {
        lineRenderer.positionCount = segments;

        float angle = 0f;
        float radius = 0.05f;
        for (int i = 0; i < segments; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));

            angle += Mathf.Deg2Rad * rotationSpeed;
            radius += radiusIncrease;
        }
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

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if ((elapsedTime >= timeToStart) && !startSpiraling)
        {
            startSpiraling = true;
            elapsedTime = 0;
        }
        if (startSpiraling)
        {
            if (elapsedTime >= timeToComplete)
            {
                if (isShapeCovered.GetNumOfColliders() < 3)
                {
                    pancake.SetActive(true);
                    
                }
                else
                {

                    Debug.Log(isShapeCovered.GetNumOfColliders());
                }
                marker.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                UpdateMarkerPosition();
            }
        }
    }
}
