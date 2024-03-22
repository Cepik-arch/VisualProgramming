using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CallUI : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;
    private FirstPersonController playerMovement;

    public GameObject PlayerCamera;
    public GameObject UiCamera;

    private void Start()
    {

        playerMovement = player.GetComponent<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(true);

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

    /*
    private void Update()
    {
        if (!UI.activeSelf)
        {
            UiCamera.SetActive(false);
            PlayerCamera.SetActive(true);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }
    }
    */

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }
    }
}
