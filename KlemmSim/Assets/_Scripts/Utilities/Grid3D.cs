using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D 
{
    private int xMax;
    private int yMax;
    private int zMax;
    private BuildingBlockDisplay [,,] buildingBlocks;
    private TextMesh[,,] debugTexts;
    private bool[,,] isOccupied;

    // Constructor
    public Grid3D (int xMax, int yMax, int zMax)
    {
        this.xMax = xMax;
        this.yMax = yMax;
        this.zMax = zMax;

        debugTexts = new TextMesh[xMax, yMax, zMax];
        buildingBlocks = new BuildingBlockDisplay[xMax, yMax, zMax];
        isOccupied = new bool[xMax, yMax, zMax];
        
        DrawDebugText();
        DrawGridLines();
    }

    private void DrawDebugText()
    {
        for (int x = 0; x < xMax; x++)
            for (int y = 0; y < yMax; y++)
                for (int z = 0; z < zMax; z++)
                {
                    Vector3 textPosition = GetWorldPosition(x, y, z) + Vector3.one*0.5f;
                    if (debugTexts[x, y, z] && buildingBlocks[x,y,z])
                    {
                        debugTexts[x, y, z] = WorldText.CreateDebugText(buildingBlocks[x, y, z].name, textPosition);
                    } 
                    else 
                    {
                        debugTexts[x, y, z] = WorldText.CreateDebugText("", textPosition);
                    }
                }
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

    public void SetGridObject(Vector3Int position, BuildingBlockDisplay value)
    {
        
        int x = position.x;
        int y = position.y;
        int z = position.z;
        
        buildingBlocks[x,y,z] = value;
        debugTexts[x,y,z].text = value.name;
        isOccupied[x,y,z] = true;
    }

    public bool IsNotInBuildingLimit(Vector3Int position)
    {
        if (position.x < 0 || position.x >= xMax) return true;
        if (position.y < 0 || position.y >= yMax) return true;
        if (position.z < 0 || position.z >= zMax) return true;
        return false;
    }

    public bool IsOccupied(Vector3Int position)
    {
        return isOccupied[position.x, position.y, position.z];
    }

    public void DestroyGridObject(Vector3Int position)
    {
        
        int x = position.x;
        int y = position.y;
        int z = position.z;
        
        buildingBlocks[x,y,z] = null;
        debugTexts[x,y,z].text = "";
        isOccupied[x,y,z] = false;
    }
}
