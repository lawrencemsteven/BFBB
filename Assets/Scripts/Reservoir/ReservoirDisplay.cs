using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReservoirDisplay : MonoBehaviour
{
    [SerializeField] protected GameObject reservoirItem;
    [SerializeField] protected Transform stackParent;
    [SerializeField] protected Vector3 offset;
    [SerializeField] protected Color perfectQualityColor;
    [SerializeField] protected Color minQualityColor;
    [SerializeField] protected Color overQualityColor;

    protected void Start()
    {
        reservoirItem.SetActive(false);
    }

    protected void RedoDisplay()
    {
        foreach (Transform child in stackParent)
        {
            Destroy(child.gameObject);
        }
        List<ReservoirItem> items = getReservoirItems();
        Vector3 netOffset = new Vector3(0,0,0);
        foreach (ReservoirItem itemStats in items)
        {
            GameObject newItem = Instantiate(reservoirItem, stackParent);
            newItem.transform.localPosition = netOffset;
            float quality = itemStats.GetDisplayQuality();
            Color qualityColor = crossfadeColors(quality, perfectQualityColor, minQualityColor, overQualityColor);
            newItem.GetComponent<MeshRenderer>().material.color = qualityColor;
            newItem.SetActive(true);
            netOffset += offset;
        }
    }

    protected abstract List<ReservoirItem> getReservoirItems();

    private Color crossfadeColors(float quality, Color perfectColor, Color minColor, Color maxColor)
    {
        if (quality > 1)
        {
            minColor = maxColor;
            quality = 2 - quality;
        }

        return Color.Lerp(minColor, perfectColor, quality);
    }
}
