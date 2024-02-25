using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSwapper : MonoBehaviour
{
    public string assetName;

    private Dictionary<string, AssetVariant> variantOptions;
    [SerializeField] private AssetVariant defaultVariant;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    void Awake()
    {
        foreach (AssetVariant variant in variantOptions.Values)
        {
            variant.SetFilterAndRenderer(meshFilter, meshRenderer);
        }

        AssetManager.RegisterAsset(this);
    }

    public ICollection<string> GetVariantOptionNames() {
        return variantOptions.Keys;
    }

    public void ApplyVariant(string swapName)
    {
        variantOptions[swapName].Apply();
    }
}
