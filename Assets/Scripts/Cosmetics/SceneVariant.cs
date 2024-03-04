using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVariant : AssetVariant
{
    [SerializeField] private string targetScene;
    private bool loaded = false;

    public override void Apply()
    {
        StartCoroutine(loadScene());
    }

    public override void Unapply()
    {
        StartCoroutine(unloadScene());
    }

    private IEnumerator unloadScene()
    {
        if (!loaded)
        {
            Debug.Log("Already unloaded. Not unloading again.");
            yield break;
        }
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(targetScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        loaded = false;
    }

    private IEnumerator loadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        loaded = true;
    }
}
