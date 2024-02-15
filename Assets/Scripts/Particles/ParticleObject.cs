using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ParticleObject : MonoBehaviour
{
    protected List<Particle> particles = new List<Particle>();

    public void AddToParticles(Particle particle)
    {
        particle.transform.SetParent(transform);
        particles.Add(particle);
    }
}