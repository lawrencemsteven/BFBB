using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeStationCursor : MonoBehaviour
{
    public Material squeezed, unsqueezed;
    public bool paused = false;
    private PancakeParticleSpawner pancakeParticleSpawner;
    private ParticleSystem batterPour;
    private float offset = 0.70f;
    private Renderer bottleRenderer;
    private Vector3? storedMousePosition;
    [SerializeField] private float distanceThreshold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject bottle = transform.GetChild(0).gameObject;
        bottleRenderer = bottle.GetComponent<Renderer>();
        pancakeParticleSpawner = GetComponent<PancakeParticleSpawner>();
        batterPour = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
            if (!Stations.Pancake.IsRunning())
            {
                return;
            }

            Ray ray = Stations.Pancake.GetAssociatedCamera().ScreenPointToRay(Input.mousePosition);
            Vector3 targetPosition = ray.GetPoint(offset);
            transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
            if (Input.GetMouseButtonDown(1))
            {
                bottleRenderer.enabled = false;
            }
            if (Input.GetMouseButtonUp(1))
            {
                bottleRenderer.enabled = true;
            }
            var batterEmission = batterPour.emission;
            if (Input.GetMouseButtonDown(0))
            {
                trySpawnParticle();
                Stations.Pancake.SetPancakeParticleObject(pancakeParticleSpawner.GetParticleObject() as PancakeParticleObject);
                bottleRenderer.material = squeezed;
                batterEmission.enabled = true;
            }
            if (Input.GetMouseButton(0))
            {
                trySpawnParticle();
            }
            if (Input.GetMouseButtonUp(0))
            {
                storedMousePosition = null;
                bottleRenderer.material = unsqueezed;
                batterEmission.enabled = false;
            }
    }

    private void trySpawnParticle()
    {
        pancakeParticleSpawner.Initialize();

        if (storedMousePosition is null)
        {
            pancakeParticleSpawner.SpawnSingleParticle(Input.mousePosition);
            storedMousePosition = Input.mousePosition;
            return;
        }

        float distanceTraveled = Vector3.Distance(Input.mousePosition, (Vector3)storedMousePosition);
        
        while (distanceTraveled > distanceThreshold)
        {           
            Vector3 angleVector = (Input.mousePosition - (Vector3)storedMousePosition).normalized;

            Vector3 midpointVector = (Vector3)storedMousePosition + (distanceThreshold * angleVector);

            pancakeParticleSpawner.SpawnSingleParticle(midpointVector);

            storedMousePosition = midpointVector;

            distanceTraveled -= distanceThreshold;
        }
    }
}
