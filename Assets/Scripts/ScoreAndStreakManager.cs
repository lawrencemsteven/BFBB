using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreAndStreakManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scoreUpdate(int Score, int multiplier = 1) {
        int actualScoreUpdate = Score * multiplier;
        GlobalVariables.score += actualScoreUpdate;
        GlobalVariables.streak += 1;
        Composer.Instance.PlayDishStreak1();
        if (GlobalVariables.streak == 3) {
            switch (GlobalVariables.currentStation) {
                case "Dish":
                    Composer.Instance.PlayDishStreak1();
                    break;
                case "Pancake":
                    Composer.Instance.PlayPancakeStreak1();
                    break;
                case "Coffee":
                    Composer.Instance.PlayCoffeeStreak1();
                    break;
                case "Waffle":
                    Composer.Instance.PlayWaffleStreak1();
                    break;
            }
        }
        else if (GlobalVariables.streak == 6) {
            switch (GlobalVariables.currentStation) {
                case "Dish":
                    Composer.Instance.PlayDishStreak2();
                    break;
                case "Pancake":
                    Composer.Instance.PlayPancakeStreak2();
                    break;
                case "Coffee":
                    Composer.Instance.PlayCoffeeStreak2();
                    break;
                case "Waffle":
                    Composer.Instance.PlayWaffleStreak2();
                    break;
            }
        }
        else if (GlobalVariables.streak == 3) {
            switch (GlobalVariables.currentStation) {
                case "Dish":
                    Composer.Instance.PlayDishStreak3();
                    break;
                case "Pancake":
                    Composer.Instance.PlayPancakeStreak3();
                    break;
                case "Coffee":
                    Composer.Instance.PlayCoffeeStreak3();
                    break;
                case "Waffle":
                    Composer.Instance.PlayWaffleStreak3();
                    break;
            }
        }
    }

    public void resetStreak() {
        GlobalVariables.streak = 0;
    }
}
