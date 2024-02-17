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

    public static void HandlePointCollision()
    {
        Composer.Instance.PlayHiHat();
    }

    public static void HandlePathUpdate(Vector2 offset)
    {
        float volume = (offset.x + 0.3f) / 0.6f;
        float pitch = ((offset.y + 0.3f) / 0.3f) - 1;

        Composer.Instance.VolumeChange(1, volume);
        Composer.Instance.PitchChange(pitch);
    }

    public Camera GetAssociatedCamera() { return associatedCamera; }
    public bool IsRunning() { return running; }
}
