using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepStationCursor : MonoBehaviour
{
    private float offset = 0.70f;
    [SerializeField] private float distanceThreshold = 20f;

    // Update is called once per frame
    void Update()
    {
            if (!Stations.Prep.IsRunning())
            {
                return;
            }

            Ray ray = Stations.Prep.GetAssociatedCamera().ScreenPointToRay(Input.mousePosition);
            Vector3 targetPosition = ray.GetPoint(offset);
            transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
            if (Input.GetMouseButtonDown(0))
            {
                //enable toppings particles
            }
            if (Input.GetMouseButton(0))
            {
                //keep doing toppings particles
            }
            if (Input.GetMouseButtonUp(0))
            {
                //stop doing toppings particles
            }
    }
}
