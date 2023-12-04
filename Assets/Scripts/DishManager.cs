using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public GameObject[] rhythms;
    public GameObject[] plates;
    public GameObject[] bars;
    public GameObject[] sponges;
    public UpDownMovement[] upDownMovements;
    public randomPlateSprite randPlateSprite;
    public float scale = 1;
    public UpDownMovement upDownMovement1, upDownMovement2, upDownMovement3;
    private Animator anim;
    private Bounds objectBounds;
    private float bottomY;
    private int i = 0;
    private bool animationInProgress = false;
    private Vector3 intitialScale, initialPos;
    public GameObject ready;
    // Start is called before the first frame update
    void Start()
    {
        intitialScale = plates[0].transform.localScale;
        initialPos = plates[0].transform.position;
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
                scalePlates();
                animationInProgress = true;
                Invoke("ContinueDelay", 0.15f);
                rhythms[i].SetActive(false);
                sponges[i].SetActive(false);
                if (i + 1 < rhythms.Length)
                {
                    i++;
                }
                else
                {
                    i = 1;
                }

                if (i == 12)
                {
                    PlateMarker.activate = false;
                }
                rhythms[i].SetActive(true);
                sponges[i].SetActive(true);
                anim = plates[i].GetComponent<Animator>();
                randPlateSprite.RandomizeSprite();
                anim.Play("MovePlateOnscreen");
            }
        }
    }

    private void scalePlates()
    {
        foreach (GameObject plate in plates)
        {
            Vector3 newScale = new Vector3(intitialScale.x * scale, intitialScale.y, intitialScale.z * scale);
            
            plate.transform.localScale = newScale;
            plate.transform.position = new Vector3(initialPos.x, initialPos.y, initialPos.z);
        }

        foreach (UpDownMovement upDownMovement in upDownMovements)
        {
            upDownMovement.UpdatePlateBounds();
        }
    }

    private void ContinueDelay()
    {
        animationInProgress = false;
    }
}