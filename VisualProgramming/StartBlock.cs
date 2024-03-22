using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : Block
{

    public override void Execute()
    {
        Debug.Log("Executing generic block logic" + nextBlock);
        nextBlock.Execute();
    }
}
