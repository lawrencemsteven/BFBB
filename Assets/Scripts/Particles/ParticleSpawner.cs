using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ParticleSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject particle;
    [SerializeField] protected float spawnHeight;
    [SerializeField] protected float spawnCooldown;
    [SerializeField] protected Transform objectSpawnParent;
    protected float currentSpawnCooldown;
    protected ParticleObject particleObject;
    protected bool active = false;
    
    public abstract void Activate();
    public abstract void Deactivate();
    public ParticleObject GetParticleObject() { return particleObject; }
    public void DestroyParticleObject()
    {
        Destroy(particleObject.gameObject);
    }

    public void SpawnSingleParticle(Vector3 position)
    {
        Ray rayCast = Stations.Pancake.GetAssociatedCamera().ScreenPointToRay(position);
        GameObject spawnedParticle = Instantiate(particle, new Vector3(rayCast.GetPoint(1).x, spawnHeight, rayCast.GetPoint(1).z), Quaternion.identity);
        particleObject.AddToParticles(spawnedParticle.GetComponent<Particle>());
    }

    public void Update()
    {
        currentSpawnCooldown -= Time.deltaTime;

        if (active && currentSpawnCooldown <= 0)
        {
            SpawnSingleParticle(Input.mousePosition);
            currentSpawnCooldown = spawnCooldown;
        }

        ParticleUpdate();
    }

    public bool ParticleObjectExists()
    {
        return particleObject is not null && !particleObject.IsDestroyed();
    }

    public abstract void ParticleUpdate();
}
