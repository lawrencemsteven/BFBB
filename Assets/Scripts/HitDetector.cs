using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public GameObject[] smudges; // An array to hold all the smudge objects
    public GameObject bar;
    public GameObject plate;
    public GameObject sponge;
    public AudioSource hihat;
    public float smudgeQualityCost = 0.2f;

    private float spongeX;
    private float spongeY;
    private float barY;
    private Bounds[] objectBounds; // An array to hold the bounds of smudge objects
    private Bounds plateBounds;
    private Vector3 topPosition;
    private bool[] smudgeHitStatus; // An array to keep track of whether each smudge has been hit
    private Renderer[] smudgeRenderers; // An array to hold the renderers of smudge objects
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
        objectBounds = new Bounds[smudges.Length];
        smudgeRenderers = new Renderer[smudges.Length];
        smudgeHitStatus = new bool[smudges.Length];
        plateBounds = plate.GetComponent<Renderer>().bounds;
        topPosition = plateBounds.max;
        previousMouseX = Input.mousePosition.x;
        lastSmudgeIndex = 0;
        closestSmudgeIndex = 0;
        smudgesRemaining = smudges.Length;

        for (int i = 0; i < smudges.Length; i++)
        {
            objectBounds[i] = smudges[i].GetComponent<Renderer>().bounds;
            smudgeRenderers[i] = smudges[i].GetComponent<Renderer>();
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

        for (int i = 0; i < smudges.Length; i++)
        {
            if (barY >= objectBounds[i].min.y && barY <= objectBounds[i].max.y)
            {
                if ((spongeX >= objectBounds[i].min.x && spongeX <= objectBounds[i].max.x) && (spongeY >= objectBounds[i].min.y && spongeY <= objectBounds[i].max.y) &&
                    smudgeRenderers[i].enabled && !smudgeHitStatus[i])
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


        if (Mathf.Approximately(barY, topPosition.y))
        {
            for (int i = 0; i < smudges.Length; i++)
            {
                //if (smudgeRenderers[i].enabled)
                //{
                    //GlobalVariables.missCounter += 1;
                    //GlobalVariables.notesMissed += 1;
                    //Debug.Log(GlobalVariables.missCounter);
                //}

                if (!smudgeRenderers[i].enabled)
                {
                    smudgeHitStatus[i] = false; 
                    SetSmudgeVisible(i);
                }

            }
        }
        previousMouseX = currentMouseX;
    }

    private void SetSmudgeInvisible(int index)
    {
        smudgeRenderers[index].enabled = false;
    }

    private void SetSmudgeVisible(int index)
    {
        smudgeRenderers[index].enabled = true;
    }

    public ReservoirPlate CreateReservoirPlate()
    {
        float quality = 1.0f - (smudgesRemaining * smudgeQualityCost);
        return new ReservoirPlate(quality);
    }
}