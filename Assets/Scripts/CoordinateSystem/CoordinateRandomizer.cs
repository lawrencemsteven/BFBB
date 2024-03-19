using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateRandomizer : MonoBehaviour
{
    [SerializeField] private CoordinateGenerator targetGenerator;
    private List<CoordinateOption> shapes = new();

    public void Awake()
    {
        targetGenerator.afterShapeGenerated.AddListener(ChooseNewShape);
        foreach (CoordinateOption shape in GetComponents<CoordinateOption>())
        {
            shapes.Add(shape);
        }
    }

    public void ChooseNewShape()
    {
        CoordinateOption shape = shapes[Random.Range(0, shapes.Count)];
        targetGenerator.coordinates = shape.coordinates;
    }
}
