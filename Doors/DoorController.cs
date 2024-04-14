using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private NeedValue needValue;
    private AudioSource doorSound;

    private float value;

    private bool opened;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        needValue = GetComponent<NeedValue>();
        doorSound = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        opened = doorAnimator.GetBool("open");

        if (!opened)
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
    }
    public void CloseDoor()
    {
        opened = doorAnimator.GetBool("open");

        if (opened)
        {
            doorAnimator.SetBool("open", false);
            doorSound.Play();
        }
    }
}