using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpatulaArea : MonoBehaviour
{
    public GameObject detectors;
    public SpatulaCursor spatula;
    private List<GameObject> areaDetectors = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < detectors.transform.childCount; i++)
        {
            areaDetectors.Add(detectors.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AreaDetector")
        {
            for (int i = 0; i < areaDetectors.Count; i++)
            {
                if (areaDetectors[i].gameObject.Equals(other.gameObject))
                {
                    spatula.SetSpatulaArea(i);
                }
            }
            // currentPancakeArea.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}
