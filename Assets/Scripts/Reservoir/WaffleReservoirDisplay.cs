using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleReservoirDisplay : ReservoirDisplay
{
    private ReservoirStack<ReservoirWaffle> reservoirStack;

    new void Start()
    {
        reservoirStack = ReservoirManager.GetWaffles();
        reservoirStack.onReservoirUpdated.AddListener(RedoDisplay);
        base.Start();
    }

    protected override List<ReservoirItem> getReservoirItems()
    {
        List<ReservoirItem> output = new List<ReservoirItem>();
        foreach (ReservoirWaffle waffle in reservoirStack.GetAll())
        {
            output.Add(waffle);
        }
        return output;
    }

    protected override void setupDisplay(GameObject item, ReservoirItem itemStats)
    {
        return;
    }
}
