using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsShapeCovered : MonoBehaviour
{
    public GameObject shape1, shape2, shape3, shape4, shape5, shape6;
    private List<GameObject> points = new List<GameObject>();
    private List<GameObject> points2 = new List<GameObject>();
    private List<GameObject> points3 = new List<GameObject>();
    private List<GameObject> points4 = new List<GameObject>();
    private List<GameObject> points5 = new List<GameObject>();
    private List<GameObject> points6 = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shape1.transform.childCount; i++)
        {
            Transform point = shape1.transform.GetChild(i);
            points.Add(point.gameObject);
        }
        for (int i = 0; i < shape2.transform.childCount; i++)
        {
            Transform point = shape2.transform.GetChild(i);
            points2.Add(point.gameObject);
        }
        for (int i = 0; i < shape3.transform.childCount; i++)
        {
            Transform point = shape3.transform.GetChild(i);
            points3.Add(point.gameObject);
        }
        for (int i = 0; i < shape4.transform.childCount; i++)
        {
            Transform point = shape4.transform.GetChild(i);
            points4.Add(point.gameObject);
        }
        for (int i = 0; i < shape5.transform.childCount; i++)
        {
            Transform point = shape5.transform.GetChild(i);
            points5.Add(point.gameObject);
        }
        for (int i = 0; i < shape6.transform.childCount; i++)
        {
            Transform point = shape6.transform.GetChild(i);
            points6.Add(point.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<int> GetNumOfColliders()
    {
        List<int> numOfColliders = new List<int>();
        numOfColliders.Add(points.Count);
        numOfColliders.Add(points2.Count);
        numOfColliders.Add(points3.Count);
        numOfColliders.Add(points4.Count);
        numOfColliders.Add(points5.Count);
        numOfColliders.Add(points6.Count);
        return numOfColliders;
    }

    private void OnParticleCollision(GameObject other)
    {
        PointIsHittable hittableComponent = other.GetComponent<PointIsHittable>();

        if (other.transform.parent != null)
        {
            if (other.transform.parent.gameObject == shape1)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        points.Remove(other);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape2)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        points2.Remove(other);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape3)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        points3.Remove(other);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape4)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        points4.Remove(other);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape5)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        points5.Remove(other);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape6)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        points6.Remove(other);
                    }

                }
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
