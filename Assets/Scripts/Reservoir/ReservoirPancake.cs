using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirPancake : ReservoirItem
{
    private GameObject pancake;

    public ReservoirPancake(float newQuality, GameObject inputPancake) : base(newQuality)
    {
        pancake = inputPancake;
        inputPancake.SetActive(false);
    }

    public GameObject GetPancake()
    {
        return pancake;
    }
}
