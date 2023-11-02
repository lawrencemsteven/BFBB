using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public GameObject[] smudges; // An array to hold all the smudge objects
    public GameObject bar;
    public GameObject plate;
    public GameObject sponge;

    private float spongeX;
    private float barY;
    private Bounds[] objectBounds; // An array to hold the bounds of smudge objects
    private Bounds plateBounds;
    private Vector3 topPosition;
    private bool[] smudgeHitStatus; // An array to keep track of whether each smudge has been hit
    private Renderer[] smudgeRenderers; // An array to hold the renderers of smudge objects
    private int closestSmudgeIndex;
    private float previousMouseX;

    private void Start()
    {
        objectBounds = new Bounds[smudges.Length];
        smudgeRenderers = new Renderer[smudges.Length];
        smudgeHitStatus = new bool[smudges.Length];
        plateBounds = plate.GetComponent<Renderer>().bounds;
        topPosition = plateBounds.max;
        previousMouseX = Input.mousePosition.x;

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
        barY = bar.transform.position.y;
        float minDistance = float.MaxValue;
        float currentMouseX = Input.mousePosition.x;
        float mouseXDelta = currentMouseX - previousMouseX;
        float previousScore = GlobalVariables.score;

        for (int i = 0; i < smudges.Length; i++)
        {
            if (barY >= objectBounds[i].min.y && barY <= objectBounds[i].max.y)
            {
                float distance = Mathf.Abs(barY - (objectBounds[i].min.y + objectBounds[i].max.y) / 2);

                if ((spongeX >= objectBounds[i].min.x && spongeX <= objectBounds[i].max.x) && distance < minDistance &&
                    smudgeRenderers[i].enabled && !smudgeHitStatus[i])
                {
                    closestSmudgeIndex = i; 
                    minDistance = distance; 
                }
            }
        }

        if ((mouseXDelta > 0 || mouseXDelta < 0) && minDistance != float.MaxValue )
        {
            GlobalVariables.missCounter = 0; 
            GlobalVariables.score += 1;
            GlobalVariables.notesHit += 1;
            smudgeHitStatus[closestSmudgeIndex] = true; 
            SetSmudgeInvisible(closestSmudgeIndex); 
        }

        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
         //   CameraShake.Instance.Shake();

        //}


        if (Mathf.Approximately(barY, topPosition.y))
        {
            for (int i = 0; i < smudges.Length; i++)
            {
                if (smudgeRenderers[i].enabled)
                {
                    GlobalVariables.missCounter += 1;
                    GlobalVariables.notesMissed += 1;
                    Debug.Log(GlobalVariables.missCounter);
                }

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
}