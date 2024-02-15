using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PancakeParticleSpawner : ParticleSpawner
{
    [SerializeField] private Color rawColor;
    [SerializeField] private Color burntColor;
    private PancakeParticleObject pancake;

    public override void Activate()
    {
        if (!ParticleObjectExists())
        {
            GameObject obj = new GameObject();
            particleObject = obj.AddComponent<PancakeParticleObject>(); 
            pancake = particleObject as PancakeParticleObject;
        }
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
            Debug.Log("updating side colors");
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

    public void SavePancake()
    {
        if (ParticleObjectExists())
        {
            Instantiate(particleObject as PancakeParticleObject, new Vector3(5,0,0), Quaternion.identity);
            DestroyParticleObject();
        }
    }
}