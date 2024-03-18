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

    public string[] needsArray = new string[4];
    public GameObject[] signArray = new GameObject[4];

    private float bpm = 135f;
    private int beatsPerMeasure;
    private int currentBeat;

    private int toppingInt;

    private string selected;

    private Vector3 containerPos;
    private Vector3 signPos;
    bool needsGenerated;


    private float distanceFromMug;

    //if lineManager.isEarly() do to early/offbeat else do success
    //randomize number and do pick icon for what each one needs
    //how to each thing appear in sequence
    //after beats/measure are complete rerandomize (if lineManager.GetCurrentBeat = beats per measure?)



    [SerializeField] private GameObject instructions;

    [SerializeField] private int pointTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        coffeePot.SetActive(false);
        cream.SetActive(false);
        sugar.SetActive(false);
        bool needsGenerated = false;

        beatsPerMeasure = (int)GameInfoManager.Instance.Song.GetBeatsPerMeasure();

    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            return;
        }

        currentBeat = lineManager.GetCurrentBeat();
        //Debug.Log("Current Beat is " + currentBeat);

        //LineManager.GetCurrentBeat(){}
        //if current = bpm recreate needs

        if(!needsGenerated) { GenerateNeed();}

        if(currentBeat == beatsPerMeasure)
        {
            //destroy elements in an needsArray;
            //
            needsGenerated = false;
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

            if (Input.GetMouseButton(0))
            {
                if (!coffeeParticle.isPlaying) { coffeeParticle.Play(); }
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

            if (Input.GetMouseButton(0))
            {
                if (!sugarParticle.isPlaying) { sugarParticle.Play(); }
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

            if (Input.GetMouseButton(0))
            {
                if (!creamParticle.isPlaying) { creamParticle.Play(); }
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
        Debug.Log("Entering Generate need");
        for (int i = 0; i < customerMugArray.Length; i++)
        {
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
                signArray[i] = Instantiate(sugar, signPos, Quaternion.Euler(0, 90, 90));
                
            }
            else if (toppingInt == 3)
            {
                needsArray[i] = "CREAM";
                signArray[i] = Instantiate(cream, signPos, Quaternion.identity);
                
            }

            signArray[i].transform.localScale /= 2;
            signArray[i].SetActive(true);
            needsGenerated = true;
        }

    }

}
