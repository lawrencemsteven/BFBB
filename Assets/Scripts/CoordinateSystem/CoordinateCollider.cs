using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateCollider : MonoBehaviour
{
    public string targetTag;
    public int index;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            Station.HandlePointCollision();
        }
    }
}
