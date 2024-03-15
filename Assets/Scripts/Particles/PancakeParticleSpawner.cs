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
    private PancakeParticleObject pancake;
    private List<GameObject> pancakeAreas = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < detectors.transform.childCount; i++)
        {
            pancakeAreas.Add(detectors.transform.GetChild(i).gameObject);
        }
    }

    public void Initialize(GameObject area)
    {
        Debug.Log(area.gameObject.name);
        if ((!ParticleObjectExists()) || (area != prevArea))
        {
            GameObject obj = new GameObject();
            obj.transform.position = new Vector3(griddleX, spawnHeight, griddleZ);
            particleObject = obj.AddComponent<PancakeParticleObject>();
            particleObject.transform.SetParent(area.transform.GetChild(2).transform);
            pancake = particleObject as PancakeParticleObject;
        }
        prevArea = area;
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
        pancake.Flip();
    }

    public void SavePancake(GameObject pancakeObject)
    {
        Debug.Log(pancakeObject.gameObject.transform.parent.gameObject.name);
        Debug.Log(pancake.GetQuality());
        ReservoirManager.GetPancakes().Add(new ReservoirPancake(pancake.GetQuality(), pancakeObject));
        Destroy(pancakeObject.gameObject);
    }
}