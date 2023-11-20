using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private bool animationInProgress = false;
    public GameObject ready;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!rhythms[0].activeSelf)
        {
            ready.SetActive(false);
        }
        if (!animationInProgress)
        {
            objectBounds = plates[i].GetComponent<Renderer>().bounds;
            bottomY = objectBounds.min.y;

            if (rhythms[i].activeSelf && (bars[i].transform.position.y - bottomY < 0.01))
            {
                Debug.Log("Playing Animation");
                anim = plates[i].GetComponent<Animator>();
                anim.Play("MovePlateOffscreen");
                animationInProgress = true;
                Invoke("ContinueDelay", 0.15f);
                rhythms[i].SetActive(false);
                if (i + 1 < rhythms.Length)
                {
                    i++;
                }
                else
                {
                    i = 1;
                }


                rhythms[i].SetActive(true);
                anim = plates[i].GetComponent<Animator>();
                anim.Play("MovePlateOnscreen");
            }
        }
    }

    private void ContinueDelay()
    {
        animationInProgress = false; // Reset the animation flag

    }
}