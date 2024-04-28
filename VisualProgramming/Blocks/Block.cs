using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    //BlockID for save/load
    public int blockID = -1;
    public RectTransform blockRectTransform;

    [HideInInspector]
    public GameObject blockConnector;
    [HideInInspector]
    public GameObject nextBlockConnector;

    // If Block is in loop its has different line color
    [HideInInspector]
    public bool inLoop = false;
    [HideInInspector]
    public Block nextBlock = null;
    public bool cantBeNext;
    [HideInInspector]
    public GameObject grayedImg;

    [HideInInspector]
    public static List<Variable> variables = new List<Variable>();
    protected float executionDelay = 0.2f;

    protected TMP_InputField debugField;

    protected virtual void Awake()
    {
        blockRectTransform = GetComponent<RectTransform>();

        blockType = BlockType.Default;

        //Search in childs for connectors points
        blockConnector = FindChildWithTag(gameObject, "BlockConnector");
        nextBlockConnector = FindChildWithTag(gameObject, "NextBlockConnector");

        //GrayedImg for double click
        grayedImg = FindChildWithTag(gameObject, "GrayedImg");
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
    public void SaveValue(string variable, object valueToSave)
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

    public void WriteToDebugField(String debugOutput, Color? color = null)
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

    //Capture and restore block data
    public virtual BlockData GetBlockData()
    {
        return new BlockData
        {
            position = new SerializableVector2(transform.position),
            inLoop = inLoop,
            blockID = blockID
        };
    }

    public virtual void SetBlockData(BlockData data)
    {
        if (data != null)
        {
            blockRectTransform.anchoredPosition = data.position.ToVector2();
            nextBlock = FindBlockByBlockID(data.nextBlockID);
            inLoop = data.inLoop;
        }

    }

    //Find a block by its blockID within the hierarchy
    protected Block FindBlockByBlockID(int blockID)
    {
        //Check if this block matches the desired blockID
        if (this.blockID == blockID)
        {
            Debug.Log("Next Block with ID: " + blockID + " was found");
            return this;
        }

        //Traverse through all child blocks (if any)
        foreach (Transform child in transform)
        {
            Block childBlock = child.GetComponent<Block>();
            if (childBlock != null)
            {
                // Recursively search within the child block
                Block foundBlock = childBlock.FindBlockByBlockID(blockID);
                if (foundBlock != null)
                {
                    Debug.Log("Block with ID: " + blockID + " was found");
                    return foundBlock;
                }
            }
        }
        return null;
    }
}

