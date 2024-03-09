using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingCoordinateGenerator : CoordinateGenerator
{
    [SerializeField] private int lines = 4;
    [SerializeField] private float maxRadius;
    private int pointsRemaining;
    private List<bool> collidedPoints = new List<bool>();

    public void NewPlate()
    {
        int pointCount = lines + 1;
        coordinates = new List<Vector2>();
        collidedPoints.Clear();

        for (int i = 0; i < pointCount; i++)
        {
            // Square stuff because otherwise they're going to be denser towards the middle.
            float radius = Mathf.Sqrt(Random.Range(0, Mathf.Pow(maxRadius, 2)));
            float angle = Random.Range(0F, 360F);

            Vector3 target3 = Quaternion.Euler(0, 0, angle) * (radius * Vector3.up);
            Vector2 target = new Vector2(target3.x, target3.y);
            coordinates.Add(target);

            collidedPoints.Add(false);
        }
    }

    public void HandleCollision(int index, bool early)
    {
        if (!collidedPoints[index])
        {
            collidedPoints[index] = true;
        }
    }
}
