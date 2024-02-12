using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoordinateGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private float extent;
    private List<CoordinateCollider> points = new List<CoordinateCollider>();
    public List<Vector2> coordinates;

    public UnityEvent afterShapeGenerated = new();

    void Start()
    {
        GenerateShape();
    }

    public void GenerateShape()
    {
        int i = 0;
        foreach (Vector2 point in coordinates)
        {
            GameObject newPoint = GameObject.Instantiate(pointPrefab, transform);
            Vector3 position = extent * new Vector3(point.x, 0, point.y);
            newPoint.transform.localPosition = position;
            CoordinateCollider coordCollider = newPoint.GetComponent<CoordinateCollider>();
            coordCollider.index = i;
            points.Add(coordCollider);
            i++;
        }

        afterShapeGenerated?.Invoke();
    }

    public List<CoordinateCollider> GetColliders()
    {
        return points;
    }
}
