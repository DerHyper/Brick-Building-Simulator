using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Transform BuildingBlocksParent; // This should be located in the hierarchy at "Environment/Building Blocks"

    [SerializeField]
    private Vector3Int size = new Vector3Int(5,5,5);
    [SerializeField]
    private bool showDebug = false; 
    private Grid3D grid;
    private void Awake()
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

        grid.AddBlock(originPosition, blockDisplay);
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
    private BuildingBlockDisplay InstantiateBuildingBlock(Vector3Int position, BuildingBlock block)
    {
        GameObject blockGameObject = Instantiate(block.model, position, Quaternion.identity, BuildingBlocksParent).gameObject;
        BuildingBlockDisplay blockDisplay = blockGameObject.AddComponent<BuildingBlockDisplay>();
        blockDisplay.UpdateDisplay(position, block);

        return blockDisplay;
    }

    // Destroys every block that can be found in this grid
    public void ClearGrid()
    {
        BuildingBlockDisplay[] buildingBlocksInGrid = GetBlocksInGrid();
        DestroyBlocks(buildingBlocksInGrid);
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
        grid.DeleteBlock(target);
        Destroy(target.gameObject);
    }

    public Vector3Int GetSize()
    {
        return size;
    }

    public BuildingBlockDisplay[] GetBlocksInGrid()
    {
        BuildingBlockDisplay[] blockArray = grid.blocks.ToArray();
        return blockArray;
    }
}
