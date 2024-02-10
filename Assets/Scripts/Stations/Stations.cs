using UnityEngine;

public static class Stations
{
    private static DishStation dish;
    private static CoffeeStation coffee;
    private static PrepStation prep;
    public static DishStation Dish
    {
        get
        {
            if (dish is null)
            {
                dish = Object.FindObjectOfType<DishStation>();

                if (dish is null)
                {
                    return null;
                }
            }

            return dish;
        }
    }

    public static CoffeeStation Coffee
    {
        get
        {
            if (coffee is null)
            {
                coffee = Object.FindObjectOfType<CoffeeStation>();

                if (coffee is null)
                {
                    return null;
                }
            }

            return coffee;
        }
    }

    public static PrepStation Prep 
    {
        get
        {
            if (prep is null)
            {
                prep = Object.FindObjectOfType<PrepStation>();

                if (prep is null)
                {
                    return null;
                }
            }

            return prep;
        }
    }
}