using System.Collections;
using TMPro;
using UnityEngine;

public class IfBlock : Block
{
    public Block falseBlock;

    public TMP_InputField variable1Input;
    public TMP_InputField variable2Input;
    public TMP_Dropdown operandDropdown;

    // Set the block type to IfBlock
    protected override void Awake()
    {
        base.Awake();
        blockType = BlockType.IfBlock;
    }

    public override void Execute()
    {
        Debug.Log("Executing IF block.");
        object value1 = null;
        object value2 = null;

        bool boolValue1 = false;
        bool boolValue2 = false;

        // Check if input is a variable or a value for Variable
        if (!float.TryParse(variable1Input.text, out float floatValue1) && !bool.TryParse(variable1Input.text, out boolValue1))
        {
            if (IsVariable(variable1Input.text))
            {
                value1 = FindValue(variable1Input.text);
            }
            else
            {
                Debug.Log("Invalid input for Variable 1. Please enter a valid number or variable name.");
                return;
            }
        }
        else
        {
            value1 = floatValue1 != 0 ? (object)floatValue1 : (object)boolValue1;
        }

        if (!float.TryParse(variable2Input.text, out float floatValue2) && !bool.TryParse(variable2Input.text, out boolValue2))
        {
            if (IsVariable(variable2Input.text))
            {
                value2 = FindValue(variable2Input.text);
            }
            else
            {
                Debug.Log("Invalid input for Variable 2. Please enter a valid number or variable name.");
                return;
            }
        }
        else
        {
            value2 = floatValue2 != 0 ? (object)floatValue2 : (object)boolValue2;
        }

        // Get selected operand from dropdown
        string operand = operandDropdown.options[operandDropdown.value].text;

        // Check the condition based on the operand
        bool condition;
        if (value1 is float && value2 is float)
        {
            condition = CheckCondition((float)value1, (float)value2, operand);
        }
        else if (value1 is bool && value2 is bool)
        {
            condition = CheckBoolCondition((bool)value1, (bool)value2, operand);
        }
        else
        {
            Debug.LogError("Incompatible types for comparison.");
            return;
        }

        if (condition)
        {
            ExecuteNextBlockWithDelay();
        }
        else
        {
            ExecuteFalseBlockWithDelay();
        }
    }

    private void ExecuteNextBlockWithDelay()
    {
        StartCoroutine(ExecuteNextBlockAfterDelay(nextBlock));
    }

    private void ExecuteFalseBlockWithDelay()
    {
        StartCoroutine(ExecuteNextBlockAfterDelay(falseBlock));
    }

    private IEnumerator ExecuteNextBlockAfterDelay(Block block)
    {
        yield return new WaitForSeconds(executionDelay);

        if (block != null)
        {
            block.Execute();
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

    private bool CheckBoolCondition(bool value1, bool value2, string operand)
    {
        switch (operand)
        {
            case "==":
                return value1 == value2;
            case "!=":
                return value1 != value2;
            default:
                Debug.LogError("Invalid operand.");
                return false;
        }
    }
}



