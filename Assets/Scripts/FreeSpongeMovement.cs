using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSpongeMovement : MonoBehaviour
{
    public Camera camera;
    private Vector3 lastMousePosition;
    private float spongeXPosition;
    private float spongeYPosition;
    Vector3 initialPosition;
    private bool mouseIsMoving = false;
    public float moveSpeed = 5f; // Adjust this value to control movement speed
    private Transform sponge;
    Vector3 mousePos;

    private void Start()
    {
        sponge = transform;
        initialPosition = sponge.position;
        lastMousePosition = Input.mousePosition;
        spongeXPosition = sponge.position.x;
        spongeYPosition = sponge.position.y;
        
    }

    void Update()
    {
        Vector3 newPosition = sponge.position;
        Vector3 currentMousePosition = Input.mousePosition;

        if (currentMousePosition != lastMousePosition)
        {
            mouseIsMoving = true;

            // Adjusted movement calculations based on sponge rotation
            float targetXPosition = newPosition.x - (currentMousePosition.x - lastMousePosition.x) * moveSpeed * Time.deltaTime * .05f;
            float targetYPosition = newPosition.y + (currentMousePosition.y - lastMousePosition.y) * moveSpeed * Time.deltaTime * .05f;

            newPosition.x = targetXPosition;
            newPosition.y = targetYPosition;

            lastMousePosition = currentMousePosition;
        }
        else
        {
            if (mouseIsMoving)
            {
                spongeXPosition = sponge.position.x;
                spongeYPosition = sponge.position.y;
            }

            newPosition.x = spongeXPosition;
            newPosition.y = spongeYPosition;
        }

        sponge.position = newPosition;
    }
}