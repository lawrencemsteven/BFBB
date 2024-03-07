using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePancakeFlip : MonoBehaviour
{
    public flipController fc;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag.Equals("Spatula")) && fc.startFlip)
        {
            GlobalVariables.score += 1;

            fc.startFlip = false;
            fc.FlipPancake();
        }
    }
}
