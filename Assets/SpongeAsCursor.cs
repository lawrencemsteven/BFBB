using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAsCursor : MonoBehaviour
{
    public Camera dishWashingCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = dishWashingCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z);
    }
}
