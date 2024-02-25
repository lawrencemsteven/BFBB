using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AssetSwapper))]
public class AssetVariant : MonoBehaviour
{
    public string name;
    public Mesh mesh;
    public Material material;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    public void SetFilterAndRenderer(MeshFilter filter, MeshRenderer renderer)
    {
        meshFilter = filter;
        meshRenderer = renderer;
    }

    public void Apply()
    {
        meshFilter.mesh = mesh;
        meshRenderer.material = material;
    }
}