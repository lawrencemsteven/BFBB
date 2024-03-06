using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartFlip : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (other.gameObject.tag.Equals("Spatula"))
        {
            fc.startFlip = true;
        }
    }
}
