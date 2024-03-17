using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmudgeCoordinateGenerator : CoordinateGenerator
{
    [SerializeField] private int minSmudges, maxSmudges;
    [SerializeField] private float maxRadius;
    [SerializeField] private float smudgeQualityCost;
    [SerializeField] private List<Material> smearMaterials;
    [SerializeField] private List<Material> scrapeMaterials;
    private int smudgesRemaining;
    private List<bool> collidedSmudges = new List<bool>();

    private ScoreAndStreakManager scoreManager;

    void Start() {
        scoreManager = GetComponent<ScoreAndStreakManager>();
    }
    public void NewPlate()
    {
        int smudgeCount = Random.Range(minSmudges, maxSmudges + 1);
        coordinates = new List<Vector2>();
        collidedSmudges.Clear();

        for (int i = 0; i < smudgeCount; i++)
        {
            // Square stuff because otherwise they're going to be denser towards the middle.
            float radius = Mathf.Sqrt(Random.Range(0, Mathf.Pow(maxRadius, 2)));
            float angle = Random.Range(0F, 360F);

            Vector3 target3 = Quaternion.Euler(0, 0, angle) * (radius * Vector3.up);
            Vector2 target = new Vector2(target3.x, target3.y);
            coordinates.Add(target);

            collidedSmudges.Add(false);
        }
    }

    public void HandleCollision(int index, bool early)
    {
        if (!collidedSmudges[index])
        {
            if (early)
            {
                setSmudgeAsSmear(index);
                Debug.Log("streakReset from early hit");
                scoreManager.resetStreak();
                Stations.Dish.GetSoundBytePlayer().PlayEarly();

            }
            else
            {
                setSmudgeInvisible(index);
                scoreManager.scoreUpdate(1);
                Stations.Dish.GetSoundBytePlayer().PlayOnTime();
            }
            collidedSmudges[index] = true;
        }
    }

    private void setSmudgeInvisible(int index)
    {
        points[index].transform.GetChild(0).GetComponent<Renderer>().enabled = false;
    }

    private void setSmudgeAsSmear(int index)
    {
        points[index].transform.GetChild(0).GetComponent<Renderer>().material = smearMaterials[Random.Range(0,smearMaterials.Count)];
    }

    public void SetSmudgeAsScrape(int index)
    {
        if (!collidedSmudges[index])
        {
            points[index].transform.GetChild(0).GetComponent<Renderer>().material = scrapeMaterials[Random.Range(0,scrapeMaterials.Count)];
            Stations.Dish.GetSoundBytePlayer().PlayLate();
            collidedSmudges[index] = true;
        }
    }

    public void SetAllSmudgesVisibleAndActive()
    {
        for (int i = 0; i < points.Count; i++)
        {
            points[i].transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            
        }

        smudgesRemaining = points.Count;
    }

    public ReservoirPlate CreateReservoirPlate()
    {
        float quality = 1.0f - (smudgesRemaining * smudgeQualityCost);
        return new ReservoirPlate(quality);
    }
}
