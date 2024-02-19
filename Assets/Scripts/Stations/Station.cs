using UnityEngine;

public abstract class Station : MonoBehaviour
{
    [SerializeField] protected Camera associatedCamera;
    [SerializeField] protected static float distanceMinimum = 0.12f;
    protected bool running = false;

    public static Station activeStation;
    
    public void Activate()
    {
        associatedCamera.gameObject.SetActive(true);
        running = true;
        activeStation = this;
    }

    public void Deactivate()
    {
        associatedCamera.gameObject.SetActive(false);
        running = false;
    }

    public static void HandlePointCollision()
    {
        activeStation?.pointCollision();
    }

    protected virtual void pointCollision()
    {
        Composer.Instance.PlayHiHat();
    }

    public static void HandlePathUpdate(Vector2 offset)
    {
        activeStation?.pathUpdate(offset);
    }

    public virtual void pathUpdate(Vector2 offset)
    {
        float distance = offset.magnitude;

        if (distance < distanceMinimum)
        {
            distance = 0;
        }

        //Composer.Instance.VolumeChange(1, volume);
        Composer.Instance.PitchChange(-distance);
    }

    public Camera GetAssociatedCamera() { return associatedCamera; }
    public bool IsRunning() { return running; }
}
