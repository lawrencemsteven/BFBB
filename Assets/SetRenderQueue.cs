using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderQueue : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null && objectRenderer.sharedMaterial != null)
        {
            objectRenderer.sharedMaterial.renderQueue = 3999;
        }
        else
        {
            Debug.LogWarning("Renderer or material not found on this object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
