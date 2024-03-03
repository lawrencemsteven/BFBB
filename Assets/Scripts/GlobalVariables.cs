using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static int camState = 3;
    public static int score = 0;
    public static int notesMissed = 0;
    public static int notesHit = 0;
    public static int missCounter = 0;
    public static int streak = 0;
    public static string songChoice = "event:/60BPM";
    public static uint bpm = 60u;
}
