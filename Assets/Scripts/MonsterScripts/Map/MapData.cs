using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapData
{
    public string mapName; 
    public Vector2 playerPosition; 
    public Dictionary<string, Vector2> portalPositions;

    public MapData(string name)
    {
        mapName = name;
        playerPosition = Vector2.zero;
        portalPositions = new Dictionary<string, Vector2>();
    }
}

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    private MapData currentMapData; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        currentMapData = new MapData(SceneManager.GetActiveScene().name);
    }

    public MapData GetCurrentMapData()
    {
        return currentMapData;
    }

    public void AddPortalPosition(string portalName, Vector2 position)
    {
        if (!currentMapData.portalPositions.ContainsKey(portalName))
        {
            currentMapData.portalPositions.Add(portalName, position);
        }
        else
        {
            currentMapData.portalPositions[portalName] = position;
        }
    }

    public void SetPlayerPosition(Vector2 position)
    {
        currentMapData.playerPosition = position;
    }

    public void MoveToMap(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }

    public void MoveToMapWithMapData(MapData mapData)
    {
        currentMapData = mapData;
        SceneManager.LoadScene(mapData.mapName);
    }

    public void SaveCurrentMap()
    {
        string jsonData = JsonUtility.ToJson(currentMapData);
        Debug.Log("Current Map Data:\n" + jsonData);
    }
}