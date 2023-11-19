using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipController : MonoBehaviour
{
    
    public GameObject camera1;
    private Animator anim;
    private bool upsideDown = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera1.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GlobalVariables.score += 1;
                FlipPancake();
            }
        }
    }
    void FlipPancake()
    {
        upsideDown = !upsideDown;
        anim.SetTrigger("FlipTrigger");
        anim.SetBool("isFlipped", upsideDown);
    }

}
