using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public GameObject plate;
    public float bpm = 135f;
    private float speed;       // Speed of the object's downward movement
    private float teleportY;
    private Bounds objectBounds;
    private Vector3 initialPosition;    // Store the initial position
    public bool reset = false;



    private void Start()
    {
        objectBounds = plate.GetComponent<Renderer>().bounds;
        teleportY = objectBounds.min.y;
        initialPosition = objectBounds.max;
        speed = (objectBounds.max.y - objectBounds.min.y) / (4.0f / (bpm / 60f));
    }

    private void Update()
    {
        speed = (objectBounds.max.y - objectBounds.min.y) / (4.0f / (bpm / 60f));
        // Move the object downward on the Y-axis
        transform.position += Vector3.down * speed * Time.deltaTime;
        // Check if the object has reached the teleport Y-coordinate
        if (transform.position.y <= teleportY)
        {
            // Teleport the object back to the top of the screen
            reset = true;
            transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
        }
    }

    public void UpdatePlateBounds()
    {
        objectBounds = plate.GetComponent<Renderer>().bounds;
        teleportY = objectBounds.min.y;
        initialPosition = objectBounds.max;
        speed = (objectBounds.max.y - objectBounds.min.y) / (4.0f / (bpm / 60f));
    }
}
