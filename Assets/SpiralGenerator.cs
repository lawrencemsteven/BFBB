using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SpiralGenerator : MonoBehaviour
{
    public int segments = 50;
    public float radiusIncrease = 0.0001f;
    public float rotationSpeed = 5f;
    public float colliderRadius = .01f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        GenerateSpiral();
    }

    void GenerateSpiral()
    {
        lineRenderer.positionCount = segments;

        float angle = 0f;
        float radius = .05f;
        for (int i = 0; i < segments; i++)
        {
            float x = (Mathf.Cos(angle) * radius);
            float z = (Mathf.Sin(angle) * radius);
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            
            angle += Mathf.Deg2Rad * rotationSpeed;
            radius += radiusIncrease;
        }
    }
}
