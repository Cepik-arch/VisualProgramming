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
        PopulateDropdownOptions();
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
        // Implement logic to declare the variable
        // For simplicity, we'll just log the declaration here
        Debug.Log($"Declared variable: {name} with initial value: {initialValue}");

        var newValue = new Value() { variable = name, value = initialValue };
        values.Add(newValue);
        ShowValues();
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

