using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirPlate : ReservoirItem
{
    public ReservoirPlate(float newQuality) : base(newQuality)
    {
        Debug.Log("Added plate with quality: " + newQuality);
    }
}
