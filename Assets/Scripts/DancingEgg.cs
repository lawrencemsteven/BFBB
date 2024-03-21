using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingEgg : MonoBehaviour
{
    private int dancePhase = 0;
    [SerializeField] private float hopHeight = 0.35f;

    void Start()
    {
        StartCoroutine(EggDanceLoop());
    }

    private IEnumerator EggDanceLoop()
    {
        float spb = 1f;
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        while (true)
        {
            spb = GameInfoManager.Instance.Song.GetSecondsPerBeat();
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            yield return new WaitForSeconds(spb);
            switch (dancePhase)
            {
                case 0:
                    StartCoroutine(DanceTwirl(spb));
                    dancePhase++;
                    break;
                case 1:
                    StartCoroutine(DanceHopSide(spb, 1.0f));
                    dancePhase++;
                    break;
                case 2:
                    StartCoroutine(DanceHopSide(spb, -1.0f));
                    dancePhase++;
                    break;
                case 3:
                    StartCoroutine(DanceHop(spb));
                    dancePhase = 0;
                    break;
            }
        }
    }

    private IEnumerator DanceTwirl(float spb)
    {
        float accumulated = 0;
        float lastYaw = 0;

        while (accumulated < spb)
        {
            accumulated += Time.deltaTime;
            float newYaw = 360f * Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Slow, Interpolawrence.InterpolawrenceSpeeds.Slow, accumulated / spb);
            float deltaYaw = newYaw - lastYaw;
            lastYaw = newYaw;
            transform.Rotate(0, deltaYaw, 0);
            yield return null;
        }
        yield return null;
    }

    private IEnumerator DanceHop(float spb)
    {
        float accumulated = 0;
        Vector3 basePosition = transform.position;
        float lastPitch = 0;

        while (accumulated < spb / 2)
        {
            accumulated += Time.deltaTime;
            float newHeight = Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Quick, Interpolawrence.InterpolawrenceSpeeds.Slow, accumulated / (spb/2));
            transform.position = basePosition + new Vector3(0, newHeight * hopHeight, 0);
            float deltaPitch = newHeight - lastPitch;
            lastPitch = newHeight;
            transform.Rotate(deltaPitch * 15f, 0, 0);
            yield return null;
        }

        accumulated = 0;

        while (accumulated < spb / 2)
        {
            accumulated += Time.deltaTime;
            float newHeight = 1 - Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Slow, Interpolawrence.InterpolawrenceSpeeds.Quick, accumulated / (spb / 2));
            transform.position = basePosition + new Vector3(0, newHeight * hopHeight, 0);
            float deltaPitch = newHeight - lastPitch;
            lastPitch = newHeight;
            transform.Rotate(deltaPitch * 15f, 0, 0);
            yield return null;
        }

        transform.position = basePosition;
        yield return null;
    }

    private IEnumerator DanceHopSide(float spb, float direction)
    {
        float accumulated = 0;
        Vector3 basePosition = transform.position;
        float lastRoll = 0;

        while (accumulated < spb / 2)
        {
            accumulated += Time.deltaTime;
            float newHeight = Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Quick, Interpolawrence.InterpolawrenceSpeeds.Slow, accumulated / (spb / 2));
            transform.position = basePosition + new Vector3(0, newHeight * hopHeight * 0.4f, newHeight * hopHeight * 0.25f * direction);
            float deltaRoll = newHeight - lastRoll;
            lastRoll = newHeight;
            transform.Rotate(0, 0, deltaRoll * 15f * direction);
            yield return null;
        }

        accumulated = 0;

        while (accumulated < spb / 2)
        {
            accumulated += Time.deltaTime;
            float newHeight = 1 - Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Slow, Interpolawrence.InterpolawrenceSpeeds.Quick, accumulated / (spb / 2));
            transform.position = basePosition + new Vector3(0, newHeight * hopHeight * 0.4f, newHeight * hopHeight * 0.25f * direction);
            float deltaRoll = newHeight - lastRoll;
            lastRoll = newHeight;
            transform.Rotate(0, 0, deltaRoll * 15f * direction);
            yield return null;
        }

        transform.position = basePosition;
        yield return null;
    }
}
