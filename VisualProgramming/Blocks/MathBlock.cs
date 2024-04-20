using TMPro;
using UnityEngine;

public class MathBlock : Block
{
    public TMP_InputField variable1Input;
    public TMP_InputField variable2Input;
    public TMP_Dropdown operatorDropdown;

    public override void Execute()
    {
        float value1;
        float value2;

        // Check if input is a variable or a value for Variable
        if (!float.TryParse(variable1Input.text, out value1))
        {
            if (IsVariable(variable1Input.text))
            {
                value1 = (float)FindValue(variable1Input.text);
            }
            else
            {
                Debug.Log("Invalid input for Variable 1. Please enter a valid number or variable name.");
                WriteToDebugField("Invalid input for Variable 1. Please enter a valid number or variable name.", Color.red);
                return;
            }
        }

        if (!float.TryParse(variable2Input.text, out value2))
        {
            if (IsVariable(variable2Input.text))
            {
                value2 = (float)FindValue(variable2Input.text);
            }
            else
            {
                Debug.Log("Invalid input for Variable 2. Please enter a valid number or variable name.");
                WriteToDebugField("Invalid input for Variable 2. Please enter a valid number or variable name.", Color.red);
                return;
            }
        }

        // Get the selected operand
        string @operator = operatorDropdown.options[operatorDropdown.value].text;

        Debug.Log($"Executing operation: {value1} {@operator} {value2}");
        WriteToDebugField($"Executing operation: {value1} {@operator} {value2}");

        // Perform the operation based on the operand
        float result = PerformOperation(value1, value2, @operator);

        Debug.Log($"Result: {result}");
        WriteToDebugField($"Result: {result}");

        if (IsVariable(variable1Input.text))
        {
            SaveValue(variable1Input.text, result);
        }
        else if (IsVariable(variable2Input.text))
        {
            SaveValue(variable2Input.text, result);
        }

        base.Execute();
    }

    private float PerformOperation(float variable1, float variable2, string @operator)
    {
        // Perform the operation based on the operand
        switch (@operator)
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
                Debug.LogWarning($"Unknown operand: {@operator}");
                return float.NaN; // Return NaN for unknown operands
        }
    }

    //Capture and restore block data
    public override BlockData GetBlockData()
    {
        return new MathBlockData(this);
    }

    public override void SetBlockData(BlockData data)
    {
        base.SetBlockData(data);
        if (data is MathBlockData mathBlockData)  // Check if data is MathBlockData
        {
            variable1Input.text = mathBlockData.variable1Input;
            variable2Input.text = mathBlockData.variable2Input;
            operatorDropdown.value = mathBlockData.operatorIndex;
        }
    }

}
