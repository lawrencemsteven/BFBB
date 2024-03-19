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

    public static string earlyDish = "EarlyDish";
    public static string onTimeDish = "HiHat";
    public static string lateDish = "LateDish";

    public static string earlyPancake;
    public static string onTimePancake;
    public static string latePancake;

    public static string earlyPrep;
    public static string onTimePrep;
    public static string latePrep;
    public static string currentStation = "Dish";

    public static string upPrep;
    public static string downPrep;
    public static string leftPrep;
    public static string rightPrep;
    public static string upLeftPrep;
    public static string upRightPrep;
    public static string downLeftPrep;
    public static string downRightPrep;

    public static FMOD.Studio.EventInstance instance;
    public static string volume = "Volume";
}