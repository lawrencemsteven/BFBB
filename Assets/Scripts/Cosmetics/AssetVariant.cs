using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AssetSwapper))]
public class AssetVariant : MonoBehaviour
{
    public string name;
    public GameObject prefab;
    private GameObject instantiated;

    public virtual void Apply()
    {
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