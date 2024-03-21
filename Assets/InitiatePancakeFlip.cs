using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePancakeFlip : MonoBehaviour
{
    public flipController fc;
    private bool noFlip = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag.Equals("Spatula")))
        {
            if (fc.startFlip && !noFlip)
            {
                GlobalVariables.score += 1;

                fc.startFlip = false;
                fc.FlipPancake();

            }
            else
            {
                noFlip = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag.Equals("Spatula")))
        {
            noFlip = false;
        }
    }

}
