using System.Collections;
using System.Collections.Generic;
using Orders;
using UnityEngine;

public class ToppingCoordinateCollider : CoordinateCollider
{
    public Topping topping;

    void Start()
    {
        int value = Random.Range(0,4);
        
        switch (value)
        {
            case 0:
                topping = Topping.BUTTER;
                break;
            case 1:
                topping = Topping.SYRUP_OLD_FASHIONED;
                break;
            case 2:
                topping = Topping.CHOCOLATE_CHIP;
                break;
            case 3:
                topping = Topping.WHIPPED_CREAM;
                break;
        }
    }
}
