using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeHitDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform shape;
    private List<GameObject> points = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < shape.childCount; i++)
        {
            Transform point = shape.GetChild(i);
            points.Add(point.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PointIsHittable>() != null)
        {
            other.GetComponent<PointIsHittable>().SetHittable(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PointIsHittable>() != null)
        {

            other.GetComponent<PointIsHittable>().SetHittable(false);
        }
    }

 
}
