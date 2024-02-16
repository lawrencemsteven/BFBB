using UnityEngine;

public abstract class Station : MonoBehaviour
{
    [SerializeField] protected Camera associatedCamera;
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

    public static void HandlePointCollision(int index)
    {
        Debug.Log($"Point {index} collided");

        Composer.Instance.PlayHiHat();

        //float volume = (distanceVector.x + 0.3f) / 0.6f;
        //float pitch = ((distanceVector.y + 0.3f) / 0.3f) - 1;

        //Composer.Instance.VolumeChange(volume);
        //Composer.Instance.PitchChange(pitch);
    }

    public static void HandlePathUpdate(Vector2 offset)
    {
        // TODO: this.
    }

    public Camera GetAssociatedCamera() { return associatedCamera; }
    public bool IsRunning() { return running; }
}
