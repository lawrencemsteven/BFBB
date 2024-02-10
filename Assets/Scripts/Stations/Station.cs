using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public abstract class Station : MonoBehaviour
{
    [SerializeField] protected Camera associatedCamera;
    [SerializeField] protected StationType stationType;
    protected bool running = false;
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
    public Camera GetAssociatedCamera() { return associatedCamera; }
    public StationType GetStationType() { return stationType; }
    public bool IsRunning() { return running; }
}
