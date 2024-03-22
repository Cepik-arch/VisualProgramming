using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DeclarationBlock : Block
{
    public TMP_InputField variableName; // Name of the variable to declare
    public TMP_InputField initialValue; // Initial value of the variable



    protected override void Awake()
    {
        base.Awake();
        // Call PopulateDropdownOptions when this block is created
    }

    public override void Execute()
    {
        if (initialValue.text != "" && variableName.text != "")
        {
            int initValue = int.Parse(initialValue.text);
            string varName = variableName.text;

            // Perform the variable declaration
            DeclareVariable(varName, initValue);

            PopulateDropdownOptions();
        }

        // Move to the next block in the sequence
        if (nextBlock != null)
        {
            nextBlock.Execute();
        }
    }

    private void DeclareVariable(string name, int initialValue)
    {
        // Check if the variable with the same name already exists
        bool variableExists = false;
        foreach (var value in values)
        {
            if (value.variable == name)
            {
                variableExists = true;
                value.value = initialValue; // Update the value of the existing variable
                Debug.Log($"Updated variable: {name} with new value: {initialValue}");
                break;
            }
        }

        // If the variable doesn't exist, add it to the list
        if (!variableExists)
        {
            Debug.Log($"Declared variable: {name} with initial value: {initialValue}");
            var newValue = new Value() { variable = name, value = initialValue };
            values.Add(newValue);
        }

        ShowValues(); // Show the updated list of variables
    }

    public void PopulateDropdownOptions()
    {
        // Get all dropdowns in the scene
        IfBlock[] ifBlocks = FindObjectsOfType<IfBlock>();

        // Populate dropdown options for each IfBlock in the scene
        foreach (IfBlock ifBlock in ifBlocks)
        {
            ifBlock.PopulateDropdownOptions();
        }
    }
}

