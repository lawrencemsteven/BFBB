using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxVariant : AssetVariant
{
    [SerializeField] private Material skybox;

    public override void Apply()
    {
        RenderSettings.skybox = skybox;
    }

    public override void Unapply()
    {
        return;
    }
}
