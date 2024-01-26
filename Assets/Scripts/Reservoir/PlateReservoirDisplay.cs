using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateReservoirDisplay : ReservoirDisplay
{
    private ReservoirStack<ReservoirPlate> reservoirStack;

    void Start()
    {
        reservoirStack = ReservoirManager.GetPlates();
        reservoirStack.onReservoirUpdated.AddListener(RedoDisplay);
        base.Start();
    }

    protected override List<ReservoirItem> getReservoirItems()
    {
        List<ReservoirItem> output = new List<ReservoirItem>();
        foreach (ReservoirPlate plate in reservoirStack.GetAll())
        {
            output.Add(plate);
        }
        return output;
    }
}
