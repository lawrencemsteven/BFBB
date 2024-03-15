using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DishStationInfo : StationInfo
{
    public DishStationInfo()
    {
        earlySound = "EarlyDish";
        onTimeSound = "HiHat";
        lateSound = "LateDish";
    }
}
