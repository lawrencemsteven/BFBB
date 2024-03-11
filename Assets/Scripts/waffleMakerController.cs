using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waffleMakerController : MonoBehaviour
{
    public Animator anim;
    public GameObject waffleLid;
    public GameObject wholeMachine;
    public GameObject camera1;
    public bool isOpen;
    public bool isClose;

    // amount that the user is allowed to miss the beat by and still score a point;
    public float acceptableOffset;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        acceptableOffset = 0.2f;
    }

    //&& (waffleLid.transform.rotation.eulerAngles.x == 0)
    //&& (wholeMachine.transform.rotation.eulerAngles.z == 180))
    // Update is called once per frame
    void Update()
    {
        /*
        Old Waffle Iron Controls Before Animation Rework
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
        */
        if (Input.GetKeyDown(KeyCode.W)) {
                anim.Play("DisconnectedWaffleMakerOpen");
                onPress();
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
                anim.Play("DisconnectedWaffleMakerClose");
                onPress();
        }
        else if (Input.GetKeyDown(KeyCode.A) && true) {
                anim.Play("NewWaffleFlip");
                onPress();
        }
        else if (Input.GetKeyDown(KeyCode.D) && true) {
                anim.Play("NewWaffleFlipBack");
                onPress();
        }
    }

    private void onPress() {
        if (Composer.Instance.testInput(acceptableOffset)) {
                    Debug.Log("Waffle Iron on beat");
                }
        else {
            Debug.Log("Waffle Iron off beat");
        }
    }
}
