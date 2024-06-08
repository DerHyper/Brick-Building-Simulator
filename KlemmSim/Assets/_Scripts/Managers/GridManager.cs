using UnityEngine;

public class GridManager : MonoBehaviour, IGridManager
{
    [SerializeField]
    private Vector3Int size = new Vector3Int(5,5,5);
    [SerializeField]
    private bool showDebug = false; 
    private Grid3D grid;
    private void Start()
    {
        ReplaceGrid(size);
        if (showDebug)
        {
            grid.DrawGridLines();
            grid.DrawDebugText();
        }
    }

    public void ReplaceGrid(Vector3Int size)
    {
        if(grid != null) ClearGrid();
        
        this.size = size;
        grid = new Grid3D(size.x, size.y, size.z);
    }

    public void ReplaceGrid(Grid3D grid)
    {
        if(grid != null) ClearGrid();
        
        this.size = grid.GetSize();
        this.grid = grid;
    }

    public bool TryInstantiateBuildingBlock(Vector3Int originPosition, BuildingBlock block)
    {
        if (!InstantiationAllowed(originPosition, block)) return false;

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(originPosition, block);

        grid.SetVoxels(originPosition, block, blockDisplay);
        return true;
    }

    // Returns true if the block can be instantiated at the given position 
    private bool InstantiationAllowed(Vector3Int originPosition, BuildingBlock block)
    {
        if (!grid.IsInsideBuildingLimit(originPosition, block))
        {
            Debug.Log("Placing a block at "+originPosition.ToString() + " is invalid, since it would clip out of the grid");
            return false;
        }

        if (!CanOccupieSpace(originPosition, block))
        {
            Debug.Log("Block '" + block.name + "' is overlapping another block at " + originPosition.ToString());
            return false;
        }

        return true;
    }

    // Returns false if any block inside the specified space is being occupied.
    private bool CanOccupieSpace(Vector3Int position, BuildingBlock block)
    {
        for (int xOffset = 0; xOffset < block.sizeX; xOffset++)
            for (int yOffset = 0; yOffset < block.sizeY; yOffset++)
                for (int zOffset = 0; zOffset < block.sizeZ; zOffset++)
                {
                    Vector3Int offset = new Vector3Int(xOffset, yOffset, zOffset);
                    Vector3Int positionWithOffset = position + offset;
                    if (grid.IsOccupied(positionWithOffset)) return false;
                }

        return true;
    }

    // Instantiates a new building block as a child of the "BuildingBlocks"-GameObject
    private static BuildingBlockDisplay InstantiateBuildingBlock(Vector3Int position, BuildingBlock block)
    {
        Transform parent =  Finder.FindOrCreateGameObjectWithTag("BuildingBlocks").transform;

        GameObject blockGameObject = Instantiate(block.model, position, Quaternion.identity, parent).gameObject;
        BuildingBlockDisplay blockDisplay = blockGameObject.AddComponent<BuildingBlockDisplay>();
        blockDisplay.UpdateDisplay(position, block);

        return blockDisplay;
    }

    // Destroys every block that can be found in this grid
    public void ClearGrid()
    {
        BuildingBlockDisplay[] blocks = Finder.FindBuildingBlocks();
        DestroyBlocks(blocks);
    }

    public void DestroyBlocks(BuildingBlockDisplay[] targets)
    {
        foreach (BuildingBlockDisplay target in targets)
        {
            DestroyBlock(target);
        }
    }

    public void DestroyBlock(BuildingBlockDisplay target)
    {
        DeleteBlockReferencesInGrid(target);
        Destroy(target.gameObject);
    }

    // Delets all references in the grid to a given block
    private void DeleteBlockReferencesInGrid(BuildingBlockDisplay target)
    {
        Vector3Int position = target.position;
        BuildingBlock block = target.block;

        for (int xOffset = 0; xOffset < block.sizeX; xOffset++)
            for (int yOffset = 0; yOffset < block.sizeY; yOffset++)
                for (int zOffset = 0; zOffset < block.sizeZ; zOffset++)
                {
                    Vector3Int offset = new Vector3Int(xOffset, yOffset, zOffset);
                    Vector3Int positionWithOffset = position + offset;
                    grid.ResetVoxel(positionWithOffset);
                }
    }

    public Vector3Int GetSize()
    {
        return size;
    }
}
