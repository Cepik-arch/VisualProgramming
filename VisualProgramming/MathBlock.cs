using TMPro;
using UnityEngine;

public class MathBlock : Block
{
    public TMP_InputField variable1Input;
    public TMP_InputField variable2Input;
    public TMP_Dropdown operandDropdown;

    public override void Execute()
    {
        float variable1;
        float variable2;

        // Check if input is a variable or a value
        variable1 = IsVariable(variable1Input.text) ? FindValue(variable1Input.text) : float.Parse(variable1Input.text);
        variable2 = IsVariable(variable2Input.text) ? FindValue(variable2Input.text) : float.Parse(variable2Input.text);

        // Get the selected operand
        string operand = operandDropdown.options[operandDropdown.value].text;

        Debug.Log($"Executing operation: {variable1} {operand} {variable2}");

        // Perform the operation based on the operand
        float result = PerformOperation(variable1, variable2, operand);

        Debug.Log($"Result: {result}");

        // Move to the next block in the sequence
        if (nextBlock != null)
        {
            nextBlock.Execute();
        }
    }

    private bool IsVariable(string variable)
    {
        foreach (Value value in values)
        {
            if (value.variable == variable)
            {
                return true;
            }
        }
        return false;
    }

    private float FindValue(string variable)
    {
        foreach (Value value in values)
        {
            if (value.variable == variable)
            {
                return value.value;
            }
        }
        return 0f;
    }
    private void SaveValue(string variable, float value)
    {
        // Update the value of the variable in the list
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i].variable == variable)
            {
                values[i].value = value;
                // Assuming you want to break the loop after finding the variable
                break;
            }
        }
    }

    private float PerformOperation(float variable1, float variable2, string operand)
    {
        // Perform the operation based on the operand
        switch (operand)
        {
            case "+":
                return variable1 + variable2;
            case "-":
                return variable1 - variable2;
            case "*":
                return variable1 * variable2;
            case "/":
                if (variable2 != 0)
                    return variable1 / variable2;
                else
                {
                    Debug.LogWarning("Division by zero.");
                    return float.NaN; // Return NaN (Not a Number) for invalid division
                }
            default:
                Debug.LogWarning($"Unknown operand: {operand}");
                return float.NaN; // Return NaN for unknown operands
        }
    }

}
