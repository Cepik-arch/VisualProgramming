using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Value
{
    public string variable { get; set; }
    public float value { get; set; }
}

public class Block : MonoBehaviour
{
    // Enum to define the type of block
    public enum BlockType
    {
        Default,
        IfBlock,
    }

    public BlockType blockType = BlockType.Default;

    public Block nextBlock;

    protected static List<Value> values = new List<Value>();

    protected virtual void Awake()
    {
        blockType = BlockType.Default;
    }

    public virtual void Execute()
    {
        Debug.Log("Executing generic block logic");
        ShowValues();
        nextBlock.Execute();
    }

    public void ShowValues()
    {

        if (values.Count > 0)
        {
            foreach (Value value in values)
            {
                Debug.Log($"Variable: {value.variable}, Value: {value.value}");
            }
        }
        else
        {
            Debug.LogWarning("No variables declared.");
        }

    }


}

