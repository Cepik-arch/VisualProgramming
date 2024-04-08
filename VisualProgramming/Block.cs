using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Variable
{
    public string name { get; set; }
    public object value { get; set; }
}

public class Block : MonoBehaviour
{
    // Enum to define the type of block
    public enum BlockType
    {
        Default,
        IfBlock,
        LoopBlock
    }

    public BlockType blockType = BlockType.Default;

    public bool cantBeNext;

    // If Block is in loop its has different line color
    //[HideInInspector]
    public bool inLoop = false;

    public Block nextBlock;

    protected static List<Variable> variables = new List<Variable>();
    protected float executionDelay = 1f;

    protected virtual void Awake()
    {
        blockType = BlockType.Default;
    }

    public virtual void Execute()
    {
        ShowValues();
        StartCoroutine(ExecuteWithDelay(executionDelay));
    }

    public void ShowValues()
    {

        if (variables.Count > 0)
        {
            foreach (Variable value in variables)
            {
                Debug.Log($"Variable: {value.name}, Value: {value.value}");
            }
        }
        else
        {
            Debug.LogWarning("No variables declared.");
        }

    }

    // Method to find the value corresponding to a variable in the 'values' list
    protected object FindValue(string variable)
    {
        foreach (Variable value in variables)
        {
            if (value.name == variable)
            {
                return value.value;
            }
        }
        return 0;
    }

    protected bool IsVariable(string variable)
    {
        foreach (Variable value in variables)
        {
            if (value.name == variable)
            {
                return true;
            }
        }
        return false;
    }
    protected void SaveValue(string variable, float valueToSave)
    {
        foreach (Variable value in variables)
        {
            if (value.name == variable)
            {
                value.value = valueToSave;
            }
        }
        return;
    }
    public object GetValueByName(string variable)
    {
        foreach (Variable value in variables)
        {
            if (value.name == variable)
            {
                return value.value;
            }
        }
        return 0;
    }

    protected IEnumerator ExecuteWithDelay(float delay)
    {
        Debug.Log($"Waiting for {delay} seconds...");
        yield return new WaitForSeconds(delay);

        // Move to the next block in the sequence
        if (nextBlock != null)
        {
            nextBlock.Execute();
        }
    }
}

