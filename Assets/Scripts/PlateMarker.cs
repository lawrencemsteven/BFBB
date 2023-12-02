using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMarker : MonoBehaviour
{
    private Animator anim;
    public GameObject marker;

    void Start()
    {
        Debug.Log("This Is Running");
        anim = marker.GetComponent<Animator>();
        anim.Play("MarkerMoveAlongCircle");
    }

}
