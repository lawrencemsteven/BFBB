using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scorePancake : MonoBehaviour
{
    // Start is called before the first frame update
    public PancakeParticleSpawner spawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.tag.Equals("PancakeAnchor") && (other.gameObject.transform.GetChild(0).GetComponent<PancakeParticleObject>().isDone()))
        {

        }
        */
    }
}
