using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightMovement : MonoBehaviour
{
    public GameObject griddle;
    public float bpm = 139f;
    private float speed;       // Speed of the object's downward movement
    private float teleportX;
    private Bounds objectBounds;
    private Vector3 initialPosition;    // Store the initial position



    private void Start()
    {
        objectBounds = griddle.GetComponent<BoxCollider>().bounds;

        teleportX = objectBounds.min.x;
        initialPosition = objectBounds.max;
        speed = (objectBounds.min.x - objectBounds.max.x) / (4.0f / (bpm / 60f));
    }

    private void Update()
    {
        speed = (objectBounds.min.x - objectBounds.max.x) / (4.0f / (bpm / 60f));
        // Move the object downward on the Y-axis
        transform.position += Vector3.right * speed * Time.deltaTime;
        // Check if the object has reached the teleport Y-coordinate
        if (transform.position.x <= teleportX)
        {
            // Teleport the object back to the top of the screen
            transform.position = new Vector3(initialPosition.x, transform.position.y, transform.position.z);
        }
    }
}
