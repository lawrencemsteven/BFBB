using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMarker : MonoBehaviour
{
    private Animator anim;
    public GameObject marker;
    public GameObject[] smudges;
    public static bool activate = false;
    void Start()
    {
    }
    void Update()
    {
        if (marker.activeSelf)
        {
            if (!activate)
            {
                foreach (GameObject smudge in smudges)
                {
                    smudge.SetActive(true);
                }
                activate = true;
            }
            anim = marker.GetComponent<Animator>();
            anim.Play("MarkerMoveAlongCircle");
        }

    }

}
