using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    private List<GameObject> smudges = new List<GameObject>(); // A list to hold all the smudge objects
    public AudioSource hihat;
    public float smudgeQualityCost = 0.2f;

    private GameObject plate;
    private GameObject bar;
    private GameObject sponge;
    private float spongeX;
    private float spongeY;
    private float barY;
    private Bounds plateBounds;
    private Vector3 topPosition;
    private bool[] smudgeHitStatus; // An array to keep track of whether each smudge has been hit
    private int closestSmudgeIndex;
    private float previousMouseX;
    public HiHatFmod hihatFmod;
    private int smudgesRemaining;

    private int lastSmudgeIndex;

    private void Start()
    {
        if(GameObject.Find("HiHat").GetComponent<HiHatFmod>())
        {
            hihatFmod = GameObject.Find("HiHat").GetComponent<HiHatFmod>();
        }

        plate = DishStationManager.Instance.GetPlate();
        bar = DishStationManager.Instance.GetBar();
        sponge = DishStationManager.Instance.GetSponge();
        
        foreach (Transform smudge in transform)
        {
            smudges.Add(smudge.gameObject);
        }

        smudgeHitStatus = new bool[smudges.Count];
        plateBounds = plate.GetComponent<Renderer>().bounds;
        topPosition = plateBounds.max;
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
        barY = bar.transform.position.y;
        float currentMouseX = Input.mousePosition.x;
        float mouseXDelta = currentMouseX - previousMouseX;
        float previousScore = GlobalVariables.score;
        bool isHit = false;

        for (int i = 0; i < smudges.Count; i++)
        {
            Renderer smudgeRenderer = smudges[i].transform.GetChild(0).GetComponent<Renderer>();
            Collider smudgeCollider = smudges[i].GetComponent<Collider>();
            Bounds smudgeBounds = smudgeCollider.bounds;

            if (barY >= smudgeBounds.min.y && barY <= smudgeBounds.max.y)
            {
                if ((spongeX >= smudgeBounds.min.x && spongeX <= smudgeBounds.max.x) && (spongeY >= smudgeBounds.min.y && spongeY <= smudgeBounds.max.y) &&
                    smudgeRenderer.enabled && !smudgeHitStatus[i])
                {
                    closestSmudgeIndex = i;
                    isHit = true;
                }
            }
        }

        if ((mouseXDelta > 0 || mouseXDelta < 0) && isHit)
        {
            //hihat.Play();
            hihatFmod.PlayHiHat();
            GlobalVariables.missCounter = 0; 
            GlobalVariables.score += 1;
            GlobalVariables.streak += 1;
            GlobalVariables.notesHit += 1;
            smudgeHitStatus[closestSmudgeIndex] = true; 
            SetSmudgeInvisible(closestSmudgeIndex);
            isHit = false;
            smudgesRemaining -= 1;
        }

        if (lastSmudgeIndex != closestSmudgeIndex) {
            if (!smudgeHitStatus[lastSmudgeIndex]) {
                GlobalVariables.missCounter += 1;
                GlobalVariables.notesMissed += 1;
                GlobalVariables.streak = 0;
                string missText = "Miss: " + lastSmudgeIndex.ToString();
                Debug.Log("Miss!");
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
                    //Debug.Log(GlobalVariables.missCounter);
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

    private void SetSmudgeInvisible(int index)
    {
        smudges[index].transform.GetChild(0).GetComponent<Renderer>().enabled = false;
    }

    private void SetSmudgeVisible(int index)
    {
        smudges[index].transform.GetChild(0).GetComponent<Renderer>().enabled = true;
    }

    public void SetAllSmudgesVisibleAndActive()
    {
        for (int i = 0; i < smudges.Count; i++)
        {
            smudgeHitStatus[i] = false; 
            smudges[i].transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            
        }
        smudgesRemaining = smudges.Count;
    }

    public ReservoirPlate CreateReservoirPlate()
    {
        Debug.Log(smudgesRemaining);
        float quality = 1.0f - (smudgesRemaining * smudgeQualityCost);
        return new ReservoirPlate(quality);
    }
}