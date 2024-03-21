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
    protected bool active = false;
    
    public abstract void Activate();
    public abstract void Deactivate();

    public abstract void ParticleUpdate();
}
