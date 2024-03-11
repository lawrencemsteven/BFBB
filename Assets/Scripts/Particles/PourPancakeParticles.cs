using System.Reflection;
using UnityEngine;

public class PourPancakeParticles : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 10.0f;
    private PancakeParticleSpawner pancakeParticleSystem;
    private Vector3? storedMousePosition;

    public void Start()
    {
        pancakeParticleSystem = GetComponent<PancakeParticleSpawner>();
    }

    public void Update()
    {

        if (Input.GetMouseButton(0))
        {
            trySpawnParticle();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pancakeParticleSystem.ToggleCooking();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pancakeParticleSystem.Flip();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pancakeParticleSystem.SavePancake();
        }
    }

    private void trySpawnParticle()
    {
        if (storedMousePosition is null)
        {
            pancakeParticleSystem.SpawnSingleParticle(Input.mousePosition);
            storedMousePosition = Input.mousePosition;
            return;
        }

        float distanceTraveled = Vector3.Distance(Input.mousePosition, (Vector3)storedMousePosition);
        
        while (distanceTraveled > distanceThreshold)
        {
            distanceTraveled -= distanceThreshold;
            
            Vector3 angleVector = (Input.mousePosition - (Vector3)storedMousePosition).normalized;

            Vector3 midpointVector = (Vector3)storedMousePosition + (distanceThreshold * angleVector);

            pancakeParticleSystem.SpawnSingleParticle(midpointVector);

            storedMousePosition = midpointVector;
        }

        storedMousePosition = Input.mousePosition;
    }
}