using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{

    public SetSong setSong;
    public EnableMusic enableMusic;
    public PlaySnippet playSnippet;

    void Start()
    {
        StartCoroutine(WaitToStartMusic());
    }

    IEnumerator WaitToStartMusic()
    {
        yield return new WaitForSeconds(0.2f);
        playSnippet.playButton = true;
        playSnippet.PlayMenuMusic();
    }
}
