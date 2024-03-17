using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSecondaryMarker : MonoBehaviour
{
    [SerializeField] private GameObject secondaryMarker;

    void Start()
    {
        secondaryMarker = Instantiate(secondaryMarker, transform.position, transform.rotation);
    }

    void OnDestroy()
    {
        Destroy(secondaryMarker);
    }

    void Update()
    {
        secondaryMarker.transform.position = transform.position;
    }
}
