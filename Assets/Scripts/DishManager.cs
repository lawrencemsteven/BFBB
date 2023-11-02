using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public GameObject[] rhythms;
    public GameObject[] plates;
    public GameObject[] bars;
    public GameObject[] sponges;
    private Animator anim;
    private Bounds objectBounds;
    private float bottomY;
    private int i = 0;
    private float animationTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Decrease the timer in each frame
        if (animationTimer > 0.0f)
        {
            animationTimer -= Time.deltaTime;
        }

        // Only check for animation conditions if the timer has run out
        if (animationTimer <= 0.0f)
        {
            objectBounds = plates[i].GetComponent<Renderer>().bounds;
            bottomY = objectBounds.min.y;

            if (rhythms[i].activeSelf && (bars[i].transform.position.y - bottomY < 0.01))
            {
                Debug.Log("Playing Animation");
                anim = plates[i].GetComponent<Animator>();
                anim.Play("MovePlateOffscreen");

                if (i + 1 < rhythms.Length)
                {
                    rhythms[i].SetActive(false);
                    rhythms[i + 1].SetActive(true);
                    bars[i + 1].SetActive(true);
                    sponges[i + 1].SetActive(true);
                    anim = plates[i + 1].GetComponent<Animator>();
                    anim.Play("MovePlateOnscreen");
                    i++;
                }
                else
                {
                    rhythms[i].SetActive(false);
                    i = 0;
                    rhythms[i].SetActive(true);
                    bars[i].SetActive(true);
                    sponges[i].SetActive(true);
                    anim = plates[i].GetComponent<Animator>();
                    anim.Play("MovePlateOnscreen");
                }

                animationTimer = 1.0f; // Set a delay of 1 second (adjust as needed)
            }
        }
    }

}
