using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggRotate : MonoBehaviour
{
    public GameObject camera;
    public GameObject camera2;
    public GameObject egg;
    public Animator anim;

    void Update()
    {
        if (camera.activeSelf)
        {
            anim.SetBool("CamActive", true);
        }
        else if (camera2.activeSelf)
        {
            anim.SetBool("CamActive", false);
        }
    }

}
