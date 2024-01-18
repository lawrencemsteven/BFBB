using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirItem : MonoBehaviour
{
    // The ideal quality should be 1.0f
    // Quality should range from 0.0f to 2.0f
    // If cooking, 0.0f represents raw and 2.0f represents charcoal
    // If there are not opposite extremes, just use 0.0f to 1.0f.
    private float quality;

    public ReservoirItem(float newQuality)
    {
        SetQuality(newQuality);
    }

    // Returns quality for use in scoring, on a scale from 0.0f to 1.0f.
    // 1.0f is perfect. 0.0f is either extreme of raw or charcoal.
    public float GetScoringQuality()
    {
        if (quality > 1)
        {
            return 2 - quality;
        } else
        {
            return quality;
        }
    }

    // Returns the direct quality value for use in display.
    public float GetDisplayQuality()
    {
        return quality;
    }

    public void SetQuality(float newQuality) {
        if (newQuality < 0)
        {
            quality = 0;
            return;
        }
        if (newQuality > 2)
        {
            quality = 2;
            return;
        }
        quality = newQuality;
    }
}
