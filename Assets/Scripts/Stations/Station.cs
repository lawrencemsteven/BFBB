using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public abstract class Station : MonoBehaviour
{
    [SerializeField] protected Camera associatedCamera;
    protected bool running = false;

    public static Station activeStation;
    
    public void Activate()
    {
        associatedCamera.gameObject.SetActive(true);
        running = true;
    }

    public void Deactivate()
    {
        associatedCamera.gameObject.SetActive(false);
        running = false;
    }

    public static void HandlePointCollision(int index)
    {
        Debug.Log($"Point {index} collided");
    }

    public Camera GetAssociatedCamera() { return associatedCamera; }
    public bool IsRunning() { return running; }
}
