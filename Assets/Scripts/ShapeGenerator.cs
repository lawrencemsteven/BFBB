using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShapeGenerator : MonoBehaviour
{
    public int segments = 50;
    public float radiusIncrease = 0.0001f;
    public float rotationSpeed = 5f;
    public float colliderRadius = 0.01f;
    public IsShapeCovered isc;
    public int currentMarkerPos = -1;

    public GameObject markerPrefab, trail;
    public float markerSize = 0.1f;
    public float bpm = 135f;
    public float headSize = 0.5f;
    public float earSize = 0.3f;
    public TextMeshProUGUI pancakeCountdown;
    public int shapeInput;
    private LineRenderer lineRenderer;
    private GameObject marker;
    private float timeToComplete;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        timeToComplete = (8 / bpm) * 60;
        if (shapeInput == 0)
        {
            GenerateSpiral();
        }
        else if (shapeInput ==1)
        {
            GenerateHeartShape();
        }
        CreateMarker();
        //isc.SetPoints();
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
            if (i % 4 == 0)
            {
                AddSphereCollider(new Vector3(x + transform.position.x, 0f + transform.position.y, z + +transform.position.z));

            }
        }
    }

    void GenerateHeartShape()
    {
        lineRenderer.positionCount = segments;

        float angle = 0f;
        float step = (2f * Mathf.PI) / segments;
        float radius = 0.01f;

        for (int i = 0; i < segments; i++)
        {
            float x = 16f * Mathf.Pow(Mathf.Sin(angle), 3);
            float y = 0f;
            float z = -(13f * Mathf.Cos(angle) - 5f * Mathf.Cos(2 * angle) - 2f * Mathf.Cos(3 * angle) - Mathf.Cos(4 * angle));

            x *= radius;
            z *= radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
            if (i % 4 == 0)
            {
                AddSphereCollider(new Vector3(x + transform.position.x, y + transform.position.y, z + +transform.position.z));
                
            }

            angle += step;
        }
    }
/*
    void GenerateStarShape()
    {
        int numPoints = 5;
        int segments = 10; // Number of line segments between each point
        lineRenderer.positionCount = segments * numPoints;

        float angleIncrement = (2f * Mathf.PI) / segments;
        float outerRadius = 0.05f;
        float innerRadius = outerRadius / 2f;

        float angle = 0f;

        for (int i = 0; i < segments * numPoints; i += 2)
        {
            float currentAngle = angle + (i / 2 % numPoints) * (2f * Mathf.PI / numPoints);

            float outerX = Mathf.Cos(currentAngle) * outerRadius;
            float outerZ = Mathf.Sin(currentAngle) * outerRadius;
            float innerX = Mathf.Cos(currentAngle + angleIncrement / 2f) * innerRadius;
            float innerZ = Mathf.Sin(currentAngle + angleIncrement / 2f) * innerRadius;

            lineRenderer.SetPosition(i, new Vector3(outerX, 0, outerZ));
            lineRenderer.SetPosition(i + 1, new Vector3(innerX, 0, innerZ));
        }

    }
*/



    void CreateMarker()
    {
        marker = Instantiate(markerPrefab, transform);
        UpdateMarkerPosition(0);
    }

    public void UpdateMarkerPosition(float elapsedTime)
    {
        float progress = elapsedTime / timeToComplete;
        int index = Mathf.FloorToInt(progress * (segments - 1));

        Vector3 spiralPoint = lineRenderer.GetPosition(index);
        marker.transform.localPosition = new Vector3(spiralPoint.x, marker.transform.localPosition.y, spiralPoint.z);
    }

    void AddSphereCollider(Vector3 position)
    {
        GameObject colliderObject = new GameObject("Collider");
        colliderObject.transform.position = position;
        colliderObject.transform.SetParent(transform);
        colliderObject.AddComponent<SphereCollider>().radius = 0.01f;
        colliderObject.AddComponent<PointIsHittable>();
    }

    void Update()
    {
        
    }

    IEnumerator CountBeatsPancake()
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
            
            Debug.Log($"Beat {count}");
            yield return new WaitForSeconds(beatInterval);
        }

        Debug.Log("Counting finished!");
    }
}
