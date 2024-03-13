using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class LoopBlock : Block
{
    public TMP_InputField loopCount; // Number of times to loop

    public override void Execute()
    {
        int loopcount = int.Parse(loopCount.text);

        Debug.Log($"Executing loop block for {loopcount} times");

        ShowValues();

        for (int i = 0; i < loopcount; i++)
        {
            // Execute the next block in the sequence for each iteration of the loop
            if (nextBlock != null)
            {
                
                nextBlock.Execute();
            }
        }
    }
}

