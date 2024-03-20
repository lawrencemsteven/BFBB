using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSong : MonoBehaviour
{
    public string scene = "";

    // Start is called before the first frame update
    void Start()
    {

    }


    public void startGame()
    {        
        GameInfoManager.Instance.ReadFile();
        //GameInfoManager.Instance.WriteFile();
    }
    public void song60BPM()
    {
        GlobalVariables.songChoice = "event:/60BPM";

        GlobalVariables.songUI = "Boogie Slow";

        GlobalVariables.bpm = 60u;

        GlobalVariables.earlyDish = "EarlyDish";

        GlobalVariables.onTimeDish = "HiHat";

        GlobalVariables.lateDish = "LateDish";

        GlobalVariables.upPrep = "D";
        GlobalVariables.upRightPrep = "C2";
        GlobalVariables.upLeftPrep = "F";
        GlobalVariables.downPrep = "Eb";
        GlobalVariables.downRightPrep = "Ab";
        GlobalVariables.downLeftPrep = "G";
        GlobalVariables.rightPrep = "D";
        GlobalVariables.leftPrep = "C";

        GlobalVariables.volume = "Volume 2";

        GameInfoManager.Instance.Song.UpdateBPM();

    }

    public void songBoogie()
    {
        GlobalVariables.songChoice = "event:/BoogieSong";

        GlobalVariables.songUI = "Boogie Feel";

        GlobalVariables.bpm = 120u;

        GlobalVariables.earlyDish = "EarlyDish";

        GlobalVariables.onTimeDish = "HiHat";

        GlobalVariables.lateDish = "LateDish";

        GlobalVariables.upPrep = "Db";
        GlobalVariables.upRightPrep = "Bb";
        GlobalVariables.upLeftPrep = "F";
        GlobalVariables.downPrep = "F";
        GlobalVariables.downRightPrep = "A";
        GlobalVariables.downLeftPrep = "G";
        GlobalVariables.rightPrep = "D";
        GlobalVariables.leftPrep = "C";

        GlobalVariables.volume = "Volume 1";

        GameInfoManager.Instance.Song.UpdateBPM();
        GameInfoManager.Instance.Dish.SetSounds(GlobalVariables.earlyDish, GlobalVariables.onTimeDish,  GlobalVariables.lateDish);
    }

    public void songRock()
    {
        GlobalVariables.songChoice = "event:/BoogieRock";

        GlobalVariables.songUI = "Boogie Rock";

        GlobalVariables.bpm = 180u;

        GlobalVariables.earlyDish = "EarlyDish3";

        GlobalVariables.onTimeDish = "HiHat3";

        GlobalVariables.lateDish = "LateDish3";

        GlobalVariables.upPrep = "D";
        GlobalVariables.upRightPrep = "B";
        GlobalVariables.upLeftPrep = "E";
        GlobalVariables.downPrep = "Gb";
        GlobalVariables.downRightPrep = "G";
        GlobalVariables.downLeftPrep = "E";
        GlobalVariables.rightPrep = "A";
        GlobalVariables.leftPrep = "D";

        GlobalVariables.volume = "Volume 2";

        GameInfoManager.Instance.Song.UpdateBPM();
        GameInfoManager.Instance.Dish.SetSounds(GlobalVariables.earlyDish, GlobalVariables.onTimeDish,  GlobalVariables.lateDish);

    }

    public void songBumpin()
    {
        GlobalVariables.songChoice = "event:/BoogieSong 2";

        GlobalVariables.songUI = "Boogie Bumpin";

        GlobalVariables.bpm = 135u;

        GlobalVariables.earlyDish = "EarlyDish2";

        GlobalVariables.onTimeDish = "HiHat2";

        GlobalVariables.lateDish = "LateDish2";

        GameInfoManager.Instance.Song.UpdateBPM();
        GameInfoManager.Instance.Dish.SetSounds(GlobalVariables.earlyDish, GlobalVariables.onTimeDish,  GlobalVariables.lateDish);
    }

}