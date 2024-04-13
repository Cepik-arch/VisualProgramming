using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : MonoBehaviour
{
    private Block firstBlockClicked;
    private Block secondBlockClicked;

    //variables checking type of block
    private IfBlock ifBlock;
    private bool trueConnection = true;

    private LoopBlock loopBlock;
    private bool loopConnection = true;

    public GameObject blockConnectionPrefab;
    private List<GameObject> connectionLines = new List<GameObject>();


    public void SetBlockClicked(Block block)
    {
        if (firstBlockClicked == null)
        {
            firstBlockClicked = block;
            ifBlock = firstBlockClicked as IfBlock;
            loopBlock = firstBlockClicked as LoopBlock;

            Debug.Log("First block clicked: " + firstBlockClicked.name + ", BlockType: " + firstBlockClicked.blockType);

        }
        else if (secondBlockClicked == null && firstBlockClicked != block)
        {
            secondBlockClicked = block;

            if (secondBlockClicked.cantBeNext == true)
            {
                secondBlockClicked = null;
                firstBlockClicked = null;
                return;
            }
            else
            {
                Debug.Log("Second block clicked: " + secondBlockClicked.name);

                if (ifBlock != null)
                {
                    //Debug.Log("is IFblock");

                    if (trueConnection)
                    {
                        ifBlock.nextBlock = secondBlockClicked;
                    }
                    else
                    {
                        ifBlock.falseBlock = secondBlockClicked;
                    }
                    ConnectBlocks();
                    trueConnection = !trueConnection;
                }
                else if (loopBlock != null)
                {
                    //Debug.Log("is Loopblock");
                    if (loopConnection)
                    {
                        loopBlock.loopedBlock = secondBlockClicked;
                    }
                    else
                    {
                        loopBlock.nextBlock = secondBlockClicked;
                    }
                    ConnectBlocks();
                    loopConnection = !loopConnection;

                }
                else
                {
                    //Debug.Log("is DefaultBlock");

                    firstBlockClicked.nextBlock = secondBlockClicked;
                    ConnectBlocks();
                }
            }
        }
        else if (secondBlockClicked == block)
        {
            RemoveConnection(secondBlockClicked);
        }
    }
    private void ConnectBlocks()
    {
        if (firstBlockClicked != null && secondBlockClicked != null)
        {
            if (blockConnectionPrefab != null)
            {
                // Remove existing connections for the first block if any
                RemoveConnection(firstBlockClicked);

                GameObject blockConnectionObject = Instantiate(blockConnectionPrefab, transform.parent);
                BlockConnection blockConnection = blockConnectionObject.GetComponent<BlockConnection>();
                if (blockConnection != null)
                {
                    blockConnection.startBlock = firstBlockClicked;
                    blockConnection.endBlock = secondBlockClicked;

                    if (firstBlockClicked.inLoop)
                    {
                        blockConnection.lineRenderer.startColor = blockConnection.loopConnectionColor;
                        blockConnection.lineRenderer.endColor = blockConnection.loopConnectionColor;
                        
                        if(secondBlockClicked.blockType != Block.BlockType.LoopBlock)
                        {
                            secondBlockClicked.inLoop = true;
                        }
                    }
                    else
                    {
                        blockConnection.lineRenderer.startColor = blockConnection.trueConnectionColor;
                        blockConnection.lineRenderer.endColor = blockConnection.trueConnectionColor;
                    }


                    blockConnection.DrawConnection();

                    //add the connection line GameObject to the list
                    connectionLines.Add(blockConnectionObject);
                    Debug.Log("Blocks connected: " + firstBlockClicked.name + " -> " + secondBlockClicked.name);
                }
            }

            //if it's an IfBlock, create a second connection line for the false block
            if (firstBlockClicked.blockType == Block.BlockType.IfBlock)
            {
                // Create a new connection line for the false block
                GameObject blockConnectionObjectFalse = Instantiate(blockConnectionPrefab, transform.parent);
                BlockConnection blockConnectionFalse = blockConnectionObjectFalse.GetComponent<BlockConnection>();
                if (blockConnectionFalse != null)
                {
                    blockConnectionFalse.startBlock = firstBlockClicked;
                    blockConnectionFalse.endBlock = ifBlock.falseBlock; // Use the false block
                    blockConnectionFalse.lineRenderer.startColor = blockConnectionFalse.falseConnectionColor;
                    blockConnectionFalse.lineRenderer.endColor = blockConnectionFalse.falseConnectionColor;
                    blockConnectionFalse.DrawConnection();

                    // Add the connection line for the false block to the list
                    connectionLines.Add(blockConnectionObjectFalse);
                }
            }

            // If it's an LoopBlock, create a second connection line for the looped block
            if (firstBlockClicked.blockType == Block.BlockType.LoopBlock)
            {
                GameObject blockConnectionObjectLoop = Instantiate(blockConnectionPrefab, transform.parent);
                BlockConnection blockConnectionLoop = blockConnectionObjectLoop.GetComponent<BlockConnection>();

                if (blockConnectionLoop != null)
                {
                    blockConnectionLoop.startBlock = firstBlockClicked;
                    blockConnectionLoop.endBlock = loopBlock.loopedBlock;
                    loopBlock.loopedBlock.inLoop = true;
                    blockConnectionLoop.lineRenderer.startColor = blockConnectionLoop.loopConnectionColor;
                    blockConnectionLoop.lineRenderer.endColor = blockConnectionLoop.loopConnectionColor;
                    blockConnectionLoop.DrawConnection();

                    connectionLines.Add(blockConnectionObjectLoop);
                }
            }


            // Reset variables
            firstBlockClicked = null;
            secondBlockClicked = null;
            ifBlock = null;
        }
    }
    private void RemoveConnection(Block block)
    {
        //find and remove the connection lines that start at the given block
        for (int i = connectionLines.Count - 1; i >= 0; i--)
        {
            BlockConnection connection = connectionLines[i].GetComponent<BlockConnection>();
            if (connection != null && connection.startBlock == block)
            {
                Destroy(connectionLines[i]);
                connectionLines.RemoveAt(i);
                Debug.Log("Connection removed for block: " + block.name);
            }
        }
    }
    //update the connection lines when a connected block is moved
    public void UpdateConnectionLines()
    {
        foreach (GameObject connectionLine in connectionLines)
        {
            connectionLine.GetComponent<BlockConnection>().DrawConnection();
        }
    }
    public void RemoveAllConnections(Block block)
    {
        //remove all outgoing connections from block
        RemoveConnection(block);
        block.nextBlock = null;

        //if the block is an IfBlock, also clear the falseBlock reference
        IfBlock ifBlock = block as IfBlock;
        if (ifBlock != null)
        {
            ifBlock.falseBlock = null;
        }
    }
}
