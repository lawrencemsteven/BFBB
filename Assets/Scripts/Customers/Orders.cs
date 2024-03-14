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
        NONE,
        CHOCOLATE_CHIP,
        WHIPPED_CREAM,
        SYRUP_OLD_FASHIONED,
        FRUIT
    }

    public class Order
    {
        MainCourse mainCourse;
        int mainCourseCount;
        List<Topping> toppings;        
        static Dictionary<MainCourse, int> mainCourseMaximums = new Dictionary<MainCourse, int>
        {
            {MainCourse.PANCAKE, 4},
            {MainCourse.WAFFLE, 2}
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
            toppings = new List<Topping> { Topping.CHOCOLATE_CHIP, Topping.CHOCOLATE_CHIP, Topping.CHOCOLATE_CHIP, Topping.CHOCOLATE_CHIP };
        }

        public static Order GenerateOrder()
        {
            MainCourse mainCourse = (MainCourse)UnityEngine.Random.Range(0, (int)MainCourse.NUM_COURSES);
            int mainCourseCount = UnityEngine.Random.Range(1, mainCourseMaximums[mainCourse] + 1);
            List<Topping> toppings = new List<Topping>();

            for (int i = 0; i < 4; i++)
            {
                toppings.Add((Topping) UnityEngine.Random.Range(1,5));
            }

            return new Order(mainCourse, mainCourseCount, toppings);
        }
        public override string ToString()
        {
            if(mainCourseCount == 0)
            {
                return "";
            }

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

        public void ClearOrder()
        {
            mainCourseCount = 0;

            foreach(Topping t in Enum.GetValues(typeof(Topping)))
            {
                if (this.toppings.Contains(t))
                {
                    RemoveTopping(t);
                }
            }
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
                return false;
            }

            if (order1.mainCourseCount != order2.mainCourseCount)
            {
                return false;
            }

            if (!new HashSet<Topping>(order1.toppings).SetEquals(order2.toppings))
            {
                return false;
            }

            return true;
        }
    }
}