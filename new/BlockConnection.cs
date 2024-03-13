using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnection : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Block startBlock;
    public Block endBlock;

    public bool isTrueConnection;

    public Color trueConnectionColor = Color.green; // Color for true connection
    public Color falseConnectionColor = Color.red; // Color for false connection

    public void DrawConnection()
    {
        if (startBlock != null && endBlock != null)
        {
            // Ensure the position count matches the number of points needed (2 for start and end)
            if (lineRenderer.positionCount != 2)
            {
                lineRenderer.positionCount = 2;
            }

            // Set the positions
            lineRenderer.SetPosition(0, startBlock.transform.position);
            lineRenderer.SetPosition(1, endBlock.transform.position);
        }
        else
        {
            // If start or end block is missing, clear the line
            lineRenderer.positionCount = 0;
        }
    }
}



