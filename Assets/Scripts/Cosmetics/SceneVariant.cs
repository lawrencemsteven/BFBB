using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVariant : AssetVariant
{
    [SerializeField] private string targetScene;

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
        Debug.Log("Unloading...");
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(targetScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("Unload complete!");
    }

    private IEnumerator loadScene()
    {
        Debug.Log("Loading...");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("Load complete!");
    }
}
