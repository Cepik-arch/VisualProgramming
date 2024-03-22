using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockProgram : MonoBehaviour
{
    public Block[] blocks; // Array to store all blocks in the program

    public void ExecuteProgram()
    {
        foreach (Block block in blocks)
        {
            // Execute each block in sequence
            block.Execute();
        }
    }
}

