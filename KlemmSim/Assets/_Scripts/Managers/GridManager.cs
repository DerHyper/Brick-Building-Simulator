using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Transform BuildingBlocksParent; // This should be located in the hierarchy at "Environment/Building Blocks"

    [SerializeField]
    private Vector3Int _size = new(5,5,5);
    [SerializeField]
    private bool _showDebug = false; 
    private const Orientation.Alignment StandardAlignment = Orientation.Alignment.North;
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

    public bool TryInstantiateBuildingBlock(Vector3Int originPosition, BuildingBlock block, Orientation.Alignment alignment)
    {
        if (!IsInstantiationAllowed(originPosition, block, alignment)) return false;

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(originPosition, block, alignment);

        _grid.AddBlock(blockDisplay);
        return true;
    }

    // Using StandardAlignment
    public bool TryInstantiateBuildingBlock(Vector3Int originPosition, BuildingBlock block)
    {
        if (!IsInstantiationAllowed(originPosition, block, StandardAlignment)) return false;

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(originPosition, block, StandardAlignment);

        _grid.AddBlock(blockDisplay);
        return true;
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

    // Returns true if the block can be instantiated at the given position 
    private bool IsInstantiationAllowed(Vector3Int originPosition, BuildingBlock block, Orientation.Alignment alignment)
    {
        if (!_grid.IsInsideBuildingLimit(originPosition, block, alignment))
        {
            Debug.Log("Placing a block at "+originPosition.ToString() + " is invalid, since it would clip out of the grid");
            return false;
        }

        if (_grid.IsSpaceOccupied(originPosition, block, alignment))
        {
            Debug.Log("Block '" + block.name + "' is overlapping another block at " + originPosition.ToString());
            return false;
        }

        return true;
    }



    // Instantiates a new building block as a child of the "BuildingBlocks"-GameObject
    private BuildingBlockDisplay InstantiateBuildingBlock(Vector3Int position, BuildingBlock block, Orientation.Alignment alignment)
    {
        Quaternion direction = Orientation.ToQuaternion(alignment);
        GameObject blockGameObject = Instantiate(block.Model, position, direction, BuildingBlocksParent).gameObject;
        BuildingBlockDisplay blockDisplay = blockGameObject.AddComponent<BuildingBlockDisplay>();
        blockDisplay = blockDisplay.UpdateDisplay(block, position, alignment);
        Debug.Log("Instantiated: "+blockDisplay.ToString());
        return blockDisplay;
    }
}
