using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    private float speed;       // Speed of the object's downward movement
    private float teleportY;
    private Bounds objectBounds;
    public GameObject plate;
    private Vector3 initialPosition;    // Store the initial position

    private void Start()
    {
        objectBounds = plate.GetComponent<Renderer>().bounds;
        teleportY = objectBounds.min.y;
        speed = (objectBounds.max.y - objectBounds.min.y) / (4.0f / (139f / 60f));
        
        initialPosition = objectBounds.max;
    }

    private void Update()
    {
        // Move the object downward on the Y-axis
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Check if the object has reached the teleport Y-coordinate
        if (transform.position.y <= teleportY)
        {
            // Teleport the object back to the top of the screen
            transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
        }
    }
}
