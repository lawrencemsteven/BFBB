using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAssetSwapping : MonoBehaviour
{
    [SerializeField] private string testTarget;
    [SerializeField] private float swapTimer = 2.0F;

    void Start()
    {
        StartCoroutine(testRoutine());
    }

    private IEnumerator testRoutine()
    {
        string asset = testTarget;
        ICollection<string> swaps = AssetManager.GetAvailableSwapsForAsset(asset);

        while (true)
        {
            foreach (string swap in swaps) {
                AssetManager.ApplyAssetSwap(asset, swap);
                yield return new WaitForSeconds(swapTimer);
            }
        }
    }
}
