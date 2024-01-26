using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirManager : MonoBehaviour
{
    private static ReservoirStack<ReservoirPlate> plates;

    [SerializeField] private static int maxPlates = 30;

    void Start()
    {
        plates = new ReservoirStack<ReservoirPlate>(maxPlates);
    }

    public static ReservoirStack<ReservoirPlate> GetPlates()
    {
        return plates;
    }
}
