using UnityEngine;

public class PourPancakeParticles : MonoBehaviour
{
    private PancakeParticleSpawner pancakeParticleSystem;

    public void Start()
    {
        pancakeParticleSystem = GetComponent<PancakeParticleSpawner>();
    }

    public void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            pancakeParticleSystem.Activate();
        }

        if (Input.GetMouseButtonUp(0))
        {
            pancakeParticleSystem.Deactivate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pancakeParticleSystem.ToggleCooking();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pancakeParticleSystem.Flip();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pancakeParticleSystem.SavePancake();
        }
    }
}