using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeShapeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject samplePoint;
    [SerializeField] private IsShapeCovered isShapeCovered;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PancakeMarkerManager markerManager;
    public List<Vector2> coordinates;

    void Start()
    {
        samplePoint.SetActive(false);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        GenerateShape();
        isShapeCovered.SetupColliders();
    }

    public void GenerateShape()
    {
        lineRenderer.positionCount = coordinates.Count;
        int i = 0;
        foreach (Vector2 point in coordinates)
        {
            GameObject newPoint = GameObject.Instantiate(samplePoint, transform);
            Vector3 position = new Vector3(point.x, 0, point.y);
            newPoint.transform.localPosition = position;
            lineRenderer.SetPosition(i, position);
            i++;
        }
        markerManager.segments = coordinates.Count;
    }
}
