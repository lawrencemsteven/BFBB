using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeReservoirDisplay : ReservoirDisplay
{
    private ReservoirStack<ReservoirPancake> reservoirStack;

    new void Start()
    {
        reservoirStack = ReservoirManager.GetPancakes();
        reservoirStack.onReservoirUpdated.AddListener(RedoDisplay);
        base.Start();
    }

    protected override List<ReservoirItem> getReservoirItems()
    {
        List<ReservoirItem> output = new List<ReservoirItem>();
        foreach (ReservoirPancake pancake in reservoirStack.GetAll())
        {
            output.Add(pancake);
        }
        return output;
    }
}
