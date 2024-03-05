using System.Collections.Generic;
using UnityEngine;

public class PancakeParticleObject : ParticleObject
{
    private int cookingSide = 0;
    private float cookAmountTop, cookAmountBottom = 0.0f;
    private bool cooking = true;
    private float cookSpeed = 0.1f;
    protected override int particleLimit
    {
        get
        {
            return 64;
        }
    }


    public void Update()
    {
        if (cooking)
        {
            if (cookingSide == 0)
            {
                cookAmountBottom += cookSpeed * Time.deltaTime;
            }
            else if (cookingSide == 1)
            {
                cookAmountTop += cookSpeed * Time.deltaTime;
            }
        }
    }
    
    public void UpdateSideColors(Color color1, Color color2)
    {
        foreach (Particle storedParticle in particles)
        {
            PancakeParticle particle = storedParticle as PancakeParticle;
            particle.GetOuterBottom().GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, cookAmountBottom);
            particle.GetOuterTop().GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, cookAmountTop);
        }
    }

    public bool IsCooking() { return cooking; }
    public void SetCooking(bool enable) { cooking = enable; }
    public void ToggleCooking() { cooking = !cooking; }
    
    public int GetCookingSide() { return cookingSide; }
    public void Flip()
    {
        if (cookingSide == 0)
        {
            if (transform.parent is not null)
            {
                transform.parent.gameObject.GetComponent<Animator>().SetTrigger("FlipTrigger");
            }
            cookingSide = 1;
            return;
        }

        if (cookingSide == 1)
        {
            ReservoirManager.GetPancakes().Add(new ReservoirPancake(GetQuality(), gameObject));

            // Reset for new pancake

            foreach (Particle particle in particles)
            {
                Destroy(particle.gameObject);
            }
            particles = new Queue<Particle>();

            cookAmountTop = 0.0f;
            cookAmountBottom = 0.0f;
            cooking = true;
            cookSpeed = 0.1f;
            cookingSide = 0;
            return;
        }
    }

    public float GetQuality()
    {
        float quality = 1;
        quality -= Mathf.Abs(Mathf.Min(cookAmountTop, 1.0f) - 0.5f);
        quality -= Mathf.Abs(Mathf.Min(cookAmountBottom, 1.0f) - 0.5f);
        return quality;
    }
}