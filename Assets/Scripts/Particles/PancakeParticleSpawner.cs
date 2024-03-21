using Orders;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PancakeParticleSpawner : ParticleSpawner
{

    [SerializeField] private float griddleX;
    [SerializeField] private float griddleZ;
    [SerializeField] private Color rawColor;
    [SerializeField] private Color burntColor;

    public GameObject detectors, prevArea = null;
    public SpatulaCursor spatulaCursor;
    private PancakeParticleObject pancake;
    private List<GameObject> pancakeAreas = new List<GameObject>();
    private Dictionary<int, PancakeParticleObject> pancakes = new Dictionary<int, PancakeParticleObject>();
    private int areaNum;


    private void Start()
    {
        for (int i = 0; i < detectors.transform.childCount; i++)
        {
            pancakeAreas.Add(detectors.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        currentSpawnCooldown -= Time.deltaTime;

        if (active && currentSpawnCooldown <= 0)
        {
            SpawnSingleParticle(Input.mousePosition);
            currentSpawnCooldown = spawnCooldown;
        }

        ParticleUpdate();
    }

    public void Initialize(GameObject area)
    {
        if (area.gameObject.Equals(pancakeAreas[0])){
            areaNum = 0;
        }
        else if (area.gameObject.Equals(pancakeAreas[1]))
        {
            areaNum = 1;
        }
        else if (area.gameObject.Equals(pancakeAreas[2]))
        {
            areaNum = 2;
        }
        if (!pancakes.ContainsKey(areaNum))
        {
            GameObject obj = new GameObject();
            obj.transform.position = new Vector3(griddleX, spawnHeight, griddleZ);
            ParticleObject particleObject = obj.AddComponent<PancakeParticleObject>();
            particleObject.transform.SetParent(area.transform.GetChild(2).transform);
            pancake = particleObject as PancakeParticleObject;
            pancakes.Add(areaNum, pancake);
        }
        else
        {
            pancake = pancakes[areaNum];
        }
    }

    public override void Activate()
    {
        Initialize(pancakeAreas[0]);
        active = true;
    }

    public override void Deactivate()
    {
        active = false;
    }

    public override void ParticleUpdate()
    {
        if (ParticleObjectExists() && pancake.IsCooking())
        {
            pancake.UpdateSideColors(rawColor, burntColor);
        }
    }

    public void ToggleCooking()
    {
        pancake.ToggleCooking();
    }

    public void Flip()
    {
        if (spatulaCursor.GetSpatulaArea() != areaNum)
        {
            areaNum = spatulaCursor.GetSpatulaArea();
            if (pancakes.ContainsKey(areaNum))
            {
                pancake = pancakes[areaNum];
            }
            else
            {
                return;
            }
        }
        pancake.Flip();
        if (pancake.isDone())
        {
            pancakes.Remove(areaNum);
        }

    }

    public bool ParticleObjectExists()
    {
        return pancake is not null && !pancake.IsDestroyed();
    }

    public void SavePancake()
    {
        pancake.SavePancake();
    }


    public ParticleObject GetParticleObject() { return pancake; }
    public void DestroyParticleObject()
    {
        Destroy(pancake.gameObject);
    }

    public void SpawnSingleParticle(Vector3 position)
    {
        Ray rayCast = Stations.Pancake.GetAssociatedCamera().ScreenPointToRay(position);
        GameObject spawnedParticle = Instantiate(particle, new Vector3(rayCast.GetPoint(1).x, spawnHeight, rayCast.GetPoint(1).z), Quaternion.identity);
        pancake.AddToParticles(spawnedParticle.GetComponent<Particle>());
    }
}