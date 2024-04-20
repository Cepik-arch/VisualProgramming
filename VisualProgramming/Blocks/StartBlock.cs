using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : Block
{

    public override void Execute()
    {
        WriteToDebugField($"Executing " + nextBlock);
        base.Execute();
    }

}
