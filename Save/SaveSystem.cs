using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "gameSave.json");
        File.WriteAllText(path, jsonData);
        Debug.Log("Game saved successfully!");
    }

}
