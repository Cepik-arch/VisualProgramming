using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ActionBlock : Block
{
    public TMP_Dropdown action; // Name of the action to perform

    public override void Execute()
    {
        string actionName = action.options[action.value].text;

        Debug.Log($"Executing action: {actionName}");

        // Perform the action based on its name
        PerformAction(actionName);

        // Move to the next block in the sequence
        if (nextBlock != null)
        {
            nextBlock.Execute();
        }
    }

    private void PerformAction(string action)
    {
        // Implement logic to perform the action based on its name
        switch (action)
        {
            case "MoveForward":
                MoveForward();
                break;
            case "Jump":
                Jump();
                break;
            // Add more actions as needed
            default:
                Debug.LogWarning($"Unknown action: {action}");
                break;
        }
    }

    private void MoveForward()
    {
        // Implement logic to move the player forward
        Debug.Log("Moving forward...");
    }

    private void Jump()
    {
        // Implement logic to make the player jump
        Debug.Log("Jumping...");
    }
}

