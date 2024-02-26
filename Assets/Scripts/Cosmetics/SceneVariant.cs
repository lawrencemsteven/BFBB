using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVariant : AssetVariant
{
    [SerializeField] private string targetScene;
    private string previousScene;

    public void Apply()
    {
        StartCoroutine(swapScene());
    }

    private IEnumerator swapScene()
    {
        AsyncOperation asyncLoad;

        if (previousScene != null)
        {
            Debug.Log("Unloading...");
            asyncLoad = SceneManager.UnloadSceneAsync(previousScene);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            Debug.Log("Unload complete!");
        }

        Debug.Log("Loading...");
        asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("Load complete!");
        previousScene = targetScene;
    }
}
