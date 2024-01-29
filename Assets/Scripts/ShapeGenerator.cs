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
            GenerateMickeyMouse();
        }
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

    void GenerateMickeyMouse()
    {
        /*
        lineRenderer.positionCount = segments * 3; // Mickey Mouse has three parts: head and two ears
        float radius = 0.05f;
        float angle = 0f;
        for (int i = 0; i < segments; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));

            angle += Mathf.PI * 2f / segments;
        }

        for (int i = 0; i < segments; i++)
        {
            float x = Mathf.Cos(angle) * headSize;
            float z = Mathf.Sin(angle) * headSize;
            lineRenderer.SetPosition(i + segments, new Vector3(x, 0, z));

            angle += Mathf.PI * 2f / segments;
        }

        for (int i = 0; i < segments; i++)
        {
            float x = Mathf.Cos(angle) * earSize;
            float z = Mathf.Sin(angle) * earSize;
            lineRenderer.SetPosition(i + segments * 2, new Vector3(x - 0.3f, 0, z + 0.3f)); // Adjust position for left ear

            angle += Mathf.PI * 2f / segments;
        }
        
        for (int i = 0; i < segments; i++)
        {
            float x = Mathf.Cos(angle) * earSize;
            float y = Mathf.Sin(angle) * earSize;
            lineRenderer.SetPosition(i + segments * 3, new Vector3(x + 0.3f, y + 0.3f, 0)); // Adjust position for right ear

            angle += Mathf.PI * 2f / segments;
        }
        */
    }

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
