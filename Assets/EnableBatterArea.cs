using Orders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBatterArea : MonoBehaviour
{
    public PancakeParticleSpawner particleSpawner;
    private GameObject currentPancakeArea, previousPancakeArea;
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject getCurrentPancakeArea() {
        return currentPancakeArea; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AreaDetector")
        {
            currentPancakeArea = other.gameObject;
            Debug.Log(other.gameObject.name);
            // currentPancakeArea.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("AreaDetector"))
        {

            // particleSpawner.SavePancake(other.gameObject.transform.GetChild(0).transform);

        }
    }
}
