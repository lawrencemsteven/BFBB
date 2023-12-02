using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHitDetector : MonoBehaviour
{
    public AudioSource hihat;
    private bool smudgeCollision = false;
    private bool spongeCollision = false;
    // Function called when a collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Smudge"
        if (collision.gameObject.CompareTag("Sponge"))
        {
            spongeCollision = true;
        }
        if (collision.gameObject.CompareTag("Smudge") && spongeCollision)
        {
            hihat.Play();
            GlobalVariables.score += 1;
            GlobalVariables.streak += 1;
            GlobalVariables.notesHit += 1;
            collision.gameObject.SetActive(false);
            Debug.Log("Hit!!");
        }

    }
}
