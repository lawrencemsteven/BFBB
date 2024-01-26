using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirManager : MonoBehaviour
{
    private static ReservoirStack<ReservoirPancake> pancakes;
    private static ReservoirStack<ReservoirPlate> plates;
    private static ReservoirStack<ReservoirWaffle> waffles;

    [SerializeField] private static int maxPancakes = 24;
    [SerializeField] private static int maxPlates = 30;
    [SerializeField] private static int maxWaffles = 12;

    void Awake()
    {
        pancakes = new ReservoirStack<ReservoirPancake>(maxPancakes);
        plates = new ReservoirStack<ReservoirPlate>(maxPlates);
        waffles = new ReservoirStack<ReservoirWaffle>(maxWaffles);
    }

    public static ReservoirStack<ReservoirPancake> GetPancakes() { return pancakes; }
    public static ReservoirStack<ReservoirPlate> GetPlates() { return plates; }
    public static ReservoirStack<ReservoirWaffle> GetWaffles() { return waffles; }
}