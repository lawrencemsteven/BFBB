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
            asyncLoad = SceneManager.UnloadSceneAsync(previousScene);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        previousScene = targetScene;
    }
}
