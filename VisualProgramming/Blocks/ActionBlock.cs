using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ActionBlock : Block
{
    [System.Serializable]
    public class ActionEvent
    {
        public string actionName;
        public UnityEvent actionEvent;
    }

    public TMP_Dropdown actionDropdown;
    public List<ActionEvent> actionEvents = new List<ActionEvent>();

    private void Start()
    {
        // Ensure the dropdown is assigned
        if (actionDropdown != null)
        {
            // Clear existing options
            actionDropdown.ClearOptions();

            // Create a list of option strings from actionEvents
            List<string> options = new List<string>();
            foreach (var actionEvent in actionEvents)
            {
                options.Add(actionEvent.actionName);
            }

            // Set the dropdown options
            actionDropdown.AddOptions(options);
        }
        else
        {
            Debug.LogWarning("Action dropdown not assigned.");
        }
    }

    public override void Execute()
    {
        if (actionDropdown != null)
        {
            string selectedAction = actionDropdown.options[actionDropdown.value].text;
            //Debug.Log($"Executing action: {selectedAction}");
            WriteToDebugField($"Executing action: {selectedAction}");
            TriggerEvent(selectedAction);
        }
        else
        {
            Debug.LogWarning("Action dropdown not assigned.");
        }

        base.Execute();
    }

    private void TriggerEvent(string actionName)
    {
        ActionEvent foundEvent = actionEvents.Find(a => a.actionName == actionName);

        if (foundEvent != null && foundEvent.actionEvent != null)
        {
            foundEvent.actionEvent.Invoke();
        }
        else
        {
            Debug.LogWarning($"Action event not found for: {actionName}");
        }
    }

    //Capture and restore block data
    public override BlockData GetBlockData()
    {
        return new ActionBlockData(this);
    }

    public override void SetBlockData(BlockData data)
    {
        base.SetBlockData(data);
        if (data is ActionBlockData actionBlockData)
        {
            actionDropdown.value = actionBlockData.selectedActionIndex;
        }
    }
}
