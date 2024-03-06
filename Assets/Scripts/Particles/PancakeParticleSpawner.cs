using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PancakeParticleSpawner : ParticleSpawner
{
    
    [SerializeField] private float griddleX;
    [SerializeField] private float griddleZ;
    [SerializeField] private Color rawColor;
    [SerializeField] private Color burntColor;
    public GameObject pancakeArea;
    private PancakeParticleObject pancake;

    public void Initialize()
    {
        if (!ParticleObjectExists())
        {
            GameObject obj = new GameObject();
            obj.transform.position = new Vector3(griddleX, spawnHeight, griddleZ);
            particleObject = obj.AddComponent<PancakeParticleObject>();
            particleObject.transform.SetParent(pancakeArea.transform); 
            pancake = particleObject as PancakeParticleObject;
        }
    }

    public override void Activate()
    {
        Initialize();
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

    public void SavePancake(Transform pancakeTransform)
    {
        if (ParticleObjectExists())
        {
            Instantiate(particleObject as PancakeParticleObject, pancakeTransform.position, Quaternion.identity);
            DestroyParticleObject();
        }
    }
}