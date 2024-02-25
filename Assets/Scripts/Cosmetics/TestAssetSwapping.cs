using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAssetSwapping : MonoBehaviour
{
    [SerializeField] private string testTarget;

    void Start()
    {
        StartCoroutine(testRoutine());
    }

    private IEnumerator testRoutine()
    {
        string asset = testTarget;
        ICollection<string> swaps = AssetManager.GetAvailableSwapsForAsset(asset);
        int i = 0;

        while (true)
        {
            foreach (string swap in swaps) {
                AssetManager.ApplyAssetSwap(asset, swap);
            }
            yield return new WaitForSeconds(2);
        }
    }
}
