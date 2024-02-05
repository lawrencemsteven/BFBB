using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ReservoirStack<T> where T : ReservoirItem
{
    private List<T> items;
    private int maxSize;

    public UnityEvent onReservoirUpdated = new();

    public ReservoirStack(int maxSize)
    {
        items = new List<T>();
        this.maxSize = maxSize;
        UpdateReservoir();
    }

    public bool Add(T item)
    {
        if (HasRoom())
        {
            items.Add(item);
            UpdateReservoir();
            return true;
        } else
        {
            return false;
        }
    }

    public T Pop()
    {
        T item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        UpdateReservoir();
        return item;
    }

    public List<T> PopMany(int amount)
    {
        List<T> subList = items.GetRange(items.Count - amount, amount);
        items.RemoveRange(items.Count - amount, amount);
        UpdateReservoir();
        return subList;
    }

    public int Count() { return items.Count; }

    // Pretty please don't change the list when you use this.
    public List<T> GetAll() { return items; }

    public bool HasRoom() { return Count() < maxSize; }
    public int GetMaxSize() { return maxSize; }
    public void SetMaxSize(int newSize) { maxSize = newSize; }

    public float GetAverageDisplayQuality()
    {
        float totalQuality = 0f;
        foreach (T item in items)
        {
            totalQuality += item.GetDisplayQuality();
        }
        return totalQuality / Count();
    }

    private void UpdateReservoir()
    {
        if (onReservoirUpdated != null)
        {
            onReservoirUpdated.Invoke();
        }
    }
}
