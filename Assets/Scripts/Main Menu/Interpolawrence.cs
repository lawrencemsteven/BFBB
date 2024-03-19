using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolawrence : MonoBehaviour
{
    public enum InterpolawrenceSpeeds
    {
        Quick,
        Slow
    }

    public static float Lerp(InterpolawrenceSpeeds startSpeed, InterpolawrenceSpeeds stopSpeed, float amount)
    {
        if (startSpeed == InterpolawrenceSpeeds.Slow && stopSpeed == InterpolawrenceSpeeds.Slow)
        {
            float transitionAmount = Mathf.Lerp(0.0f, Mathf.PI, amount);
            return 0.5f * Mathf.Sin(transitionAmount - Mathf.PI / 2.0f) + 0.5f;
        }
        else if (startSpeed == InterpolawrenceSpeeds.Quick && stopSpeed == InterpolawrenceSpeeds.Quick)
        {
            float transitionAmount = Mathf.Lerp(0.0f, Mathf.PI, amount);
            if (transitionAmount < 0.5f)
            {
                return Mathf.Sin(transitionAmount) * 0.5f;
            }
            else
            {
                return Mathf.Abs(Mathf.Sin(transitionAmount) * 0.5f - 1.0f);
            }
        }
        else if (startSpeed == InterpolawrenceSpeeds.Slow && stopSpeed == InterpolawrenceSpeeds.Quick)
        {
            float transitionAmount = Mathf.Lerp(0.0f, Mathf.PI / 2.0f, amount);
            return Mathf.Sin(transitionAmount - Mathf.PI / 2.0f) + 1.0f;
        }
        else if (startSpeed == InterpolawrenceSpeeds.Quick && stopSpeed == InterpolawrenceSpeeds.Slow)
        {
            float transitionAmount = Mathf.Lerp(0.0f, Mathf.PI / 2.0f, amount);
            return Mathf.Sin(transitionAmount);
        }

        return -1.0f;
    }
}
