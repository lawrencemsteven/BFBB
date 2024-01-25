using System;
using System.Collections.Generic;
using UnityEngine;

namespace Orders
{
    public enum MainCourse
    {
        PANCAKE,
        WAFFLE,
        NUM_COURSES
    }

    public enum Topping
    {
        CHOCOLATE_CHIP,
        PECAN,
        MARSHMALLOW,
        BACON_BIT,
        BANANA_SLICE,
        BLUEBERRY,
        STRAWBERRY,
        WHIPPED_CREAM,
        SYRUP_OLD_FASHIONED,
        SYRUP_BLUEBERRY,
        SYRUP_STRAWBERRY,
        JELLY,
        NUTELLA,
        BUTTER,
        CREAM_CHEESE,
        NUM_TOPPINGS
    }

    public class Order
    {
        MainCourse mainCourse;
        int mainCourseCount;
        List<Topping> toppings;        
        static Dictionary<MainCourse, int> mainCourseMaximums = new Dictionary<MainCourse, int>
        {
            {MainCourse.PANCAKE, 8},
            {MainCourse.WAFFLE, 3}
        };
        public Order(MainCourse mainCourse, int mainCourseCount, List<Topping> toppings)
        {
            this.mainCourse = mainCourse;
            this.mainCourseCount = mainCourseCount;
            this.toppings = toppings;
        }

        public static Order GenerateOrder()
        {
            MainCourse mainCourse = (MainCourse)UnityEngine.Random.Range(0, (int)MainCourse.NUM_COURSES);
            int mainCourseCount = UnityEngine.Random.Range(1, mainCourseMaximums[mainCourse] + 1);
            List<Topping> toppings = new List<Topping>();

            for (int i = 0; i < (int)Topping.NUM_TOPPINGS; i++)
            {
                if (UnityEngine.Random.Range(0, 4) == 1)
                {
                    toppings.Add((Topping) i);
                }
            }

            return new Order(mainCourse, mainCourseCount, toppings);
        }
        public override string ToString()
        {
            return $"{mainCourseCount} {mainCourse}s with {string.Join(", ", toppings)}";
        }
    }
}