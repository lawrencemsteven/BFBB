using System.Collections.Generic;
using UnityEngine;

public class Pancake : MonoBehaviour
{
    private List<GameObject> particles = new List<GameObject>();

    //0 = bottom; 1 = top
    private int cookingSide = 0;
    private int flipping = 0;
    private float flipAngle = 0;
    private float cookAmountTop, cookAmountBottom = 0.0f;
    private bool cooking = false;
    private float cookSpeed = 0.1f;

    public void Update()
    {
        if (cooking)
        {
            Debug.Log($"Top: {cookAmountTop}, Bottom: {cookAmountBottom}");
            if (cookingSide == 0)
            {
                cookAmountBottom += cookSpeed * Time.deltaTime;
            }
            else if (cookingSide == 1)
            {
                cookAmountTop += cookSpeed * Time.deltaTime;
            }
        }

        if (flipping != 0)
        {
            Debug.Log("FLIPPING");
            if (flipping == 1)
            {
                Debug.Log("upside down");
                flipAngle += 10f;
                if (flipAngle >= 180)
                {
                    Debug.Log("upside down complete");
                    flipAngle = 180;
                    flipping = 0;
                }
                transform.rotation = Quaternion.Euler( flipAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
            if (flipping == -1)
            {
                Debug.Log("right side up");
                flipAngle -= 10f;
                if (flipAngle <= 0)
                {
                    Debug.Log("right side up compelte");
                    flipAngle = 0;
                    flipping = 0;
                }
                transform.rotation = Quaternion.Euler( flipAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }
    }

    public void AddToParticles(GameObject particle)
    {
        particle.transform.SetParent(transform);
        particles.Add(particle);
    }

    public void UpdateSideColors(Color color1, Color color2)
    {
        foreach (GameObject storedParticle in particles)
        {
            PancakeParticle particle = storedParticle.GetComponent<PancakeParticle>();
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
            //flipping = 1;
        }
        else if (cookingSide == 1)
        {
            cookingSide = 0;
            //flipping = -1;
        }
    }
}