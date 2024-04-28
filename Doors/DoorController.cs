using UnityEngine;
using UnityEngine.Rendering;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private NeedValue needValue;
    private AudioSource doorSound;

    private float value;
    private bool opened;

    [HideInInspector]
    public string doorName;

    private void Start()
    {
        doorName = this.name;
        doorAnimator = GetComponent<Animator>();
        needValue = GetComponent<NeedValue>();
        doorSound = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (!opened)
        {
            if (needValue != null && needValue.needValueToOpen)
            {
                value = (float)needValue.blockWithValue.GetValueByName(needValue.nameOfValue);
                if (value >= needValue.minValueToOpen && value <= needValue.maxValueToOpen)
                {
                    doorAnimator.SetBool("open", true);
                    doorSound.Play();
                    opened = true;
                }
            }
            else
            {
                doorAnimator.SetBool("open", true);
                doorSound.Play();
                opened = true;
            }
        }
    }
    public void CloseDoor()
    {
        if (opened)
        {
            doorAnimator.SetBool("open", false);
            doorSound.Play();
            opened = false;
        }
    }

    //Save the current state of the door
    public DoorData GetDoorData()
    {
        DoorData doorData = new DoorData
        {
            doorName = doorName,
            opened = opened
        };

        return doorData;
    }
    public void SetDoorData(DoorData data)
    {

        if (data.opened)
        {
            doorAnimator.SetBool("open", true);
        }
        else
        {
            doorAnimator.SetBool("open", false);
        }

    }
}