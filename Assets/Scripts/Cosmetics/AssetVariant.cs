using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AssetSwapper))]
public class AssetVariant : MonoBehaviour
{
    public string variantName;
    public GameObject prefab;
    private GameObject instantiated;

    public virtual void Apply()
    {
        if (prefab == null)
        {
            Debug.LogWarning("Variant " + variantName + " on " + name + " has null prefab!");
            return;
        }
        instantiated = Instantiate(prefab, transform);
    }

    public virtual void Unapply()
    {
        if (instantiated != null)
        {
            Destroy(instantiated);
        }
    }
}