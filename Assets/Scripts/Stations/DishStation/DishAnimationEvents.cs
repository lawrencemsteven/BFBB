using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishAnimationEvents : MonoBehaviour
{
    public void RefreshPlate()
    {
        Stations.Dish.RefreshPlate();
    }

    public void EndPlateMovement()
    {
        Stations.Dish.EndPlateMovement();
    }
}
