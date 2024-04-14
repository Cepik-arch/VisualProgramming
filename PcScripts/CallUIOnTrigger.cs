using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CallUI : MonoBehaviour
{
    private Canvas ui;

    private GameObject player;
    private PlayerController.PlayerController playerMovement;
    private GameObject playerCamera;
    private GameObject uiCamera;

    private void Awake()
    {
        ui = gameObject.GetComponentInChildren<Canvas>();
        uiCamera = GameObject.FindGameObjectWithTag("UiCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera"); 
    }

    private void Start()
    {
        ui.enabled = false;

        uiCamera.SetActive(false);

        playerMovement = player.GetComponent<PlayerController.PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ui.enabled = true;
            ToggleLines(ui.transform, true);
            ToggleInputs(ui, true);

            uiCamera.SetActive(true);
            playerCamera.SetActive(false);

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
        ui.enabled = false;
        ToggleLines(ui.transform, false);
        ToggleInputs (ui, false);

        uiCamera.SetActive(false);
        playerCamera.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    private void ToggleLines(Transform parent, bool enableLines)
    {
        foreach (Transform child in parent)
        {
            if (child.name.StartsWith("Line(Clone)"))
            {
                child.gameObject.SetActive(enableLines);
            }

            if (child.childCount > 0)
            {
                ToggleLines(child, enableLines);
            }
        }
    }

    private void ToggleInputs(Canvas parent, bool enableInput)
    {
        InputField[] inputFields = parent.GetComponentsInChildren<InputField>();
        foreach (InputField inputField in inputFields)
        {
            inputField.interactable = enableInput;
        }

    }

}
