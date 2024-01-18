using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirStack<T> : MonoBehaviour where T : ReservoirItem
{
    private List<T> items;

    public ReservoirStack()
    {
        items = new List<T>();
    }

    public void Add(T item)
    {
        items.Add(item);
    }

    public T Pop()
    {
        T item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    public int Count() { return items.Count; }

    // Pretty please don't change the list when you use this.
    public List<T> GetAll() { return items; }
}
