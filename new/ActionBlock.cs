using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ActionBlock : Block
{
    public TMP_Dropdown action; // Name of the action to perform
    public UnityEvent[] actionEvents; // UnityEvents for different actions

    public override void Execute()
    {
        string actionName = action.options[action.value].text;

        Debug.Log($"Executing action: {actionName}");

        // Trigger the assigned event
        TriggerEvent(actionName);

        // Move to the next block in the sequence
        if (nextBlock != null)
        {
            nextBlock.Execute();
        }
    }

    private void TriggerEvent(string action)
    {
        // Find the index of the action
        int index = FindActionIndex(action);

        // If the action is found, trigger its associated event
        if (index != -1 && index < actionEvents.Length)
        {
            actionEvents[index]?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Action event not found for: {action}");
        }
    }

    private int FindActionIndex(string action)
    {
        // Implement logic to find the index of the action
        // For simplicity, I'm just returning the index directly
        switch (action)
        {
            case "Start":
                return 0;
            case "Stop":
                return 1;
            case "Open":
                return 2;
            case "Close":
                return 3;
            // Add more actions as needed
            default:
                return -1;
        }
    }
}
