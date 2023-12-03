using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsShapeCovered : MonoBehaviour
{
    public GameObject shape;
    public GameObject pancake;
    private ParticleSystem particleSystem;
    private List<GameObject> points = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        for (int i = 0; i < shape.transform.childCount; i++)
        {
            Transform point = shape.transform.GetChild(i);
            points.Add(point.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnParticleTrigger()
    {
        Debug.Log("AHHA");
        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
        int numParticles = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, particles);

        for (int i = 0; i < numParticles; i++)
        {
            ParticleSystem.Particle particle = particles[i];

            // Perform a raycast from the particle's position
            RaycastHit hit;
            if (Physics.Raycast(particle.position, particle.velocity.normalized, out hit, Mathf.Infinity))
            {
                // Access information about the collider
                Collider collider = hit.collider;
                Debug.Log(collider);
                // Do something with the collider information
            }
        }
    }
    */

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other);
        points.Remove(other);
        Debug.Log(points.Count);
        if (points.Count == 0)
        {
            pancake.SetActive(true);
            shape.SetActive(false);
        }
    }
}
