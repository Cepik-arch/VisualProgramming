using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnPress : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveLoadManager saveLoadManager = FindAnyObjectByType<SaveLoadManager>();

            if (saveLoadManager != null )
            {
                saveLoadManager.SaveGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SaveLoadManager saveLoadManager = FindAnyObjectByType<SaveLoadManager>();

            if (saveLoadManager != null)
            {
                saveLoadManager.LoadGame();
            }
        }
    }
}
