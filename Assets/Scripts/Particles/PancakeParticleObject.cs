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
        if (transform.parent is not null)
        {
            transform.parent.gameObject.GetComponent<Animator>().SetTrigger("FlipTrigger");
        }

        cookingSide = cookingSide == 1 ? 0 : 1;
    }
}