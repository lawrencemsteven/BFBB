using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeStationCursor : MonoBehaviour
{
    public GameObject squeezed, unsqueezed;
    public bool paused = false;
    private PancakeParticleSpawner pancakeParticleSpawner;
    private ParticleSystem batterPour;
    private float offset = 0.90f;
    private Renderer bottleRenderer;
    private Vector3? storedMousePosition;
    private EnableBatterArea eba;
    private GameObject lastPancakeArea;
    [SerializeField] private float distanceThreshold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        eba = gameObject.transform.GetComponentInChildren<EnableBatterArea>();
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
            lastPancakeArea = eba.getCurrentPancakeArea();
            trySpawnParticle();
            Stations.Pancake.SetPancakeParticleObject(pancakeParticleSpawner.GetParticleObject() as PancakeParticleObject);
            unsqueezed.SetActive(false);
            squeezed.SetActive(true);
            batterEmission.enabled = true;
        }
        if (Input.GetMouseButton(0))
        {/*
                if ((lastPancakeArea != eba.currentPancakeArea) && lastPancakeArea != null)
                {
                    lastPancakeArea.transform.GetChild(-1).gameObject.SetActive(false);
                }
            */
            lastPancakeArea = eba.getCurrentPancakeArea();
            trySpawnParticle();
        }
        if (Input.GetMouseButtonUp(0))
        {
            storedMousePosition = null;
            squeezed.SetActive(false);
            unsqueezed.SetActive(true);
            batterEmission.enabled = false;
        }
    }

    private void trySpawnParticle()
    {
        pancakeParticleSpawner.Initialize(lastPancakeArea);

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
