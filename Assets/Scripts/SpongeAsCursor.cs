using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAsCursor : MonoBehaviour
{
    public Camera dishWashingCamera;
    private float offset = 0.70f;
    private Composer composer;
    // Start is called before the first frame update
    void Start()
    {
        composer = GameObject.FindObjectOfType<Composer>();
        if (composer == null)
        {
            Debug.LogError("Composer not found in the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = dishWashingCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(offset);
        transform.localPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Plate")
        {
            composer.SpongeOnPlate();
        }
    }
}
