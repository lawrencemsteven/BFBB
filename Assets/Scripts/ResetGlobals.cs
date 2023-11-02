using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetGlobals : MonoBehaviour
{
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "" + GlobalVariables.score;
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void reset()
    {
        GlobalVariables.notesHit = 0;
        GlobalVariables.notesMissed = 0;
        GlobalVariables.score = 0;
        GlobalVariables.missCounter = 0;
    }
}
