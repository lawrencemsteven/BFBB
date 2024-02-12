using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public abstract class Station : MonoBehaviour
{
    [SerializeField] protected Camera associatedCamera;
    [SerializeField] protected StationType stationType;
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
    public StationType GetStationType() { return stationType; }
    public bool IsRunning() { return running; }
}
