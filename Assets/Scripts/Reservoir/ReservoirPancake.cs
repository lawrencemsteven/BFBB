using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirPancake : ReservoirItem
{
    private GameObject pancake;

    public ReservoirPancake(float newQuality, GameObject inputPancake) : base(newQuality)
    {
        pancake = GameObject.Instantiate(inputPancake);
        pancake.SetActive(false);
    }

    public GameObject GetPancake()
    {
        return pancake;
    }
}
