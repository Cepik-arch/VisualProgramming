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
    public Color loopConnectionColor = Color.blue; // Color for loop connection

    public void DrawConnection()
    {
        if (startBlock != null && endBlock != null)
        {
            if (lineRenderer.positionCount != 2)
            {
                lineRenderer.positionCount = 2;
            }

            //set the line positions
            lineRenderer.SetPosition(0, startBlock.nextBlockConnector.transform.position);
            lineRenderer.SetPosition(1, endBlock.blockConnector.transform.position);
        }
        else
        {
            //clear line
            lineRenderer.positionCount = 0;
        }
    }
}



