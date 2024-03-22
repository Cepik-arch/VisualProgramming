using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        Debug.Log("OpenDoorInController");
        doorAnimator.SetBool("open", true);
    }
    public void CloseDoor()
    {
        Debug.Log("CloseDoorInController");
        doorAnimator.SetBool("open", false);
    }
}
