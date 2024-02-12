using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateCollider : MonoBehaviour
{
    public string targetTag;
    public int index;

    void OnCollisionEnter(Collision other)
    {
        if (other.CompareTag(targetTag))
        {
            Station.HandlePointCollision(index);
        }
    }
}
