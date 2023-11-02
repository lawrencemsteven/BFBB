using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipController : MonoBehaviour
{
    public Animator anim;
    public GameObject camera1;
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
                anim.Play("WaffleFlip");
            }
        }
    }
}
