using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : MonoBehaviour
{
    private Block firstBlockClicked; // Store the first block clicked for connection
    private Block secondBlockClicked; // Store the second block clicked for connection

    // variable for IFblock
    private IfBlock ifBlock;

    public GameObject blockConnectionPrefab;
    private List<GameObject> connectionLines = new List<GameObject>(); // List to store connection line GameObjects

    private bool trueconnection = true;
    public void SetBlockClicked(Block block)
    {
        if (firstBlockClicked == null)
        {
            firstBlockClicked = block;
            ifBlock = firstBlockClicked as IfBlock;
            Debug.Log("First block clicked: " + firstBlockClicked.name + ", BlockType: " + firstBlockClicked.blockType);

        }
        else if (secondBlockClicked == null && firstBlockClicked != block)
        {
            secondBlockClicked = block;
            Debug.Log("Second block clicked: " + secondBlockClicked.name);

            if (ifBlock != null)
            {
                Debug.Log("is IFblock");

                if (trueconnection)
                {
                    ifBlock.nextBlock = secondBlockClicked;
                    ConnectBlocks();
                    trueconnection = false;
                }
                else
                {
                    ifBlock.falseBlock = secondBlockClicked;
                    ConnectBlocks();
                    trueconnection = true;
                }

            }
            else
            {
                firstBlockClicked.nextBlock = secondBlockClicked;
                ConnectBlocks();
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

                    // Determine connection color
                    if (firstBlockClicked.blockType == Block.BlockType.IfBlock && secondBlockClicked == ifBlock.falseBlock)
                    {
                        blockConnection.lineRenderer.startColor = blockConnection.falseConnectionColor;
                        blockConnection.lineRenderer.endColor = blockConnection.falseConnectionColor;
                    }
                    else
                    {
                        blockConnection.lineRenderer.startColor = blockConnection.trueConnectionColor;
                        blockConnection.lineRenderer.endColor = blockConnection.trueConnectionColor;
                    }

                    blockConnection.DrawConnection();

                    // Add the connection line GameObject to the list
                    connectionLines.Add(blockConnectionObject);
                    Debug.Log("Blocks connected: " + firstBlockClicked.name + " -> " + secondBlockClicked.name);
                }
            }

            // If it's an IfBlock, create a second connection line for the false block
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

            // Reset variables
            firstBlockClicked = null;
            secondBlockClicked = null;
            ifBlock = null;
        }
    }


    private void RemoveConnection(Block block)
    {
        // Find and remove the connection lines that start at the given block
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

    // Update the connection lines when a connected block is moved
    public void UpdateConnectionLines()
    {
        foreach (GameObject connectionLine in connectionLines)
        {
            connectionLine.GetComponent<BlockConnection>().DrawConnection();
        }
    }
}
