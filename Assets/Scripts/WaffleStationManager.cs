using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class WaffleStationManager : MonoBehaviour
{

    public Camera stationCamera;
    public Animator waffleMakerAnim;
    public float bpm = 135f;
    public WaffleBatterPour waffleBatterPour;
    public fmodTimer timer;
    public GameObject pancakePrefab, waffle, waffleFill, marker1, marker2, marker3, marker4, marker5, marker6, shape1, shape2, shape3, shape4, shape5, shape6;
    private int positionFlipped = 0, positionPancakeSpawn = 0, positionPancakeFlip = 0;
    private float elapsedTime = 0, timeToComplete;
    private bool pancakeFlipped = false, waffleFlipped = false, pointsSet = false;
    public TextMeshProUGUI countdown, pancakeCountdown;
    public IsShapeCovered isShapeCovered;
    private ShapeGenerator sg1, sg2, sg3, sg4, sg5, sg6;

    private Animator pancake1Anim;
    private GameObject pancake1, pancake2, pancake3, pancake4, pancake5, pancake6;
    private List<ShapeGenerator> sgs = new List<ShapeGenerator>();
    private List<GameObject> pancakes = new List<GameObject>();
    private List<GameObject> markers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        timeToComplete = (8 / bpm) * 60;
        waffleBatterPour.isClosed = false;
        SetLists();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pointsSet)
        {
            isShapeCovered.SetPoints();
            pointsSet = true;
        }
        elapsedTime += Time.deltaTime;
        waffleMakerAnim.SetTrigger("Open");
        
        if (elapsedTime >= timeToComplete)
        {
            if (isShapeCovered.GetNumOfHitColliders().ToArray()[0] <10)
            {
                pancake1 = Instantiate(pancakePrefab, shape1.transform.position, shape1.transform.rotation);
                pancake1Anim = pancake1.GetComponent<Animator>();
                positionPancakeSpawn = timer.position;
            }
            if (isShapeCovered.GetNumOfHitColliders().ToArray()[1] < 10)
            {
                pancake2 = Instantiate(pancakePrefab, shape2.transform.position, shape2.transform.rotation);
            }
            if (isShapeCovered.GetNumOfHitColliders().ToArray()[2] < 10)
            {
                pancake3 = Instantiate(pancakePrefab, shape3.transform.position, shape3.transform.rotation);
            }
            if (isShapeCovered.GetNumOfHitColliders().ToArray()[3] < 10)
            {
                pancake4 = Instantiate(pancakePrefab, shape4.transform.position, shape4.transform.rotation);
            }
            if (isShapeCovered.GetNumOfHitColliders().ToArray()[4] < 10)
            {
                pancake5 = Instantiate(pancakePrefab, shape5.transform.position, shape5.transform.rotation);
            }
            if (isShapeCovered.GetNumOfHitColliders().ToArray()[5] < 10)
            {
                pancake6 = Instantiate(pancakePrefab, shape6.transform.position, shape6.transform.rotation);
            }

            elapsedTime = 0;
            //isShapeCovered.SetPoints();
            for (int i = 0; i < isShapeCovered.GetNumOfHitColliders().Count; i++)
            {
                if (isShapeCovered.GetNumOfHitColliders()[i] >= 10)
                {
                    sgs[i].UpdateMarkerPosition(0);
                }
            }
        }
        else
        {
            sg1.UpdateMarkerPosition(elapsedTime);
            sg2.UpdateMarkerPosition(elapsedTime);
            sg3.UpdateMarkerPosition(elapsedTime);
            sg4.UpdateMarkerPosition(elapsedTime);
            sg5.UpdateMarkerPosition(elapsedTime);
            sg6.UpdateMarkerPosition(elapsedTime);
        }

        if ((Input.GetKeyDown(KeyCode.F)) && (!pancakeFlipped) && (positionPancakeSpawn != 0))
        {
            if (timer.OnXBars(positionPancakeSpawn, timer.position, 2))
            {
                GlobalVariables.score += 1;
            }
            positionPancakeFlip = timer.position;
            pancakeFlipped = true;
            pancake1Anim.SetTrigger("FlipTrigger");
            pancake1Anim.SetBool("isFlipped", true);
        }

        else if ((Input.GetKeyDown(KeyCode.F)) && (pancakeFlipped) && (positionPancakeSpawn != 0))
        {
            if (timer.OnXBars(positionPancakeFlip, timer.position, 2))
            {
                GlobalVariables.score += 1;
            }
            pancakeFlipped = true;
            pancake1Anim.SetTrigger("FlipTrigger");
            pancake1Anim.SetBool("isFlipped", false);
        }

        if ((waffleBatterPour.isClosed) && (!waffleFlipped) && (Input.GetKeyDown(KeyCode.Space)))
        {
            positionFlipped = timer.position;
            if (timer.OnXBars(waffleBatterPour.positionClosed, positionFlipped, 1))
            {
                GlobalVariables.score += 1;
            }
            waffleFlipped = true;
            waffleMakerAnim.SetTrigger("Flip");
        }

        else if ((waffleBatterPour.isClosed) && (waffleFlipped) && (Input.GetKeyDown(KeyCode.Space)))
        {
            if (timer.OnXBars(positionFlipped, timer.position, 1))
            {
                GlobalVariables.score += 1;
            }

            waffleMakerAnim.SetTrigger("FlipBack");
            waffleFlipped = false;
            waffleBatterPour.isClosed = false;
            waffleFill.SetActive(false);
            waffle.SetActive(true);
            waffleMakerAnim.SetTrigger("Open");
            StartCoroutine(CountBeatsToResetWaffleMaker());
        }

        IEnumerator CountBeatsToResetWaffleMaker()
        {
            float beatInterval = 60f / bpm;

            for (int count = 8; count >= 1; count--)
            {
                Debug.Log($"Beat {count}");
                if (count == 1)
                {
                    waffle.SetActive(false);
                    waffleBatterPour.ResetBatter();
                    waffleFill.SetActive(true);
                }
                yield return new WaitForSeconds(beatInterval);
            }

            Debug.Log("Counting finished!");
        }

    }

    void SetLists()
    {
        sg1 = shape1.GetComponent<ShapeGenerator>();
        sg2 = shape2.GetComponent<ShapeGenerator>();
        sg3 = shape3.GetComponent<ShapeGenerator>();
        sg4 = shape4.GetComponent<ShapeGenerator>();
        sg5 = shape5.GetComponent<ShapeGenerator>();
        sg6 = shape6.GetComponent<ShapeGenerator>();
        sgs.Add(sg1);
        sgs.Add(sg2);
        sgs.Add(sg3);
        sgs.Add(sg4);
        sgs.Add(sg5);
        sgs.Add(sg6);
        pancakes.Add(pancake1);
        pancakes.Add(pancake2);
        pancakes.Add(pancake3);
        pancakes.Add(pancake4);
        pancakes.Add(pancake5);
        pancakes.Add(pancake6);
        markers.Add(marker1);
        markers.Add(marker2);
        markers.Add(marker3);
        markers.Add(marker4);
        markers.Add(marker5);
        markers.Add(marker6);
    }
}
