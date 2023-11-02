using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance; // Singleton instance

    // Set these values to control the camera shake intensity and duration
    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.2f;

    private Transform camTransform;
    private Vector3 originalPosition;

    private void Awake()
    {
        Instance = this; // Create a singleton instance
        camTransform = transform;
    }

    private void Start()
    {
        originalPosition = camTransform.localPosition; // Store the original position
    }

    // Function to shake the camera
    public void Shake()
    {
        if (shakeDuration > 0)
        {
            StartCoroutine(DoShake());
        }
    }

    private IEnumerator DoShake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeAmount;

            camTransform.localPosition = randomPoint;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        camTransform.localPosition = originalPosition;
    }
}