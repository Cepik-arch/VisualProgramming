using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private SaveLoadManager saveLoadManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        saveLoadManager.SaveGame();
    }

    public void LoadGame()
    {
        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnGameSceneLoaded;

        // Load the "Game" scene
        SceneManager.LoadScene("Game");
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnGameSceneLoaded;

        //Find the SaveLoadManager in the newly loaded scene
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager != null)
        {
            //Call LoadGame on SaveLoadManager
            saveLoadManager.LoadGame();

        }
        else
        {
            Debug.LogWarning("SaveLoadManager not found in the loaded scene.");
        }
    }
}
