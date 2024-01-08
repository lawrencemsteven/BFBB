using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Streak : MonoBehaviour
{

    private int currentStreak = 0;
    public TMP_Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt.text = "Streak: " + currentStreak.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentStreak = GlobalVariables.streak;
        txt.text = "Streak: " + currentStreak.ToString();
    }
}
