using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipController : MonoBehaviour
{
    public GameObject camera1;
    public bool startFlip = false;
    private Animator anim;
    private bool flipped = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (camera1.activeSelf)
        {
            /*
            // Check for right click and swipe up
            if (Input.GetMouseButton(1) && Input.GetAxis("Mouse Y") > .5)
            {
                GlobalVariables.score += 1;
                FlipPancake();
            }
            */
        }
    }

    public void FlipPancake()
    {
        flipped = !flipped;
        anim.SetTrigger("FlipTrigger");
        anim.SetBool("isFlipped", flipped);
    }
}