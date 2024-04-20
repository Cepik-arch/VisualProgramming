using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public static GameData LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameSave.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("Game loaded successfully!");
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return null;
        }
    }
}
