using System;
using System.Collections.Generic;
using System.Linq;
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

        public Order()
        {
            mainCourse = MainCourse.PANCAKE;
            mainCourseCount = 1;
            toppings = new List<Topping> { Topping.CHOCOLATE_CHIP };
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

            // temporary for prep station testing
            return new Order();
        }
        public override string ToString()
        {
            return $"{mainCourseCount} {mainCourse}s with {string.Join(", ", toppings)}";
        }

        public void SetMainCourse(MainCourse mainCourse)
        {
            this.mainCourse = mainCourse;
            mainCourseCount = 1;
        }

        public void SetMainCourseAmount(int amount)
        {
            mainCourseCount = amount;
        }

        public void AddMainCourseAmount(int amount)
        {
            mainCourseCount += amount;
        }

        public void RemoveMainCourseAmount(int amount)
        {
            mainCourseCount -= amount;
        }

        public void AddTopping(Topping topping)
        {
            toppings.Add(topping);
        }

        public void RemoveTopping(Topping topping)
        {
            toppings.Remove(topping);
        }

        public MainCourse GetMainCourse() { return mainCourse; }
        public int GetMainCourseCount() { return mainCourseCount; }
        public List<Topping> GetToppings() { return toppings; }

        public static bool Equals(Order order1, Order order2)
        {
            if (order1 is null || order2 is null)
            {
                if (order1 is null && order2 is null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (order1.mainCourse != order2.mainCourse)
            {
                Debug.Log("main courses unequal");
                return false;
            }

            if (order1.mainCourseCount != order2.mainCourseCount)
            {
                Debug.Log("main course count unequal");
                return false;
            }

            if (!new HashSet<Topping>(order1.toppings).SetEquals(order2.toppings))
            {
                Debug.Log("toppings unequal");
                return false;
            }

            return true;
        }
    }
}