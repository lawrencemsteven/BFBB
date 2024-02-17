using UnityEngine;

public class PancakeParticleObject : ParticleObject
{
    private int cookingSide = 0;
    private int flipping = 0;
    private float flipAngle = 0;
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
        if (flipping != 0 )
        {
            return;
        }

        if (cookingSide == 0)
        {
            cookingSide = 1;
            foreach (Particle particle in particles)
            {
                particle.transform.rotation = Quaternion.Euler(180, particle.transform.rotation.y, particle.transform.rotation.z);
            }
        }
        else if (cookingSide == 1)
        {
            cookingSide = 0;
            foreach (Particle particle in particles)
            {
                particle.transform.rotation = Quaternion.Euler(0, particle.transform.rotation.y, particle.transform.rotation.z);
            }
        }
    }
}