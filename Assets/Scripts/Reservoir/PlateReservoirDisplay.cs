using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateReservoirDisplay : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    [SerializeField] private Transform stackParent;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Color perfectQualityColor;
    [SerializeField] private Color minQualityColor;

    void Start()
    {
        ReservoirManager.GetPlates().onReservoirUpdated.AddListener(RedoDisplay);
        plate.SetActive(false);
    }

    private void RedoDisplay()
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
            float quality = plateStats.GetDisplayQuality();
            Color qualityColor = crossfadeColors(quality, perfectQualityColor, minQualityColor, minQualityColor);
            newPlate.GetComponent<MeshRenderer>().material.color = qualityColor;
            newPlate.SetActive(true);
            netOffset += offset;
        }
    }

    private Color crossfadeColors(float quality, Color perfectColor, Color minColor, Color maxColor)
    {
        if (quality > 1)
        {
            minColor = maxColor;
            quality = 2 - quality;
        }

        Vector3 perfectVector = new Vector3(perfectColor.r, perfectColor.g, perfectColor.b);
        Vector3 minVector = new Vector3(minColor.r, minColor.g, minColor.b);
        Vector3 crossfadeVector = Vector3.Lerp(minVector, perfectVector, quality);

        float crossfadeR = crossfadeVector.x;
        float crossfadeG = crossfadeVector.y;
        float crossfadeB = crossfadeVector.z;
        return new Color(crossfadeR, crossfadeG, crossfadeB);
    }
}
