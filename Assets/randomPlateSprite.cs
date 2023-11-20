using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPlateSprite : MonoBehaviour
{
    public Material actualMaterial;
    public Material[] materialList;
    void Start()
    {
        int upperBound = (materialList.Length - 1);
        int materialNum = Random.Range(0, upperBound);
        actualMaterial.CopyPropertiesFromMaterial(materialList[materialNum]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
