using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LoopBlock : Block
{
    public Block loopedBlock;
    public TMP_InputField loopCount;

    private int loopNumber;
    private int iteration = 0;


    protected override void Awake()
    {
        base.Awake();
        blockType = BlockType.LoopBlock;

    }

    public override void Execute()
    {
        float value;

        // Check if input is a variable or a value for Variable
        if (!float.TryParse(loopCount.text, out value))
        {
            if (IsVariable(loopCount.text))
            {
                value = (float)FindValue(loopCount.text);
            }
            else
            {
                Debug.Log("Invalid input. Please enter a valid number or variable name.");
                WriteToDebugField("Invalid input. Please enter a valid number or variable name.", Color.red);
                return;
            }
        }

        loopNumber = (int)Math.Round(value);

        iteration++;

        if (iteration > loopNumber)
        {
            WriteToDebugField($"Loop ended");
            ExecuteNextBlockWithDelay();
            iteration = 0;
        }
        else
        {
            WriteToDebugField($"Iteration of loop: {iteration}");
            ExecuteLoopedBlockWithDelay();
        }

    }

    private void ExecuteNextBlockWithDelay()
    {
        StartCoroutine(ExecuteNextBlockAfterDelay(nextBlock));
    }

    private void ExecuteLoopedBlockWithDelay()
    {
        StartCoroutine(ExecuteNextBlockAfterDelay(loopedBlock));
    }
    private IEnumerator ExecuteNextBlockAfterDelay(Block block)
    {
        yield return new WaitForSeconds(executionDelay);

        if (block != null)
        {
            block.Execute();
        }
    }

    //Capture and restore block data
    public override BlockData GetBlockData()
    {
        return new LoopBlockData(this);
    }

    public override void SetBlockData(BlockData data)
    {
        base.SetBlockData(data);
        if (data is LoopBlockData loopBlockData)
        {
            loopCount.text = loopBlockData.loopCount.ToString();
        }

    }
}

