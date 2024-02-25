using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSwapper : MonoBehaviour
{
    public int assetId;
    public string assetName;

    private List<AssetVariant> variantOptions;
    [SerializeField] private AssetVariant defaultVariant;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    void Awake()
    {
        foreach (AssetVariant variant in variantOptions)
        {
            variant.SetFilterAndRenderer(meshFilter, meshRenderer);
        }
    }
}
