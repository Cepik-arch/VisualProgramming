using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private NeedValue needValue;

    private float value;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        needValue = GetComponent<NeedValue>();
    }

    public void OpenDoor()
    {
        if (needValue != null && needValue.needValueToOpen)
        {
            value = (float)needValue.blockWithValue.GetValueByName(needValue.nameOfValue);
            if (value >= needValue.minValueToOpen && value <= needValue.maxValueToOpen)
            {
                Debug.Log("OpenDoorInController");
                doorAnimator.SetBool("open", true);
            }
        }
        else
        {
            Debug.Log("OpenDoorInController");
            doorAnimator.SetBool("open", true);
        }
    }
    public void CloseDoor()
    {
        Debug.Log("CloseDoorInController");
        doorAnimator.SetBool("open", false);
    }
}