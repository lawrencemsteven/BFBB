using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoordinateGenerator))]
public class SmudgeCoordinateGenerator : MonoBehaviour
{
    [SerializeField] private int minSmudges, maxSmudges;
    [SerializeField] private float maxRadius;
    [SerializeField] private float smudgeQualityCost;
    private int smudgesRemaining;
    private List<bool> collidedSmudges = new List<bool>();

    private CoordinateGenerator coordinateGenerator;

    void Awake()
    {
        coordinateGenerator = GetComponent<CoordinateGenerator>();
    }

    public void NewPlate()
    {
        int smudgeCount = Random.Range(minSmudges, maxSmudges + 1);
        coordinateGenerator.coordinates = new List<Vector2>();
        collidedSmudges.Clear();

        for (int i = 0; i < smudgeCount; i++)
        {
            // Square stuff because otherwise they're going to be denser towards the middle.
            float radius = Mathf.Sqrt(Random.Range(0, Mathf.Pow(maxRadius, 2)));
            float angle = Random.Range(0F, 360F);

            Vector3 target3 = Quaternion.Euler(0, 0, angle) * (radius * Vector3.up);
            Vector2 target = new Vector2(target3.x, target3.y);
            coordinateGenerator.coordinates.Add(target);

            collidedSmudges.Add(false);
        }
    }

    public void HandleCollision(int index)
    {
        if (!collidedSmudges[index])
        {
            setSmudgeInvisible(index);
            collidedSmudges[index] = true;
        }
    }

    private void setSmudgeInvisible(int index)
    {
        GetCollider(index).transform.GetChild(0).GetComponent<Renderer>().enabled = false;
    }

    public void SetAllSmudgesVisibleAndActive()
    {
        for (int i = 0; i < GetColliders().Count; i++)
        {
            GetCollider(i).transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            
        }

        smudgesRemaining = GetColliders().Count;
    }

    public ReservoirPlate CreateReservoirPlate()
    {
        float quality = 1.0f - (smudgesRemaining * smudgeQualityCost);
        return new ReservoirPlate(quality);
    }

    public void ClearPlate()
    {
        coordinateGenerator.RemoveShape();
    }

    public CoordinateCollider GetCollider(int index)
    {
        return coordinateGenerator.GetColliders()[index];
    }

    public List<CoordinateCollider> GetColliders()
    {
        return coordinateGenerator.GetColliders();
    }
}
