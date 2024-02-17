using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHitDetector : MonoBehaviour
{
    public SmudgeCollision[] smudgeColliders;


    public int i = 0;

    //public Renderer[] smudgeRenderers;
    public AudioSource hihat;
    public UpDownMovement bar;
    public HiHatFmod hihatFmod;


    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("HiHat").GetComponent<HiHatFmod>())
        {
            hihatFmod = GameObject.Find("HiHat").GetComponent<HiHatFmod>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(i < smudgeColliders.Length)
        {
            if(smudgeColliders[i].barColliding && smudgeColliders[i].spongeColliding)
            {
                Hit();
                i++;
            }
            else if(smudgeColliders[i].barExit)
            {
                Miss();
                i++;
            }
        }

        if(bar.reset)
        {
            
            for(int j = 0; j < smudgeColliders.Length; j++)
            {
                smudgeColliders[j].Reset();
                //smudgeColliders[j].barExit = false;
                SetSmudgeVisible(j);
            }
            i = 0;
            bar.reset = false;
            //print("BAR RESET BAR RESET BAR RESET BAR RESET BAR RESET BAR RESET BAR RESET BAR RESET BAR RESET BAR RESET");
        }
    }





    void Hit()
    {
        //hihat.Play();
        hihatFmod.PlayHiHat();
        GlobalVariables.missCounter = 0; 
        GlobalVariables.score += 1;
        GlobalVariables.streak += 1;
        GlobalVariables.notesHit += 1;
        SetSmudgeInvisible(i);
    }

    void Miss()
    {
        GlobalVariables.missCounter += 1;
        GlobalVariables.notesMissed += 1;
        GlobalVariables.streak = 0;
    }

    private void SetSmudgeInvisible(int index)
    {
        //smudgeRenderers[index].enabled = false;
        smudgeColliders[index].Reset();
        smudgeColliders[index].gameObject.SetActive(false);
    }

    private void SetSmudgeVisible(int index)
    {
        //smudgeRenderers[index].enabled = true;
        
        smudgeColliders[index].gameObject.SetActive(true);
    }

}
