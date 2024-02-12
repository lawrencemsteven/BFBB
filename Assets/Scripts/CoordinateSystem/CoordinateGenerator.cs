using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private float extent;
    public List<Vector2> coordinates;

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
            newPoint.GetComponent<CoordinateCollider>().index = i;
            i++;
        }
    }
}
