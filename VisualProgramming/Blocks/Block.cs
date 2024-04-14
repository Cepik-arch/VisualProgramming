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

    [HideInInspector]
    public GameObject blockConnector;
    [HideInInspector]
    public GameObject nextBlockConnector;

    // If Block is in loop its has different line color
    [HideInInspector]
    public bool inLoop = false;
    [HideInInspector]
    public Block nextBlock;
    public bool cantBeNext;
    [HideInInspector]
    public GameObject grayedImg;

    protected static List<Variable> variables = new List<Variable>();
    protected float executionDelay = 1f;

    protected TMP_InputField debugField;

    protected virtual void Awake()
    {
        blockType = BlockType.Default;

        //Search in childs for connectors points
        blockConnector = FindChildWithTag(gameObject, "BlockConnector");
        nextBlockConnector = FindChildWithTag(gameObject, "NextBlockConnector");

        //GrayedImg for double click
        grayedImg = FindChildWithTag(gameObject,"GrayedImg");
        grayedImg.SetActive(false);

        //Debug blocks
        debugField = transform.parent.parent.Find("DebugLog")?.GetComponentInChildren<TMP_InputField>();



    }

    public virtual void Execute()
    {
        //ShowValues();
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

    //Search for childs with tag
    private static GameObject FindChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }

    protected void WriteToDebugField(String debugOutput, Color? color = null)
    {
        

        if (color != null)
        {
            color = Color.white;
        }
        
        if (debugField != null && debugOutput != null)
        {
            debugField.text = debugOutput;

            if (color != null)
            {
                debugField.textComponent.color = color.Value;
            }
            else
            {
                debugField.textComponent.color = Color.white;
            }
        }
    }
}

