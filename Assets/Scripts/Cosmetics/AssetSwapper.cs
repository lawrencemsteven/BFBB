using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSwapper : MonoBehaviour
{
    public string assetName;

    private Dictionary<string, AssetVariant> variantOptions = new();
    [SerializeField] private AssetVariant defaultVariant;
    [SerializeField] private GameObject placeholderChild;
    private string currentSwap;

    void Awake()
    {
        currentSwap = defaultVariant.variantName;
        foreach (AssetVariant variant in GetComponents<AssetVariant>())
        {
            variantOptions.Add(variant.variantName, variant);
        }

        AssetManager.RegisterAsset(this);
    }

    void Start()
    {
        if (placeholderChild != null)
        {
            Destroy(placeholderChild);
        }
        defaultVariant.Apply();
    }

    public ICollection<string> GetVariantOptionNames() {
        return variantOptions.Keys;
    }

    public void ApplyVariant(string swapName)
    {
        variantOptions[currentSwap].Unapply();
        variantOptions[swapName].Apply();
        currentSwap = swapName;
    }
}
