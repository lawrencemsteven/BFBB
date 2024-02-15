using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeParticle : MonoBehaviour
{
    public GameObject GetOuterBottom()
    {
        return transform.GetChild(0).gameObject;
    }
    
    public GameObject GetOuterTop()
    {
        return transform.GetChild(2).gameObject;
    }
}
