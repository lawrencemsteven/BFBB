using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stevelation : MonoBehaviour
{
    public enum StevelationSpeeds
    {
        Quick,
        Slow
    }

    public static float Lerp(StevelationSpeeds startSpeed, StevelationSpeeds stopSpeed, float amount)
    {
        if (startSpeed == StevelationSpeeds.Slow && stopSpeed == StevelationSpeeds.Slow)
        {
            float transitionAmount = Mathf.Lerp(0.0f, Mathf.PI, amount);
            return 0.5f * Mathf.Sin(transitionAmount - Mathf.PI / 2.0f) + 0.5f;
        }
        else if (startSpeed == StevelationSpeeds.Quick && stopSpeed == StevelationSpeeds.Quick)
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
        else if (startSpeed == StevelationSpeeds.Slow && stopSpeed == StevelationSpeeds.Quick)
        {
            float transitionAmount = Mathf.Lerp(0.0f, Mathf.PI / 2.0f, amount);
            return Mathf.Sin(transitionAmount - Mathf.PI / 2.0f) + 1.0f;
        }
        else if (startSpeed == StevelationSpeeds.Quick && stopSpeed == StevelationSpeeds.Slow)
        {
            float transitionAmount = Mathf.Lerp(0.0f, Mathf.PI / 2.0f, amount);
            return Mathf.Sin(transitionAmount);
        }

        return -1.0f;
    }
}
