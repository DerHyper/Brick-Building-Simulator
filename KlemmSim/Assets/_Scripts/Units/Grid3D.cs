using System.Collections.Generic;
using UnityEngine;

// 3-dimensional grid that consists of voxels
public class Grid3D 
{
    public List<BuildingBlockDisplay> Blocks {get; private set;} = new List<BuildingBlockDisplay>();
    private Voxel[,,] _voxel;

    // Constructor 
    public Grid3D (int xMax, int yMax, int zMax)
    {
        _voxel = new Voxel[xMax,yMax,zMax];
        
        // Initialize every voxel in voxels
        for (int x = 0; x < xMax; x++)
            for (int y = 0; y < yMax; y++)
                for (int z = 0; z < zMax; z++)
                    _voxel[x,y,z] = new Voxel();
    }

    // Can be seen in Edit Mode and Runtime
    public void DrawDebugText()
    {
        int xMax = _voxel.GetLength(0);
        int yMax = _voxel.GetLength(1);
        int zMax = _voxel.GetLength(2);

        // Draw debug text for every voxel in voxels
        for (int x = 0; x < xMax; x++)
            for (int y = 0; y < yMax; y++)
                for (int z = 0; z < zMax; z++)
                {
                    Vector3 textPosition = GetWorldPosition(x, y, z) + Vector3.one*0.5f;
                    _voxel[x, y, z].DebugText = WorldText.CreateDebugText("", textPosition);
                }
    }

    // Can only be seen in Edit Mode
    public void DrawGridLines()
    {
        int xMax = _voxel.GetLength(0);
        int yMax = _voxel.GetLength(1);
        int zMax = _voxel.GetLength(2);

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

    // For every voxel, occupied by the BuildingBlockDisplay, set the voxel accordingly 
    public void AddBlock(BuildingBlockDisplay blockDisplay)
    {
        Blocks.Add(blockDisplay);
        Orientation.Alignment alignment = blockDisplay.Alignment;

        Vector3Int startPosition = blockDisplay.Position; 
        Vector3Int endPosition = Orientation.GetRotatedSize(alignment, blockDisplay);

        // Set every voxel covered by the block 
        Helper.DoWithinRange(startPosition, endPosition, SetVoxel, blockDisplay);
    }

    public bool IsInsideBuildingLimit(Vector3Int blockOriginPoint, BuildingBlock block, Orientation.Alignment alignment)
    {
        // Check for every voxel, that would be covered by the block, if it is inside the building limit
        Vector3Int endPosition = Orientation.GetRotatedSize(alignment, block);
        List<bool> inLimit = Helper.DoWithinRange(blockOriginPoint, endPosition, IsInsideBuildingLimit);
        bool blockInsideLimit = !inLimit.Contains(false);

        return blockInsideLimit;
    }

    // returns true if the position is inside the building limit
    public bool IsInsideBuildingLimit(Vector3Int position)
    {
        int xMax = _voxel.GetLength(0);
        int yMax = _voxel.GetLength(1);
        int zMax = _voxel.GetLength(2);

        if (position.x < 0 || position.x >= xMax) return false;
        if (position.y < 0 || position.y >= yMax) return false;
        if (position.z < 0 || position.z >= zMax) return false;
        return true;
    }

    // Returns false if any block inside the specified space is being occupied.
    public bool IsSpaceOccupied(Vector3Int blockOriginPoint, BuildingBlock block, Orientation.Alignment alignment)
    {
        Vector3Int endPosition = Orientation.GetRotatedSize(alignment, block);
        List<bool> voxelOccupied = Helper.DoWithinRange(blockOriginPoint, endPosition, IsOccupied);
        bool atLeastOneOccupied = voxelOccupied.Contains(true);

        return atLeastOneOccupied;
    }

    public bool IsOccupied(Vector3Int position)
    {
        return _voxel[position.x, position.y, position.z].IsOccupied;
    }

    public Vector3Int GetSize()
    {
        int x = _voxel.GetLength(0);
        int y = _voxel.GetLength(1);
        int z = _voxel.GetLength(2);
        Vector3Int size = new Vector3Int(x,y,z);

        return size;
    }

    public BuildingBlock GetBuildingBlock(Vector3Int position)
    {
        int x = position.x;
        int y = position.y;
        int z = position.z;

        return _voxel[x,y,z].BuildingBlock;
    }

    // Delets all references in list and voxel
    public void DeleteBlock(BuildingBlockDisplay target)
    {
        Blocks.Remove(target);
        Vector3Int originPosition = target.Position;
        Vector3Int endPosition = Orientation.GetRotatedSize(target.Alignment, target.Block);
        Helper.DoWithinRange<Vector3Int>(originPosition, endPosition, ResetVoxel);
    }

    public new string ToString()
    {
        string info = "";
        foreach (var block in Blocks)
        {
            info += block.ToString()+"\n";
        }

        
        return "{"+info+"}";
    }

    // Converts the index of the grid to the position in the scene
    private Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z);
    }

    private void SetVoxel(Vector3Int position, BuildingBlockDisplay blockDisplay)
    {
        int x = position.x;
        int y = position.y;
        int z = position.z;
        string debugText = blockDisplay.name+$"\n({x},{y},{z})";

        _voxel[x,y,z].UpdateVoxel(blockDisplay.Block, debugText, true);
    }

    private void ResetVoxel(Vector3Int position)
    {
        int x = position.x;
        int y = position.y;
        int z = position.z;
        
        _voxel[x,y,z].UpdateVoxel(null, "", false);
    }

    // Each voxel contains data about the BuildingBlock that occupies it.
    private class Voxel
    {
        public BuildingBlock BuildingBlock;
        public TextMesh DebugText;
        public bool IsOccupied;

        public Voxel()
        {
            this.BuildingBlock = null;
            this.DebugText = null;
            this.IsOccupied = false;
        }

        public void UpdateVoxel(BuildingBlock buildingBlock, string debugText, bool isOccupied)
        {
            this.BuildingBlock = buildingBlock;
            this.IsOccupied = isOccupied;

            // Only update the debugText if it exists
            if (this.DebugText) this.DebugText.text = debugText;
        }
    }
}
