using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlaySnippet : MonoBehaviour
{

    private SongInfo info = new SongInfo();
    public SetSong setSong;
    public List<string> setList = new List<string>();
    public bool shopMenuActive = false;
    
    public void PlaySong() {
        FMOD.Studio.PLAYBACK_STATE state;
        GlobalVariables.preview.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.STOPPED) {
            GlobalVariables.preview.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        } 
        GlobalVariables.preview = FMODUnity.RuntimeManager.CreateInstance(setList[GlobalVariables.songIndex]);
        GlobalVariables.preview.start();
        GlobalVariables.preview.release();

        
    }

    public void StopSong() {
        FMOD.Studio.PLAYBACK_STATE state;
        GlobalVariables.preview.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.STOPPED) {
            GlobalVariables.preview.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void Right() {
        if (GlobalVariables.songIndex < setList.Count - 1) {
            GlobalVariables.songIndex++;
        }
    }

    public void Left() {
        if (GlobalVariables.songIndex >= 1) {
            GlobalVariables.songIndex--;
        }
    }

    public void SetVariables() {
        if (GlobalVariables.songIndex == 0) {
            setSong.song60BPM();
            info.UpdateBPM();
        }
        else if (GlobalVariables.songIndex == 1) {
            setSong.songBoogie();
            info.UpdateBPM();
        }
        else if (GlobalVariables.songIndex == 2) {
            setSong.songRock();
            info.UpdateBPM();
        }
        else if (GlobalVariables.songIndex == 3) {
            setSong.songBumpin();
            info.UpdateBPM();
        }
    }

    public void PlayMenuMusic()
    {
        if(shopMenuActive)
        {
            shopMenuActive = false;
        }
        else
        {
            setSong.songMenu();
            GlobalVariables.preview = FMODUnity.RuntimeManager.CreateInstance("event:/MenuSong");
            GlobalVariables.preview.start();
            GlobalVariables.preview.release();
        }
        
    }

    public void Shop()
    {
        shopMenuActive = true;
    }
}
