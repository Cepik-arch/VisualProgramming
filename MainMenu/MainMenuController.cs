using Player;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
   public Button continueButton;

    private void Start()
    {
        UpdateContinueButton();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        GameManager gameManager = GameManager.instance;
        if (gameManager != null)
        {
            gameManager.LoadGame();
        }
        else
        {
            Debug.LogWarning("GameManager instance not found.");
        }
    }

    private void UpdateContinueButton()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "gameSave.json");
        bool saveFileExists = File.Exists(saveFilePath);
        continueButton.interactable = saveFileExists;
    }
}
