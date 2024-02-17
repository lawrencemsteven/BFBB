using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ParticleObject : MonoBehaviour
{
    protected Queue<Particle> particles = new Queue<Particle>();
    protected abstract int particleLimit { get; }

    public void AddToParticles(Particle particle)
    {
        particle.transform.SetParent(transform);
        if (particles.Count >= particleLimit)
        {
            Particle toDestroy = particles.Dequeue();
            Destroy(toDestroy.gameObject);
        }
        particles.Enqueue(particle);
    }
}