using UnityEngine;

public static class Stations
{
    private static DishStation dish;
    private static PancakeStation pancake;
    private static CoffeeStation coffee;
    private static PrepStation prep;

    //YEAH THIS FUCKIN BLOWS WE GOT TWO WEEKS LEFT CRY ABOUT IT
    public static void InitializeStations()
    {
        dish = null;
        pancake = null;
        coffee = null;
        prep = null;
    }

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

    public static PancakeStation Pancake
    {
        get
        {
            if (pancake is null)
            {
                pancake = Object.FindObjectOfType<PancakeStation>();

                if (pancake is null)
                {
                    return null;
                }
            }

            return pancake;
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