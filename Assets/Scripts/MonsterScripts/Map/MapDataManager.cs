using System.IO;
using UnityEngine;

public class MapDataManager : MonoBehaviour
{
    private string mapDataFilePath;

    private void Awake()
    {
        mapDataFilePath = Path.Combine(Application.dataPath, "MapData.json");
    }

    public void SaveMapDataToFile(MapData mapData)
    {
        string jsonData = JsonUtility.ToJson(mapData);
        File.WriteAllText(mapDataFilePath, jsonData);
    }

    public MapData LoadMapDataFromFile()
    {
        if (File.Exists(mapDataFilePath))
        {
            string jsonData = File.ReadAllText(mapDataFilePath);
            MapData mapData = JsonUtility.FromJson<MapData>(jsonData);
            return mapData;
        }
        else
        {
            Debug.LogWarning("Map data file not found!");
            return null;
        }
    }
}