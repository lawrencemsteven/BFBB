using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;



public class PointData
{
    public GameObject gameObject;
    public bool isHit;
}

public class IsShapeCovered : MonoBehaviour
{
    public GameObject shape1, shape2, shape3, shape4, shape5, shape6;
    public int currentMarkerPos = 0;
    private List<PointData> points = new List<PointData>();
    private List<PointData> points2 = new List<PointData>();
    private List<PointData> points3 = new List<PointData>();
    private List<PointData> points4 = new List<PointData>();
    private List<PointData> points5 = new List<PointData>();
    private List<PointData> points6 = new List<PointData>();

    public void SetPoints()
    {
        Debug.Log("Setting points");
        SetPointDataList(shape1, points);
        SetPointDataList(shape2, points2);
        SetPointDataList(shape3, points3);
        SetPointDataList(shape4, points4);
        SetPointDataList(shape5, points5);
        SetPointDataList(shape6, points6);
    }

    private void SetPointDataList(GameObject shape, List<PointData> pointList)
    {
        Debug.Log("Collider count for " +  shape.gameObject+ " : " + shape.transform.childCount);
        for (int i = 0; i < shape.transform.childCount - 1; i++)
        {
            Transform point = shape.transform.GetChild(i);
            pointList.Add(new PointData { gameObject = point.gameObject, isHit = false });
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<int> GetNumOfHitColliders()
    {
        List<int> numOfColliders = new List<int>();

        List<List<PointData>> pointDataLists = new List<List<PointData>>
        {
            points,
            points2,
            points3,
            points4,
            points5,
            points6
        };

        

        foreach(List<PointData> points in pointDataLists)
        {
            int num = points.Count;
            foreach(PointData point in points)
            {
                Debug.Log("Returning point data: " + point.gameObject.transform.parent.gameObject + " " + point.isHit + num);
                if (point.isHit){
                    num -= 1;
                }

            }
            numOfColliders.Add(num);
        }
        return numOfColliders;
    }

    public void SetCurrentMarkerPos(GameObject shape, int pos){
        shape.GetComponent<ShapeGenerator>().currentMarkerPos = pos;
    
    }

    private void SetColliderToHit(GameObject collider, List<PointData> points)
    {
        foreach (PointData point in points)
        {   
            if (point.gameObject == collider)
            {
                Debug.Log(point.gameObject.transform.parent.gameObject);
                point.isHit = true;
                return;
            }
        }
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
                        SetColliderToHit(other, points);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape2)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        SetColliderToHit(other, points2);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape3)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        SetColliderToHit(other, points3);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape4)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        SetColliderToHit(other, points4);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape5)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        SetColliderToHit(other, points5);
                    }

                }
            }
            else if (other.transform.parent.gameObject == shape6)
            {
                if (hittableComponent != null)
                {

                    if (hittableComponent.GetHittable() == true)
                    {
                        SetColliderToHit(other, points6);
                    }

                }
            }
        }
    }
}
