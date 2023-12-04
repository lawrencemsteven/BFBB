using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPlateSprite : MonoBehaviour
{
    public Material actualMaterial;
    public Material[] materialList;
    void Start()
    {
        RandomizeSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RandomizeSprite()
    {
        int materialNum = Random.Range(0, materialList.Length);
        actualMaterial.CopyPropertiesFromMaterial(materialList[materialNum]);
    }
}
