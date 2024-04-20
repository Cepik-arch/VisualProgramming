using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableVector3 playerPosition;
    public List<BlockData> blockDataList;
    public List<DoorData> doorDataList;
    public ActiveQuest activeQuest;

    public GameData()
    {
        blockDataList = new List<BlockData>();
        doorDataList = new List<DoorData>();
    }

}

[System.Serializable]
public class BlockData
{
    public int blockID;
    public SerializableVector2 position;
    public int nextBlockID;
    public bool inLoop;

}

[System.Serializable]
public class StartBlockData : BlockData
{
    public StartBlockData(StartBlock startBlock)
    {
        position = new SerializableVector2(startBlock.transform.position);
        nextBlockID = startBlock.nextBlock != null ? startBlock.nextBlock.blockID : -1;
        inLoop = startBlock.inLoop;
    }
}

[System.Serializable]
public class DeclarationBlockData : BlockData
{
    public string variableName;
    public string initialValue;

    public DeclarationBlockData(DeclarationBlock declarationBlock)
    {
        position = new SerializableVector2(declarationBlock.transform.position);
        nextBlockID = declarationBlock.nextBlock != null ? declarationBlock.nextBlock.blockID : -1;
        inLoop = declarationBlock.inLoop;
        variableName = declarationBlock.variableName.text;
        initialValue = declarationBlock.initialValue.text;
    }
}

[System.Serializable]
public class IfBlockData : BlockData
{
    public Block falseBlock;
    public string variable1Input;
    public string variable2Input;
    public int operatorIndex;

    public IfBlockData(IfBlock ifBlock)
    {
        position = new SerializableVector2(ifBlock.transform.position);
        nextBlockID = ifBlock.nextBlock != null ? ifBlock.nextBlock.blockID : -1;
        falseBlock = ifBlock.falseBlock;
        inLoop = ifBlock.inLoop;
        variable1Input = ifBlock.variable1Input != null ? ifBlock.variable1Input.text : "";
        variable2Input = ifBlock.variable2Input != null ? ifBlock.variable2Input.text : "";
        operatorIndex = ifBlock.operatorDropdown != null ? ifBlock.operatorDropdown.value : 0;
    }
}

[System.Serializable]
public class LoopBlockData : BlockData
{
    public Block loopedBlock;
    public int loopCount;

    public LoopBlockData(LoopBlock loopBlock)
    {
        position = new SerializableVector2(loopBlock.transform.position);
        nextBlockID = loopBlock.nextBlock != null ? loopBlock.nextBlock.blockID : -1;
        inLoop = loopBlock.inLoop;
        loopedBlock = loopBlock.loopedBlock;
        loopCount = int.Parse(loopBlock.loopCount.text);
    }
}

[System.Serializable]
public class MathBlockData : BlockData
{
    public string variable1Input;
    public string variable2Input;
    public int operatorIndex;

    public MathBlockData(MathBlock mathBlock)
    {
        position = new SerializableVector2(mathBlock.transform.position);
        nextBlockID = mathBlock.nextBlock != null ? mathBlock.nextBlock.blockID : -1;
        inLoop = mathBlock.inLoop;
        variable1Input = mathBlock.variable1Input.text;
        variable2Input = mathBlock.variable2Input.text;
        operatorIndex = mathBlock.operatorDropdown.value;
    }
}

[System.Serializable]
public class ActionBlockData : BlockData
{
    public int selectedActionIndex;

    public ActionBlockData(ActionBlock actionBlock)
    {
        position = new SerializableVector2(actionBlock.transform.position);
        nextBlockID = actionBlock.nextBlock != null ? actionBlock.nextBlock.blockID : -1;
        inLoop = actionBlock.inLoop;
        selectedActionIndex = actionBlock.actionDropdown.value;
    }
}

[System.Serializable]
public class DoorData
{
    public string doorName;
    public bool opened;

}

[System.Serializable]
public class ActiveQuest
{
    public string title;
    public string description;
}


//Vector3 and Vector2 Serialize/Deserialize
[System.Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public class SerializableVector2
{
    public float x;
    public float y;

    public SerializableVector2(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
}
