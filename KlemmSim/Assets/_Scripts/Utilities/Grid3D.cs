using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D 
{
    private int xMax;
    private int yMax;
    private int zMax;
    private int [,,] gridArray;
    private TextMesh[,,] debugTextArray;

    // Constructor
    public Grid3D (int xMax, int yMax, int zMax)
    {
        this.xMax = xMax;
        this.yMax = yMax;
        this.zMax = zMax;
        debugTextArray = new TextMesh[xMax, yMax, zMax];

        gridArray = new int[xMax, yMax, zMax];

        
        DrawDebugText();
        DrawGridLines();

    }

    private void DrawDebugText()
    {
        for (int x = 0; x < xMax; x++)
            for (int y = 0; y < yMax; y++)
                for (int z = 0; z < zMax; z++)
                    debugTextArray[x, y, z] = WorldText.CreateWorldText(gridArray[x, y, z].ToString(), GetWorldPosition(x, y, z), 10, Color.white);
    }

    private void DrawGridLines()
    {
        // Draw Lines in x direction
        for (int y = 0; y < yMax+1; y++)
            for (int z = 0; z < zMax+1; z++)
                Debug.DrawLine(GetWorldPosition(0, y, z), GetWorldPosition(xMax, y, z), Color.white, 100f);

        // Draw Lines in y direction
        for (int x = 0; x < xMax+1; x++)
            for (int z = 0; z < zMax+1; z++)
                Debug.DrawLine(GetWorldPosition(x, 0, z), GetWorldPosition(x, yMax, z), Color.white, 100f);

        // Draw Lines in z direction
        for (int x = 0; x < xMax+1; x++)
            for (int y = 0; y < yMax+1; y++)
                Debug.DrawLine(GetWorldPosition(x, y, 0), GetWorldPosition(x, y, zMax), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z);
    }

    public void SetGridObject(Vector3Int position, int value)
    {
        if (isNotInBuildingLimit(position)) 
            Debug.Log(position.ToString() + " is not a valid Position to build.");
        
        int x = position.x;
        int y = position.y;
        int z = position.z;
        
        gridArray[x,y,z] = value;
        debugTextArray[x,y,z].text = value.ToString();
    }

    private bool isNotInBuildingLimit(Vector3Int Position)
    {
        if (Position.x < 0 || Position.x >= xMax) return true;
        if (Position.y < 0 || Position.y >= yMax) return true;
        if (Position.z < 0 || Position.z >= zMax) return true;
        return false;
    }
}
