using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeStation : Station
{
    [SerializeField] private GameObject coffeePot;
    [SerializeField] private ParticleSystem coffeeParticle;
    [SerializeField] private GameObject sugar;
    [SerializeField] private ParticleSystem sugarParticle;
    [SerializeField] private GameObject cream;
    [SerializeField] private ParticleSystem creamParticle;
    [SerializeField] private GameObject[] customerMugArray;
    [SerializeField] private GameObject ring;

    public string[] needsArray = new string[4];
    public GameObject[] signArray = new GameObject[4];


    public float secondsPerBeat;
    public int beatsPerMeasure;
    public int currentBeat;
    public float currentBeatTime;

    private string selected;

    private Vector3 containerPos;
    private Vector3 signPos;
    bool needsGenerated;

    private float acceptableDistance = .3f;
    private float accetableTiming = .5f;

    Color lightRed = new Color(.5f, 0f, 0f, 1f);
    Color changeColor = new Color(.1f, 0f, 0f, 1f);

    [SerializeField] private GameObject instructions;

    [SerializeField] private int pointTotal = 0;

    // Start is called before the first frame update
   public override void Initialize()
    {
        coffeePot.SetActive(false);
        cream.SetActive(false);
        sugar.SetActive(false);
        needsGenerated = false;

        secondsPerBeat = GameInfoManager.Instance.Song.GetSecondsPerBeat();
        beatsPerMeasure = (int)GameInfoManager.Instance.Song.GetBeatsPerMeasure();
        currentBeat = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            return;
        }

        currentBeatTime += Time.deltaTime;

        if (currentBeat == 1 && needsGenerated == false) 
        {
            GenerateNeed();
            ring.GetComponentInChildren<Renderer>().material.color = lightRed;
            ring.transform.position = customerMugArray[0].transform.position;


        }

        if (currentBeatTime >= secondsPerBeat)
        {
            currentBeatTime = 0;

            if (currentBeat == beatsPerMeasure)
            {
                currentBeat = 1;
                needsGenerated = false;
            }
            else
            {
                ring.GetComponentInChildren<Renderer>().material.color = Color.white;
                currentBeat += 1;
                ring.transform.position = customerMugArray[currentBeat-1].transform.position;
                ring.GetComponentInChildren<Renderer>().material.color = lightRed;

            }
        }
        else
        {
            colorItems();
        }


        
        PourControl();
        PourRelease();
    }

    void PourControl()
    {
        containerPos = Input.mousePosition;

        if (Input.GetKey(KeyCode.A))
        {
            selected = "COFFEE";

            if (!coffeePot.activeSelf) { coffeePot.SetActive(true); }

            coffeePot.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 2f));

            if (Input.GetMouseButtonDown(0))
            {
                if (!coffeeParticle.isPlaying) { coffeeParticle.Play(); }

                RangeCheck(coffeeParticle.transform.position.x) ;
       
            }

            else if (Input.GetMouseButtonUp(0))
            {
                if (coffeeParticle.isPlaying) { coffeeParticle.Stop(); }
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {
            selected = "SUGAR";
            if (!sugar.activeSelf) { sugar.SetActive(true); }

            sugar.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 2f));

            if (Input.GetMouseButtonDown(0))
            {
                if (!sugarParticle.isPlaying) { sugarParticle.Play(); }

                RangeCheck(sugarParticle.transform.position.x);
    
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (sugarParticle.isPlaying) { sugarParticle.Stop(); }
            }
        }


        else if (Input.GetKey(KeyCode.D))
        {
            selected = "CREAM";
            if (!cream.activeSelf) { cream.SetActive(true); }

            cream.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(containerPos.x, containerPos.y, 2f));

            if (Input.GetMouseButtonDown(0))
            {
                if (!creamParticle.isPlaying) { creamParticle.Play(); }

                RangeCheck(creamParticle.transform.position.x);
 
            }

            else if (Input.GetMouseButtonUp(0))
            {
                if (creamParticle.isPlaying) { creamParticle.Stop(); }
            }
        }
    }
    
    void PourRelease()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            selected = "";

            if (coffeePot.activeSelf) { coffeePot.SetActive(false); }
            
            if (coffeeParticle.isPlaying) { coffeeParticle.Stop(); }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            selected = "";

            if (sugar.activeSelf) { sugar.SetActive(false); }

            if(sugarParticle.isPlaying) { sugarParticle.Stop(); }
        }

        if (Input.GetKeyUp(KeyCode.D)) 
        {
            selected = "";

            if(cream.activeSelf) { cream.SetActive(false); }

            if (creamParticle.isPlaying) { creamParticle.Stop(); }
        }
    }

    void GenerateNeed()
    {
        for (int i = 0; i < customerMugArray.Length; i++)
        {
            if (signArray[i] is not null) { Destroy(signArray[i]); }

            int toppingInt = Random.Range(1, 4);
            signPos = customerMugArray[i].transform.position;
            signPos.y += .2f;
            
            if(toppingInt == 1)
            {
                needsArray[i] = "COFFEE";
                signArray[i] = Instantiate(coffeePot, signPos, Quaternion.identity) ;
       
            }
            else if (toppingInt == 2) 
            {
                needsArray[i] = "SUGAR";
                signPos.y += .2f;
                signArray[i] = Instantiate(sugar, signPos, Quaternion.Euler(0, 90, 90));
                
            }
            else if (toppingInt == 3)
            {
                needsArray[i] = "CREAM";
                signArray[i] = Instantiate(cream, signPos, Quaternion.identity);
                
            }

            signArray[i].transform.localScale /= 2;
            signArray[i].SetActive(true);
        }
        needsGenerated = true;
    }

    void RangeCheck(float posX)
    {
        float actualDistance = posX - customerMugArray[currentBeat-1].transform.position.x;

        if(actualDistance<= acceptableDistance && actualDistance >= -acceptableDistance){ CorrectItemCheck(currentBeat - 1); }

    }

    void CorrectItemCheck(int mugIndex)
    {
        if (needsArray[mugIndex].Equals(selected) && currentBeatTime <= accetableTiming)
        {
            //add success sfx here
            pointTotal += 1;
        }

        //late sound effects go here

        needsArray[mugIndex] = "";
        Destroy(signArray[mugIndex]);
        signArray[mugIndex] = null;
    }

    void colorItems()
    {

        if (ring.GetComponentInChildren<Renderer>().material.color != Color.green && (secondsPerBeat - currentBeatTime) <= accetableTiming)
        {
            ring.GetComponentInChildren<Renderer>().material.color = Color.green;
        }
        else
        {
            ring.GetComponentInChildren<Renderer>().material.color += changeColor;
        }
    }

}
