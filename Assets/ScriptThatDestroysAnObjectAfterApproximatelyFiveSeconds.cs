using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptThatDestroysAnObjectAfterApproximatelyFiveSeconds : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField] private CameraController cameraController;

    void Update()
    {
        if (cameraController.UsingGameCameras())
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}
