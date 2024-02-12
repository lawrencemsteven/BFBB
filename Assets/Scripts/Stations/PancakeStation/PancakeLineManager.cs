using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoordinateGenerator))]
[RequireComponent(typeof(LineRenderer))]
public class PancakeLineManager : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;

    private CoordinateGenerator coordinateGenerator;
    private LineRenderer lineRenderer;
    private GameObject marker;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        coordinateGenerator = GetComponent<CoordinateGenerator>();
        coordinateGenerator.afterShapeGenerated.AddListener(DrawLine);
    }

    void Start()
    {
        lineRenderer.useWorldSpace = false;
    }

    public void DrawLine()
    {
        List<CoordinateCollider> points = coordinateGenerator.GetColliders();
        lineRenderer.positionCount = points.Count;
        int i = 0;
        foreach (CoordinateCollider point in points)
        {
            Vector3 position = point.transform.localPosition;
            lineRenderer.SetPosition(i, position);
            i++;
        }
    }
}
