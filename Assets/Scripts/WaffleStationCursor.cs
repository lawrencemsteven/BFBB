using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleStationCursor : MonoBehaviour
{
    public Camera waffleCamera;
    private float offset = 0.70f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = waffleCamera.ScreenPointToRay(Input.mousePosition);

        Debug.Log(waffleCamera.transform.position.z - transform.position.z);
        Vector3 targetPosition = ray.GetPoint(offset);
        transform.localPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
    }
}
