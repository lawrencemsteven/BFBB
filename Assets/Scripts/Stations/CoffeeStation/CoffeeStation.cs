using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeStation : Station
{
    [SerializeField] private GameObject coffeeStationDeco;
    [SerializeField] private GameObject customerMug;
    [SerializeField] private GameObject coffeePot;
    [SerializeField] private ParticleSystem coffeeParticle;
    [SerializeField] private GameObject sugar;
    [SerializeField] private ParticleSystem sugarParticle;
    [SerializeField] private GameObject cream;
    [SerializeField] private ParticleSystem creamParticle;

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private int current = 0;
    private float waypointRadius = .1f;

    private float baseMoveSpeed;

    [SerializeField] private float bpm = 135f;
    private float travelDistance;
    private float time;

    private Vector2 iniMousePos;
    private Vector3 coffeeMousePos;
    private Vector3 iniPotPos;
    private Vector3 iniCreamPos;
    private Vector3 initialCoffeePourBarSize;
    private Vector3 initialCreamPourBarSize;

    [SerializeField] private float coffeePourTime = 0f;
    private float idealCoffeePourTime;
    private float coffeePourPercentage;
    [SerializeField] private GameObject coffeePourBar;

    [SerializeField] private float creamPourTime = 0f;
    private float idealCreamPourTime;
    private float creamPourPercentage;
    [SerializeField] private GameObject creamPourBar;

    private float coffeeDistanceFromMug;
    private float creamDistanceFromMug;
    private float sugarDistanceFromMug;

    private bool customerMugMoving = false;
    private bool pouring = false;
    private bool sugarEnabled;

    [SerializeField] private GameObject instructions;

    [SerializeField] private int pointTotal = 0;

    // Start is called before the first frame update
   public override void Initialize()
    {
        time = 8f / (bpm / 60f);
        travelDistance = Vector3.Distance(waypoints[0].transform.position, waypoints[waypoints.Length - 1].transform.position);
        baseMoveSpeed = travelDistance / time;

        idealCoffeePourTime = Vector3.Distance(waypoints[1].transform.position, waypoints[2].transform.position) / (3 * baseMoveSpeed);
        idealCreamPourTime = Vector3.Distance(waypoints[3].transform.position, waypoints[4].transform.position) / (3 * baseMoveSpeed);

        iniPotPos = coffeePot.transform.position;
        iniCreamPos = cream.transform.position;

        initialCoffeePourBarSize = coffeePourBar.transform.localScale;
        initialCreamPourBarSize = creamPourBar.transform.localScale;

        coffeePourBar.SetActive(false);
        creamPourBar.SetActive(false);

        coffeePot.SetActive(false);
        cream.SetActive(false);
        sugar.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            return;
        }

        if (customerMugMoving)
        {
            if (Vector3.Distance(waypoints[current].transform.position, customerMug.transform.position) < waypointRadius)
            {
                current++;


                if (current >= waypoints.Length)
                {
                    //reset stats on everything
                    coffeePourPercentage = 100 - System.Math.Abs(1 - (coffeePourTime / idealCoffeePourTime)) * 100;

                    if (coffeePourPercentage >= 90)
                    {
                        pointTotal += 4;
                    }
                    else if (coffeePourPercentage >= 80)
                    {
                        pointTotal += 2;
                    }

                    creamPourPercentage = 100 - System.Math.Abs(1 - (creamPourTime / idealCreamPourTime)) * 100;

                    if (creamPourPercentage >= 90)
                    {
                        pointTotal += 3;
                    }
                    else if (creamPourPercentage >= 80)
                    {
                        pointTotal += 1;
                    }

                    customerMugMoving = false;
                    coffeePourTime = 0;
                    creamPourTime = 0;
                    current = 0;

                    customerMug.transform.position = waypoints[current].transform.position;

                    coffeePourBar.transform.localScale = initialCoffeePourBarSize;
                    coffeePourBar.SetActive(false);

                    creamPourBar.transform.localScale = initialCreamPourBarSize;
                    creamPourBar.SetActive(false);

                    coffeeStationDeco.SetActive(true);
                    coffeePot.SetActive(false);
                    cream.SetActive(false);
                    sugar.SetActive(false);

                    instructions.SetActive(false);
                }
            }
            customerMug.transform.position = Vector3.MoveTowards(customerMug.transform.position, waypoints[current].transform.position, baseMoveSpeed * Time.deltaTime);


        }

        if (current == 2)
        {
            coffeeMousePos = Input.mousePosition;
            coffeePot.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(coffeeMousePos.x, coffeeMousePos.y, 2f));


            if (pouring)
            {

                if (!coffeeParticle.isPlaying)
                {
                    coffeeParticle.Play();
                }

                coffeeDistanceFromMug = System.Math.Abs(coffeeParticle.transform.position.x - customerMug.transform.position.x);

                if (coffeeDistanceFromMug <= .15)
                {
                    coffeePourTime += Time.deltaTime;
                    coffeePourBar.transform.localScale += new Vector3(0f, .005f, 0f);
                    //add sound effects for when the coffee is hitting or not
                }


                if (coffeePourTime >= idealCoffeePourTime && coffeePourTime <= idealCoffeePourTime + .2f)
                {
                    coffeePourBar.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    coffeePourBar.GetComponent<Renderer>().material.color = Color.red;
                }
            }

        }
        else if (current == 3)
        {
            if(coffeeParticle.isPlaying) { coffeeParticle.Stop(); }

            coffeePot.transform.position = iniPotPos;
            pouring = false;
        }
        else if (current == 4)
        {
            coffeeMousePos = Input.mousePosition;
            cream.transform.position = associatedCamera.ScreenToWorldPoint(new Vector3(coffeeMousePos.x, coffeeMousePos.y, 2f));

            if (pouring)
            {

                if(!creamParticle.isPlaying) { creamParticle.Play(); }

                creamDistanceFromMug = System.Math.Abs(creamParticle.transform.position.x - customerMug.transform.position.x);

                if (creamDistanceFromMug <= .15)
                {
                    creamPourTime += Time.deltaTime;
                    creamPourBar.transform.localScale += new Vector3(0f, .005f, 0f);
                    //add sound effects for cream pouring music
                }

                if (creamPourTime >= idealCreamPourTime && creamPourTime <= creamPourTime + .2f)
                {
                    creamPourBar.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    creamPourBar.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        else if (current == 5)
        {
            if(creamParticle.isPlaying) { creamParticle.Stop(); }

            cream.transform.position = iniCreamPos;
            pouring = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!customerMugMoving)
            {
                customerMugMoving = true;
                sugarEnabled = true;

                coffeePourBar.SetActive(true);
                coffeePourBar.GetComponent<Renderer>().material.color = Color.red;

                creamPourBar.SetActive(true);
                creamPourBar.GetComponent<Renderer>().material.color = Color.red;

                coffeeStationDeco.SetActive(false);
                coffeePot.SetActive(true);
                sugar.SetActive(true);
                cream.SetActive(true);

                instructions.SetActive(true);

            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //pouring starts
            if (current == 2 || current == 4)
            {
                pouring = true;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //pouring stops
            if (current == 2 || current == 4)
            {
                pouring = false;
                coffeePot.GetComponentInChildren<ParticleSystem>().Stop();
                cream.GetComponentInChildren<ParticleSystem>().Stop();
            }
        }




        if (Input.GetKeyDown(KeyCode.Space) && sugarEnabled && customerMugMoving)
        {
            sugarEnabled = false;
            sugarParticle.Emit(20);
            sugarDistanceFromMug = System.Math.Abs(sugarParticle.transform.position.x - customerMug.transform.position.x);

            if (sugarDistanceFromMug <= .1)
            {
                pointTotal += 3;
            }
            else if (sugarDistanceFromMug < .25)
            {
                pointTotal += 1;
            }
        }
    }
}
