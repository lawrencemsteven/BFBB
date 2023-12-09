using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggRotate : MonoBehaviour
{
    public GameObject camera;
    public GameObject egg;
    public float targetRotation = 180f;
    private bool isRotating = false;

    void Update()
    {
        if (camera.activeSelf && !isRotating)
        {
            RotateEgg(targetRotation);
        }
        else if (!camera.activeSelf && isRotating)
        {
            float currentRotation = egg.transform.eulerAngles.y;
            float rotationDifference = targetRotation - currentRotation;
            RotateEgg(rotationDifference);
        }
    }

    void RotateEgg(float rotationAmount)
    {
        egg.transform.Rotate(0f, rotationAmount, 0f);
        isRotating = true;
    }
}
