using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateReservoirDisplay : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    [SerializeField] private Transform stackParent;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        ReservoirManager.GetPlates().onReservoirUpdated.AddListener(RedoDisplay);
        plate.SetActive(false);
    }

    void RedoDisplay()
    {
        foreach (Transform child in stackParent)
        {
            Destroy(child.gameObject);
        }
        List<ReservoirPlate> plates = ReservoirManager.GetPlates().GetAll();
        Vector3 netOffset = new Vector3(0,0,0);
        foreach (ReservoirPlate plateStats in plates)
        {
            GameObject newPlate = Instantiate(plate, stackParent);
            newPlate.transform.localPosition = netOffset;
            newPlate.SetActive(true);
            netOffset += offset;
        }
    }
}
