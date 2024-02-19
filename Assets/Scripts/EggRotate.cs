using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggRotate : MonoBehaviour
{
    public Animator anim;
    void Update()
    {
        bool coffeeCamActive = Stations.Coffee.GetAssociatedCamera().gameObject.activeSelf;

        if (coffeeCamActive && anim.GetBool("CamActive"))
        {
            //anim.SetBool("CamActive", false);
        }
        else if (!coffeeCamActive && !anim.GetBool("CamActive"))
        {
            //anim.SetBool("CamActive", true);
        }
    }

}
