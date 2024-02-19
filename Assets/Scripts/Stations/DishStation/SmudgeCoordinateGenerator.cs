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
        coordinateGenerator.coordinates = new List<Vector2>();

        for (int i = 0; i < smudgeCount; i++)
        {
            // Square stuff because otherwise they're going to be denser towards the middle.
            float radius = Mathf.Sqrt(Random.Range(0, Mathf.Pow(maxRadius, 2)));
            float angle = Random.Range(0F, 360F);

            Vector3 target3 = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Vector2 target = new Vector2(target3.x, target3.y);
            coordinateGenerator.coordinates.Add(target);
        }

        coordinateGenerator.GenerateShape();
    }
}
