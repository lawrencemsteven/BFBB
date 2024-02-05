using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmudgeCollision : MonoBehaviour
{

    public bool spongeColliding = false;
    public bool barColliding = false;
    public bool barExit = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        print("trigger");
        print(collider.gameObject.tag);
        if(collider.gameObject.tag == "Sponge")
        {
            Debug.Log("SPONGE");
            spongeColliding = true;
        }

        if(collider.gameObject.tag == "Bar")
        {
            Debug.Log("BAR");
            barColliding = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Sponge")
        {
            spongeColliding = false;
        }

        if(collider.gameObject.tag == "Bar")
        {
            barColliding = false;
            barExit = true;
        }
    }

    public void Reset()
    {
        spongeColliding = false;
        barColliding = false;
        barExit = false;
    }
}
