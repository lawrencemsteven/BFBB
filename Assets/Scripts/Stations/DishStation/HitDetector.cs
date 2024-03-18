using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    private List<GameObject> smudges = new List<GameObject>(); // A list to hold all the smudge objects
    public AudioSource hihat;
    public float smudgeQualityCost = 0.2f;
    private GameObject sponge;
    private float spongeX;
    private float spongeY;
    private bool[] smudgeHitStatus; // An array to keep track of whether each smudge has been hit
    private int closestSmudgeIndex;
    private float previousMouseX;
    public HiHatFmod hihatFmod;
    private int smudgesRemaining;

    private int lastSmudgeIndex;
    private ScoreAndStreakManager scoreManager;

    private void Start()
    {
        scoreManager = GetComponent<ScoreAndStreakManager>();
        if(GameObject.Find("HiHat").GetComponent<HiHatFmod>())
        {
            hihatFmod = GameObject.Find("HiHat").GetComponent<HiHatFmod>();
        }

        sponge = Stations.Dish.GetSponge();
        
        foreach (Transform smudge in transform)
        {
            smudges.Add(smudge.gameObject);
        }

        smudgeHitStatus = new bool[smudges.Count];
        previousMouseX = Input.mousePosition.x;
        lastSmudgeIndex = 0;
        closestSmudgeIndex = 0;
        smudgesRemaining = smudges.Count;

        for (int i = 0; i < smudges.Count; i++)
        {
            smudgeHitStatus[i] = false; 
        }
    }

    private void Update()
    {
        spongeX = sponge.transform.position.x;
        spongeY = sponge.transform.position.y;
        float currentMouseX = Input.mousePosition.x;
        float mouseXDelta = currentMouseX - previousMouseX;
        bool isHit = false;

        for (int i = 0; i < smudges.Count; i++)
        {
            Renderer smudgeRenderer = smudges[i].transform.GetChild(0).GetComponent<Renderer>();
            Collider smudgeCollider = smudges[i].GetComponent<Collider>();
            Bounds smudgeBounds = smudgeCollider.bounds;

            if ((spongeX >= smudgeBounds.min.x && spongeX <= smudgeBounds.max.x) && (spongeY >= smudgeBounds.min.y && spongeY <= smudgeBounds.max.y) &&
                smudgeRenderer.enabled && !smudgeHitStatus[i])
            {
                closestSmudgeIndex = i;
                isHit = true;
            }
        }

        if ((mouseXDelta > 0 || mouseXDelta < 0) && isHit)
        {
            //hihat.Play();
            hihatFmod.PlayHiHat();
            scoreManager.scoreUpdate(1);
            GlobalVariables.notesHit += 1;
            smudgeHitStatus[closestSmudgeIndex] = true; 
            //SetSmudgeInvisible(closestSmudgeIndex);
            smudgesRemaining -= 1;
        }

        if (lastSmudgeIndex != closestSmudgeIndex) {
            if (!smudgeHitStatus[lastSmudgeIndex]) {
                GlobalVariables.missCounter += 1;
                GlobalVariables.notesMissed += 1;
                scoreManager.resetStreak();
            }
            lastSmudgeIndex = closestSmudgeIndex;
        }

        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
         //   CameraShake.Instance.Shake();

        //}

        /*if (Mathf.Approximately(barY, topPosition.y))
        {
            for (int i = 0; i < smudges.Count; i++)
            {
                //if (smudgeRenderers[i].enabled)
                //{
                    //GlobalVariables.missCounter += 1;
                    //GlobalVariables.notesMissed += 1;
                //}

                if (!smudges[i].transform.GetChild(0).GetComponent<Renderer>().enabled)
                {
                    smudgeHitStatus[i] = false; 
                    SetSmudgeVisible(i);
                }

            }
            smudgesRemaining = smudges.Count;
        }*/
        
        previousMouseX = currentMouseX;
    }
}