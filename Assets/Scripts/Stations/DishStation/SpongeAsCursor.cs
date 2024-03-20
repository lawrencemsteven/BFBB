using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAsCursor : MonoBehaviour
{
    public Camera dishWashingCamera;
    private float offset = 0.70f;
    [SerializeField] private ComposerInterpreter composer;
    private bool isMouseMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (!Stations.Dish.IsRunning())
        {
            return;
        }

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
