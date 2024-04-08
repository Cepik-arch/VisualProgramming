using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CallUI : MonoBehaviour
{
    public Canvas UI;
    public GameObject player;
    private FirstPersonController playerMovement;

    public GameObject PlayerCamera;
    public GameObject UiCamera;

    private void Start()
    {
        UI.enabled = false;
        playerMovement = player.GetComponent<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.enabled = true;
            ToggleLines(UI.transform, true);

            UiCamera.SetActive(true);
            PlayerCamera.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
        }
    }

    public void TurnOffUI()
    {
        UI.enabled = false;
        ToggleLines(UI.transform, false);

        UiCamera.SetActive(false);
        PlayerCamera.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    private void ToggleLines(Transform parentTransform, bool enableLines)
    {
        // Iterate through all children of the parent transform
        foreach (Transform child in parentTransform)
        {
            // Check if the child's name matches "Line(clone)"
            if (child.name.StartsWith("Line(Clone)"))
            {
                // Toggle the visibility of the line object based on the 'enableLines' parameter
                child.gameObject.SetActive(enableLines);
            }

            // Recursively toggle lines under each child if it has further nested children
            if (child.childCount > 0)
            {
                ToggleLines(child, enableLines);
            }
        }
    }

}
