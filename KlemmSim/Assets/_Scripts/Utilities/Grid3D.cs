using UnityEngine;

public class Grid3D 
{
    private Voxel[,,] voxels;

    // Constructor 
    public Grid3D (int xMax, int yMax, int zMax)
    {
        voxels = new Voxel[xMax,yMax,zMax];
        
        // Initialize every voxel in voxels
        for (int x = 0; x < xMax; x++)
            for (int y = 0; y < yMax; y++)
                for (int z = 0; z < zMax; z++)
                    voxels[x,y,z] = new Voxel();
    }

    private class Voxel
    {
        public BuildingBlock buildingBlock;
        public TextMesh debugText;
        public bool isOccupied;

        public Voxel()
        {
            this.buildingBlock = null;
            this.debugText = null;
            this.isOccupied = false;
        }

        public void UpdateVoxel(BuildingBlock buildingBlock, string debugText, bool isOccupied)
        {
            this.buildingBlock = buildingBlock;
            this.isOccupied = isOccupied;

            // Only update the debugText if it exists
            if (this.debugText) this.debugText.text = debugText;
        }
    }

    public void DrawDebugText()
    {
        int xMax = voxels.GetLength(0);
        int yMax = voxels.GetLength(1);
        int zMax = voxels.GetLength(2);

        // Draw debug text for every voxel in voxels
        for (int x = 0; x < xMax; x++)
            for (int y = 0; y < yMax; y++)
                for (int z = 0; z < zMax; z++)
                {
                    Vector3 textPosition = GetWorldPosition(x, y, z) + Vector3.one*0.5f;
                    voxels[x, y, z].debugText = WorldText.CreateDebugText("", textPosition);
                }
    }

    public void DrawGridLines()
    {
        int xMax = voxels.GetLength(0);
        int yMax = voxels.GetLength(1);
        int zMax = voxels.GetLength(2);

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

    public void SetVoxels(Vector3Int position, BuildingBlock block, BuildingBlockDisplay blockDisplay)
    {
        for (int xOffset = 0; xOffset < block.sizeX; xOffset++)
            for (int yOffset = 0; yOffset < block.sizeY; yOffset++)
                for (int zOffset = 0; zOffset < block.sizeZ; zOffset++)
                {
                    Vector3Int offset = new Vector3Int(xOffset, yOffset, zOffset);
                    Vector3Int positionWithOffset = position + offset;
                    SetVoxel(positionWithOffset, blockDisplay);
                }
    }

    private void SetVoxel(Vector3Int position, BuildingBlockDisplay referenceBlock)
    {
        int x = position.x;
        int y = position.y;
        int z = position.z;
        
        voxels[x,y,z].UpdateVoxel(referenceBlock.block, referenceBlock.name, true);
    }

    public bool IsNotInBuildingLimit(Vector3Int position)
    {
        int xMax = voxels.GetLength(0);
        int yMax = voxels.GetLength(1);
        int zMax = voxels.GetLength(2);

        if (position.x < 0 || position.x >= xMax) return true;
        if (position.y < 0 || position.y >= yMax) return true;
        if (position.z < 0 || position.z >= zMax) return true;
        return false;
    }

    public bool IsOccupied(Vector3Int position)
    {
        return voxels[position.x, position.y, position.z].isOccupied;
    }

    public void DestroyGridObject(Vector3Int position)
    {
        int x = position.x;
        int y = position.y;
        int z = position.z;
        
        voxels[x,y,z].UpdateVoxel(null, "", false);
    }
}
