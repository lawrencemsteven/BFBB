using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeMover : MonoBehaviour
{
    public Transform sponge;
    public Transform bar;
    public float moveSpeed = 0.05f; // Adjust this value to control movement speed


    private Bounds barBounds;
    Vector3 initialPosition;
    private Vector3 lastMousePosition;
    private float spongeXPosition;
    private bool mouseIsMoving = false;

    private void Start()
    {
        if (bar != null)
        {
            // Calculate the allowed range for sponge movement along the X-axis based on the bar's bounds
            barBounds = bar.GetComponent<Renderer>().bounds;

            initialPosition = sponge.position;
            initialPosition.x = bar.position.x;
            sponge.position = initialPosition;
        }
        lastMousePosition = Input.mousePosition;
        spongeXPosition = sponge.position.x;
    }

    private void Update()
    {
        Vector3 newPosition = sponge.position;

        // Get the current mouse position
        Vector3 currentMousePosition = Input.mousePosition;

        // Check if the mouse has moved
        if (currentMousePosition != lastMousePosition)
        {
            mouseIsMoving = true;

            // Calculate the target X position based on the sponge's current position and inverted mouse input
            float targetXPosition = newPosition.x - (currentMousePosition.x - lastMousePosition.x) * moveSpeed * Time.deltaTime* .005f;

            // Clamp the target X position within the bounds of the bar
            targetXPosition = Mathf.Clamp(targetXPosition, barBounds.min.x, barBounds.max.x);

            // Update the "sponge" position
            newPosition.x = targetXPosition;

            lastMousePosition = currentMousePosition;
        }
        else
        {
            if (mouseIsMoving)
            {
                // Store the current X position if the mouse was moving in the previous frame
                spongeXPosition = sponge.position.x;
            }

            // When the mouse is not moving, make the sponge remain at the stored X position
            newPosition.x = spongeXPosition;
        }

        // Set the Y position to match the Y position of the "bar"
        newPosition.y = bar.position.y;

        // Update the "sponge" position
        sponge.position = newPosition;
    }
}