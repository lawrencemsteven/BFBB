using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaffleMakerUI : MonoBehaviour
{
    public GameObject image;
    public GameObject waffleMaker;
    private Vector3 offSet = new Vector3(0, 0.30f, 0);
    public GameObject camera;
    public string textToCopyFrom;
    public TMP_Text textMeshToCopyTo;
    // Start is called before the first frame update
    void Start()
    {
        textToCopyFrom = "H";

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
            if (waffleMaker.transform.rotation.eulerAngles.z == 180)
            {
                textMeshToCopyTo.text = textToCopyFrom;
            }
            else if (waffleMaker.transform.rotation.eulerAngles.z == 0)
            {
                textMeshToCopyTo.text = "G";
            }
        }
        else
        {
            image.SetActive(false);
        }

    }
}
