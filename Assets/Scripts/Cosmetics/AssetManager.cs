using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetManager
{
    private static Dictionary<string, List<AssetSwapper>> assets = new();

    public static void RegisterAsset(AssetSwapper asset) {
        string id = asset.assetName;
        if (!assets.ContainsKey(id))
        {
            assets.Add(id, new List<AssetSwapper>());
        }
        assets[id].Add(asset);
    }

    public static ICollection<string> GetAssetNames()
    {
        return assets.Keys;
    }

    public static ICollection<string> GetAvailableSwapsForAsset(string targetAssetName)
    {
        if (assets.ContainsKey(targetAssetName))
        {
            AssetSwapper asset = assets[targetAssetName][0];
            return asset.GetVariantOptionNames();
        } else
        {
            Debug.LogWarning("Invalid asset name " + targetAssetName);
            return null;
        }
    }

    public static void ApplyAssetSwap(string assetName, string variantName)
    {
        if (!assets.ContainsKey(assetName))
        {
            Debug.LogWarning("Invalid asset name " + assetName);
            return;
        }
        foreach (AssetSwapper swapper in assets[assetName])
        {
            swapper.ApplyVariant(variantName);
        }
    }
}
