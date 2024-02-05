using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject Camera_3;
    public GameObject Camera_4;
    public GameObject Camera_5;
    public static int camState = 3;
    public static int score = 0;
    public static int notesMissed = 0;
    public static int notesHit = 0;
    public static int missCounter = 0;
    public static int streak = 0;
    public static Vector3 markerPos;
    public static Vector3 mousePos;
    public static bool pancakeStationActive = false;
}
