using TMPro;
using UnityEngine;

public class DeclarationBlock : Block
{
    public TMP_InputField variableName;
    public TMP_InputField initialValue;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Execute()
    {
        if (initialValue.text != "" && variableName.text != "")
        {
            string varName = variableName.text;

            // Remove previous variable, if exists
            RemoveVariable(varName);


            if(bool.TryParse(initialValue.text, out bool boolvalue))
            {
                DeclareVariable(varName, boolvalue);
            }
            else if (float.TryParse(initialValue.text, out float floatvalue))
            {
                DeclareVariable(varName, floatvalue);
            }
            else
            {
                Debug.Log("Wrong input try to be declared");
                return;
            }

        }

        // Move to the next block in the sequence
        if (nextBlock != null)
        {
            nextBlock.Execute();
        }
    }

    private void RemoveVariable(string name)
    {
        // Check if the variable with the same name already exists
        foreach (var value in variables)
        {
            if (value.name == name)
            {
                variables.Remove(value);
                Debug.Log($"Removed variable: {name}");
                break;
            }
        }
    }

    private void DeclareVariable(string name, object value)
    {
        Variable newVariable = new Variable() { name = name, value = value };
        variables.Add(newVariable);
        Debug.Log($"Declared variable: {name} with initial value: {value}");
        ShowValues(); // Show the updated list of variables
    }

}