using UnityEngine;

public class EngineSwitch : MonoBehaviour
{
    public GameObject engineUiOn;
    public GameObject engineUiOff;

    public DoorController doorToOpen;
    public ChangeQuest questChanger;


    private bool engineRunning;
    private BatteryManager batteryManager;

    private void Start()
    {
        batteryManager = GetComponent<BatteryManager>();
    }

    public void StartEngine()
    {
        engineRunning = CheckBatteries();
        ToggleEngine();
    }

    private void ToggleEngine()
    {
        if (engineRunning)
        {
            engineUiOn.SetActive(true);
            engineUiOff.SetActive(false);

            doorToOpen.OpenDoor();
            questChanger.ChangeQuestText();

        }
        else
        {
            engineUiOn.SetActive(false);
            engineUiOff.SetActive(true);
        }
    }

    private bool CheckBatteries()
    {
        int batteriesRdy = 0;

        foreach (var battery in batteryManager.batteries)
        {
            if (battery.batteryLevel > 0.1f)
            {
                batteriesRdy++;
            }

        }

        if (batteriesRdy == batteryManager.batteries.Count)
        {
            return true;
        }
        return false;
    }
}
