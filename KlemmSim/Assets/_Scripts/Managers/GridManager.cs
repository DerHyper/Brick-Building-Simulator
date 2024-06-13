using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Transform BuildingBlocksParent; // This should be located in the hierarchy at "Environment/Building Blocks"

    [SerializeField]
    private Vector3Int _size = new(5,5,5);
    [SerializeField]
    private bool _showDebug = false; 
    private Grid3D _grid;
    private void Awake()
    {
        ReplaceGrid(_size);
        if (_showDebug)
        {
            _grid.DrawGridLines();
            _grid.DrawDebugText();
        }
    }

    public void ReplaceGrid(Vector3Int size)
    {
        if(_grid != null) ClearGrid();
        
        this._size = size;
        _grid = new Grid3D(size.x, size.y, size.z);
    }

    public void ReplaceGrid(Grid3D grid)
    {
        if(grid != null) ClearGrid();
        
        this._size = grid.GetSize();
        this._grid = grid;
    }

    public bool TryInstantiateBuildingBlock(Vector3Int originPosition, BuildingBlock block)
    {
        if (!InstantiationAllowed(originPosition, block)) return false;

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(originPosition, block);

        _grid.AddBlock(originPosition, blockDisplay);
        return true;
    }

    // Returns true if the block can be instantiated at the given position 
    private bool InstantiationAllowed(Vector3Int originPosition, BuildingBlock block)
    {
        if (!_grid.IsInsideBuildingLimit(originPosition, block))
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
        for (int xOffset = 0; xOffset < block.SizeX; xOffset++)
            for (int yOffset = 0; yOffset < block.SizeY; yOffset++)
                for (int zOffset = 0; zOffset < block.SizeZ; zOffset++)
                {
                    Vector3Int offset = new Vector3Int(xOffset, yOffset, zOffset);
                    Vector3Int positionWithOffset = position + offset;
                    if (_grid.IsOccupied(positionWithOffset)) return false;
                }

        return true;
    }

    // Instantiates a new building block as a child of the "BuildingBlocks"-GameObject
    private BuildingBlockDisplay InstantiateBuildingBlock(Vector3Int position, BuildingBlock block)
    {
        GameObject blockGameObject = Instantiate(block.Model, position, Quaternion.identity, BuildingBlocksParent).gameObject;
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
        _grid.DeleteBlock(target);
        Destroy(target.gameObject);
    }

    public Vector3Int GetSize()
    {
        return _size;
    }

    public BuildingBlockDisplay[] GetBlocksInGrid()
    {
        BuildingBlockDisplay[] blockArray = _grid.Blocks.ToArray();
        return blockArray;
    }
}
