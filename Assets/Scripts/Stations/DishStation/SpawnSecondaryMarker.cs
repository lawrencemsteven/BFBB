using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSecondaryMarker : MonoBehaviour
{
    [SerializeField] private GameObject secondaryMarker;

    private GameObject markerInstance;

    void Start()
    {
        markerInstance = Instantiate(secondaryMarker, transform.position, transform.rotation);
    }

    void OnDestroy()
    {
        if (markerInstance is not null)
        {
            Destroy(markerInstance.gameObject);
        }
    }

    void Update()
    {
        markerInstance.transform.position = transform.position;
    }
}
