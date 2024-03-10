using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeStation : Station
{
    [SerializeField] private GameObject customerMug;
    [SerializeField] private GameObject coffeePot;
    [SerializeField] private ParticleSystem coffeeParticle;
    [SerializeField] private GameObject sugar;
    [SerializeField] private ParticleSystem sugarParticle;
    [SerializeField] private GameObject cream;
    [SerializeField] private ParticleSystem creamParticle;

    public float bpm = 135f;

    private Vector3 containerPos;

    private float coffeeDistanceFromMug;
    private float creamDistanceFromMug;
    private float sugarDistanceFromMug;



    [SerializeField] private GameObject instructions;

    [SerializeField] private int pointTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
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

        PourControl();
        PourRelease();
    }

    void PourControl()
    {
        containerPos = Input.mousePosition;

        if (Input.GetKey(KeyCode.A))
        {
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
            if (coffeePot.activeSelf) { coffeePot.SetActive(false); }
            
            if (coffeeParticle.isPlaying) { coffeeParticle.Stop(); }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (sugar.activeSelf) { sugar.SetActive(false); }

            if(sugarParticle.isPlaying) { sugarParticle.Stop(); }
        }

        if (Input.GetKeyUp(KeyCode.D)) 
        {
            if(cream.activeSelf) { cream.SetActive(false); }

            if (creamParticle.isPlaying) { creamParticle.Stop(); }
        }
    }
}
