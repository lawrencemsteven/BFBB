using System.IO;
using UnityEngine;

public class GameInfoManager : Singleton<GameInfoManager>
{
    private string saveFile;
    private GameInfo info = new GameInfo();
    public SongInfo Song { get { return info.Song; } }
    public DishStationInfo Dish { get { return info.Dish; } }
    public PancakeStationInfo Pancake { get { return info.Pancake; } }
    public PrepStationInfo Prep { get { return info.Prep; } }


    public new static GameInfoManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameInfoManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameInfoManager").AddComponent<GameInfoManager>();
                }
            }
            return _instance;
        }
    }

    new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }

    public void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            info = JsonUtility.FromJson<GameInfo>(fileContents);
        }
    }

    public void WriteFile()
    {
        string jsonString = JsonUtility.ToJson(info);
        File.WriteAllText(saveFile, jsonString);
    }

}