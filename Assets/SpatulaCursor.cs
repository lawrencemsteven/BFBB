using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaCursor : MonoBehaviour
{
    private float offset = 0.70f;
    private MeshRenderer mesh;
    private bool paused = false;
    [SerializeField] private float distanceThreshold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spatula = transform.GetChild(0).gameObject;
        mesh = spatula.GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }
        if (!paused)
        {

            if (!Stations.Pancake.IsRunning())
            {
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                mesh.enabled = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                mesh.enabled = false;
            }

            Ray ray = Stations.Pancake.GetAssociatedCamera().ScreenPointToRay(Input.mousePosition);
            Vector3 targetPosition = ray.GetPoint(offset);
            transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        }
    }
}
