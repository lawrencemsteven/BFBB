using UnityEngine;

[System.Serializable]
public class GameInfo
{
    public SongInfo Song;
    public DishStationInfo Dish;
    public PancakeStationInfo Pancake;
    public PrepStationInfo Prep;

    public GameInfo()
    {
        Song = new SongInfo();
        Dish = new DishStationInfo();
        Pancake = new PancakeStationInfo();
        Prep = new PrepStationInfo();
    }
}