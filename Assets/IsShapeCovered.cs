using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsShapeCovered : MonoBehaviour
{
    public GameObject shape;
    public GameObject pancake;
    private List<GameObject> points = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shape.transform.childCount; i++)
        {
            Transform point = shape.transform.GetChild(i);
            points.Add(point.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetNumOfColliders()
    {
        return points.Count;
    }

    private void OnParticleCollision(GameObject other)
    {
        PointIsHittable hittableComponent = other.GetComponent<PointIsHittable>();
        if (hittableComponent != null)
        {
            if (hittableComponent.GetHittable() == true)
            {
                points.Remove(other);
            }

        }
        /*
        if (points.Count == 0)
        {
            pancake.SetActive(true);
            shape.SetActive(false);
            gameObject.SetActive(false);
        }
        */
    }
}
