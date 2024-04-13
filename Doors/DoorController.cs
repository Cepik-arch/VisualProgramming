using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private NeedValue needValue;
    private AudioSource doorSound;

    private float value;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        needValue = GetComponent<NeedValue>();
        doorSound = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (needValue != null && needValue.needValueToOpen)
        {
            value = (float)needValue.blockWithValue.GetValueByName(needValue.nameOfValue);
            if (value >= needValue.minValueToOpen && value <= needValue.maxValueToOpen)
            {
                doorAnimator.SetBool("open", true);
                doorSound.Play();
            }
        }
        else
        {
            doorAnimator.SetBool("open", true);
            doorSound.Play();
        }
    }
    public void CloseDoor()
    {
        doorAnimator.SetBool("open", false);
        doorSound.Play();
    }
}