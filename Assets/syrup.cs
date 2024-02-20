using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syrup : MonoBehaviour
{
    public GameObject liquid;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton (0))
        {
            Instantiate(liquid, this.transform.position, this.transform.rotation);
        }
    }
}
