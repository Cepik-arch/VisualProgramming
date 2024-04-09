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
        inLoop = false;
        
    }

    public override void Execute()
    {
        loopNumber = int.Parse(loopCount.text);

        iteration++;
        if (iteration > loopNumber)
        {
            ExecuteNextBlockWithDelay();
            iteration = 0;
        }
        else
        {
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
}

