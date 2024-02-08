using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishStation : Station
{
    private List<GameObject> rhythms = new List<GameObject>();
    private GameObject plate;
    private GameObject bar;
    [SerializeField] private GameObject sponge;
    public randomPlateSprite randPlateSprite;
    [SerializeField] private float scale = 1;
    private Bounds objectBounds;
    private float bottomY;
    private int i = 0;
    private bool animationInProgress = false;
    private Vector3 intitialScale, initialPos;
    public GameObject ready;
    private Animator plateAnimator;

    // Start is called before the first frame update
    void Start()
    {
        plate = transform.Find("Plate").gameObject;
        bar = transform.Find("Bar").gameObject;

        plateAnimator = plate.GetComponent<Animator>();

        foreach (Transform rhythm in transform.Find("Rhythms"))
        {
            rhythms.Add(rhythm.gameObject);
        }

        rhythms[0].SetActive(true);

        intitialScale = plate.transform.localScale;
        initialPos = plate.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rhythms[0].activeSelf)
        {
            ready.SetActive(false);
        }
        if (!animationInProgress && running)
        {
            objectBounds = plate.GetComponent<Renderer>().bounds;
            bottomY = objectBounds.min.y;

            if (rhythms[i].activeSelf && (bar.transform.position.y - bottomY < 0.01))
            {
                HitDetector hitDetector = rhythms[i].GetComponentInChildren<HitDetector>();
                if (hitDetector == null)
                {
                    Debug.Log("No hit detector?");
                } else {
                    ReservoirManager.GetPlates().Add(hitDetector.CreateReservoirPlate());
                }

                plateAnimator.Play("MovePlateOffscreen");
                animationInProgress = true;
                rhythms[i].SetActive(false);
                sponge.SetActive(false);
                if (i + 1 < rhythms.Count)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }           
            }
        }
    }

    public void RefreshPlate()
    {
        randPlateSprite.RandomizeSprite();
        scalePlate();
        plateAnimator.Play("MovePlateOnscreen");
    }

    public void EndPlateMovement()
    {
        animationInProgress = false;
        rhythms[i].SetActive(true);
        rhythms[i].GetComponentInChildren<HitDetector>().SetAllSmudgesVisibleAndActive();
        sponge.SetActive(true);
    }

    private void scalePlate()
    {
        Vector3 newScale = new Vector3(intitialScale.x * scale, intitialScale.y, intitialScale.z * scale);
        
        plate.transform.localScale = newScale;
        plate.transform.position = new Vector3(initialPos.x, initialPos.y, initialPos.z);
        
        bar.GetComponent<UpDownMovement>().UpdatePlateBounds();
    }

    public GameObject GetPlate() { return plate; }
    public GameObject GetBar() { return bar; }
    public GameObject GetSponge() { return sponge; }
    public float GetScale() { return scale; }
    public void SetScale(float scale) { this.scale = scale; }
}