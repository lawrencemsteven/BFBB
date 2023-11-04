using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    private int currentScore = 0;
    private int test = 0;
    public TMP_Text txt;

    void Start()
    {
        txt.text = "Score: " + currentScore.ToString();
    }
    // Start is called before the first frame update
    void Update()
    {
        currentScore = GlobalVariables.score;
        txt.text = "Score: " + currentScore.ToString();
    }
}
