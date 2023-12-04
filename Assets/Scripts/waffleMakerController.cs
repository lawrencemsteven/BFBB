using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waffleMakerController : MonoBehaviour
{
    public Animator anim;
    public GameObject waffleLid;
    public GameObject wholeMachine;
    public GameObject camera1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //&& (waffleLid.transform.rotation.eulerAngles.x == 0)
    //&& (wholeMachine.transform.rotation.eulerAngles.z == 180))
    // Update is called once per frame
    void Update()
    {
        if (camera1.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GlobalVariables.score += 1;
                anim.Play("NewWaffleIronClose");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                GlobalVariables.score += 1;
                anim.Play("NewWaffleFlipBack");
            }
        }
    }
}
