using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uIManager : MonoBehaviour
{
    public GameObject image;
    private Vector3 offSet = new Vector3(-0.05f, 0.20f, 0);
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        if (camera.activeSelf)
        {
            image.SetActive(true);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.activeSelf)
        {
            image.SetActive(true);
            image.transform.position = Camera.main.WorldToScreenPoint(transform.position + offSet);
        }
        else
        {
            image.SetActive(false);
        }
        
    }
}
