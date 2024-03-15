using Unity.VisualScripting;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    [SerializeField] protected Camera associatedCamera;
    [SerializeField] protected LineManager lineManager;
    [SerializeField] protected CoordinateGenerator coordinateGenerator;
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected SoundBytePlayer soundBytePlayer;

    [SerializeField] protected static float distanceMinimum = 0.12f;

    protected bool running = false;

    public static Station activeStation;
    
    public void Awake()
    {
        Stations.InitializeStations();
    }

    public virtual void Activate()
    {
        associatedCamera.gameObject.SetActive(true);
        running = true;
        activeStation = this;
    }

    public virtual void Deactivate()
    {
        associatedCamera.gameObject.SetActive(false);
        running = false;
    }

    public static void HandlePointCollision(int index)
    {
        activeStation?.pointCollision(index);
    }

    protected virtual void pointCollision(int index)
    {
        Composer.Instance.PlayHiHat();
    }

    public static void HandlePathUpdate(Vector2 offset)
    {
        activeStation?.pathUpdate(offset);
    }

    public virtual void pathUpdate(Vector2 offset)
    {
        return;
    }

    public Camera GetAssociatedCamera() { return associatedCamera; }
    public bool IsRunning() { return running; }

    public LineManager GetLineManager() { return lineManager; }
    public CoordinateGenerator GetCoordinateGenerator() { return coordinateGenerator; }
    public LineRenderer GetLineRenderer() { return lineRenderer; }
    public SoundBytePlayer GetSoundBytePlayer() { return soundBytePlayer; }
}
