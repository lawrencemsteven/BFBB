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
        List<ReservoirItem> items = getReservoirItems();
        int i = 0;
        int max = items.Count;
        foreach (Transform child in stackParent)
        {
            if (i >= max)
            {
                Destroy(child.gameObject);
            }
            i++;
        }
        int j = 0;
        Vector3 netOffset = new Vector3(0, 0, 0);
        foreach (ReservoirItem itemStats in items)
        {
            if (j >= i)
            {
                GameObject newItem = Instantiate(reservoirItem, stackParent);
                newItem.transform.localPosition = netOffset;
                setupDisplay(newItem, itemStats);
                newItem.SetActive(true);
            }
            j++;
            netOffset += offset;
        }
    }

    protected virtual void setupDisplay(GameObject item, ReservoirItem itemStats)
    {
        float quality = itemStats.GetDisplayQuality();
        Color qualityColor = crossfadeColors(quality, perfectQualityColor, minQualityColor, overQualityColor);
        item.GetComponent<MeshRenderer>().material.color = qualityColor;
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
