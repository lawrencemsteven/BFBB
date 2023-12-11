using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointIsHittable : MonoBehaviour
{
    private bool hittable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHittable(bool isHittable)
    {
        hittable = isHittable;
    }

    public bool GetHittable()
    {
        return hittable;
    }
}
