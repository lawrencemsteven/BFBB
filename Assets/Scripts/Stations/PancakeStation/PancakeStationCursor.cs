using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeStationCursor : MonoBehaviour
{
    public ParticleSystem batter;
    public Material squeezed, unsqueezed;
    private float offset = 0.70f;
    private Renderer bottleRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject bottle = transform.GetChild(0).gameObject;

        bottleRenderer = bottle.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!Stations.Pancake.IsRunning())
        {
            return;
        }
        
        var emissionBatter = batter.emission;
        Ray ray = Stations.Pancake.GetAssociatedCamera().ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(offset);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        if (Input.GetMouseButtonDown(0))
        {
            bottleRenderer.material = squeezed;
            emissionBatter.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            emissionBatter.enabled = false;
            bottleRenderer.material = unsqueezed;
        }
    }
}
