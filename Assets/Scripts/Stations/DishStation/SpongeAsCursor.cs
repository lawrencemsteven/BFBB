using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAsCursor : MonoBehaviour
{
    public Camera dishWashingCamera;
    private float offset = 0.70f;
    private ComposerInterpreter composer;
    private bool isMouseMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        composer = GameObject.FindObjectOfType<ComposerInterpreter>();
        if (composer == null)
        {
            Debug.LogError("Composer not found in the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            isMouseMoving = true;
        }
        else
        {
            isMouseMoving = false;
        }

        Ray ray = dishWashingCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(offset);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Plate" && isMouseMoving)
        {
            composer.spongeOnPlate();
        }
    }
}
