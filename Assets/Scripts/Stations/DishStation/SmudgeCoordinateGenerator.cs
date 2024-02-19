using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoordinateGenerator))]
public class SmudgeCoordinateGenerator : MonoBehaviour
{
    [SerializeField] private int minSmudges, maxSmudges;
    [SerializeField] private float maxRadius;

    private CoordinateGenerator coordinateGenerator;

    void Awake()
    {
        coordinateGenerator = GetComponent<CoordinateGenerator>();
        SongInfo.Instance.onMeasure.AddListener(NewPlate);
    }

    public void NewPlate()
    {
        int smudgeCount = Random.Range(minSmudges, maxSmudges + 1);
        for (int i = 0; i < smudgeCount; i++)
        {
            // Square stuff because otherwise they're going to be denser towards the middle.
            float radius = Mathf.Sqrt(Random.Range(0, Mathf.Pow(maxRadius, 2)));
        }
    }
}
