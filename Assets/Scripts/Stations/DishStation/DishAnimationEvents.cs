using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishAnimationEvents : MonoBehaviour
{
    public void RefreshPlate()
    {
        DishStationManager.Instance.RefreshPlate();
    }

    public void EndPlateMovement()
    {
        DishStationManager.Instance.EndPlateMovement();
    }
}
