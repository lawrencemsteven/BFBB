using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoordinateGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private float extent;
    protected List<CoordinateCollider> points = new List<CoordinateCollider>();
    public List<Vector2> coordinates;
    [SerializeField] protected Transform pointSpawnArea;

    public UnityEvent afterShapeGenerated = new();

    void Start()
    {
        GenerateShape();
    }

    public void GenerateShape()
    {
        RemoveShape();

        int i = 0;
        foreach (Vector2 point in coordinates)
        {
            GameObject newPoint = Instantiate(pointPrefab, pointSpawnArea);
            Vector3 position = extent * new Vector3(-point.x, 0, point.y);
            newPoint.transform.localPosition = position;
            CoordinateCollider coordCollider = newPoint.GetComponent<CoordinateCollider>();
            coordCollider.index = i;
            points.Add(coordCollider);
            i++;
        }

        afterShapeGenerated?.Invoke();
    }

    public void RemoveShape()
    {
        foreach (CoordinateCollider point in points)
        {
            Destroy(point.gameObject);
        }
        points = new List<CoordinateCollider>();
    }

    public List<CoordinateCollider> GetColliders()
    {
        return points;
    }

    public CoordinateCollider GetCollider(int index)
    {
        return points[index];
    }
}
