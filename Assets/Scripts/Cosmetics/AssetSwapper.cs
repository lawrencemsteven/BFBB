using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSwapper : MonoBehaviour
{
    public string assetName;

    private Dictionary<string, AssetVariant> variantOptions = new();
    private string currentSwap;

    void Awake()
    {
        currentSwap = "Default";
        foreach (AssetVariant variant in GetComponents<AssetVariant>())
        {
            variantOptions.Add(variant.variantName, variant);
        }

        AssetManager.RegisterAsset(this);
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        ApplyVariant("Default");
    }

    public ICollection<string> GetVariantOptionNames() {
        return variantOptions.Keys;
    }

    public void ApplyVariant(string swapName)
    {
        if (variantOptions.ContainsKey(swapName))
        {
            variantOptions[currentSwap].Unapply();
            variantOptions[swapName].Apply();
            currentSwap = swapName;
        } else
        {
            Debug.LogWarning("Attempted to add invalid variant " + swapName + " to asset " + assetName);
        }
    }
}
