using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HiHatFmod : MonoBehaviour
{
    //[FMODUnity.EventReference]
    public string hihat;
    public bool debugMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayHiHat()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(hihat, gameObject);
    }

    public void PlaySqueak()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/EarlyDish", gameObject);
    }

    public void PlayScrape()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/LateDish", gameObject);
    }

    public void DishStreak1()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/DishStreak1", gameObject);
    }

    public void DishStreak2()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/DishStreak2", gameObject);
    }

    public void DishStreak3()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/DishStreak3", gameObject);
    }

    public void PancakeStreak1()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PancakeStreak1", gameObject);
    }

    public void PancakeStreak2()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PancakeStreak2", gameObject);
    }

    public void PancakeStreak3()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PancakeStreak3", gameObject);
    }

    public void CoffeeStreak1()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CoffeeStreak1", gameObject);
    }

    public void CoffeeStreak2()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CoffeeStreak2", gameObject);
    }

    public void CoffeeStreak3()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CoffeeStreak3", gameObject);
    }

    public void WaffleStreak1()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/WaffleStreak1", gameObject);
    }

    public void WaffleStreak2()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/WaffleStreak2", gameObject);
    }

    public void WaffleStreak3()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/WaffleStreak3", gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(debugMode)
        {
            debugMode = false;
            PlayHiHat();   
        }
    }
}
