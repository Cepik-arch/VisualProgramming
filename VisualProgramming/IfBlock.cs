using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IfBlock : Block
{
    public Block falseBlock;

    public TMP_Dropdown value1Dropdown;
    public TMP_Dropdown value2Dropdown;
    public TMP_Dropdown operandDropdown;

    // Set the block type to IfBlock
    protected override void Awake()
    {
        base.Awake();
        blockType = BlockType.IfBlock;
    }

    public override void Execute()
    {

        // Get values from dropdowns
        string value1Variable = value1Dropdown.options[value1Dropdown.value].text;
        string value2Variable = value2Dropdown.options[value2Dropdown.value].text;

        // Find the corresponding values in the 'values' list
        float value1 = FindValue(value1Variable);
        float value2 = FindValue(value2Variable);

        // Get selected operand from dropdown
        string operand = operandDropdown.options[operandDropdown.value].text;

        // Check the condition based on the operand
        bool condition = CheckCondition(value1, value2, operand);

        if (condition)
        {
            Debug.Log("Condition is true. Executing if block logic.");
            if (nextBlock != null)
            {
                nextBlock.Execute();
            }
        }
        else
        {
            Debug.Log("Condition is false. Skipping if block.");
            if (falseBlock != null)
            {
                falseBlock.Execute();
            }
        }

    }

    // Method to check the condition based on the operand
    private bool CheckCondition(float value1, float value2, string operand)
    {
        switch (operand)
        {
            case "==":
                return value1 == value2;
            case "!=":
                return value1 != value2;
            case "<":
                return value1 < value2;
            case ">":
                return value1 > value2;
            case "<=":
                return value1 <= value2;
            case ">=":
                return value1 >= value2;
            default:
                Debug.LogError("Invalid operand.");
                return false;
        }
    }

    public void PopulateDropdownOptions()
    {
        // Store the current selected values
        string selectedValue1 = "";
        if (value1Dropdown.options.Count > value1Dropdown.value)
        {
            selectedValue1 = value1Dropdown.options[value1Dropdown.value].text;
        }
        string selectedValue2 = "";
        if (value2Dropdown.options.Count > value2Dropdown.value)
        {
            selectedValue2 = value2Dropdown.options[value2Dropdown.value].text;
        }

        // Clear the options
        value1Dropdown.ClearOptions();
        value2Dropdown.ClearOptions();

        // Create a list of dropdown options
        List<string> options = new List<string>();

        // Add values from the 'values' list to the dropdown options
        foreach (Value value in values)
        {
            options.Add(value.variable);
        }

        // Set the dropdown options
        value1Dropdown.AddOptions(options);
        value2Dropdown.AddOptions(options);

        // Re-select the previously selected values if they still exist in the options
        if (!string.IsNullOrEmpty(selectedValue1) && options.Contains(selectedValue1))
        {
            value1Dropdown.value = options.IndexOf(selectedValue1);
        }
        if (!string.IsNullOrEmpty(selectedValue2) && options.Contains(selectedValue2))
        {
            value2Dropdown.value = options.IndexOf(selectedValue2);
        }
    }


    // Method to find the value corresponding to a variable in the 'values' list
    private float FindValue(string variable)
    {
        foreach (Value value in values)
        {
            if (value.variable == variable)
            {
                return value.value;
            }
        }

        // If variable not found, return 0 (or handle differently based on your requirement)
        Debug.LogError($"Variable '{variable}' not found in values list.");
        return 0f;
    }
}



