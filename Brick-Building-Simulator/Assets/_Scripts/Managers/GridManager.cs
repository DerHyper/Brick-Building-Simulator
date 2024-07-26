using UnityEngine;
/// <summary>
/// Instantiates and manages access to a Grid3D instance.
/// </summary>
public class GridManager : MonoBehaviour
{
    public Transform BuildingBlocksParent; // This should be located in the hierarchy at "Environment/Building Blocks"

    [SerializeField, Min(1)]
    private Vector3Int _size = new(5, 5, 5);
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
        if (_grid != null) ClearGrid();

        this._size = size;
        _grid = new Grid3D(size.x, size.y, size.z);
    }

    public void ReplaceGrid(Grid3D grid)
    {
        if (grid != null) ClearGrid();

        this._size = grid.GetSize();
        this._grid = grid;
    }
    /// <summary>
    /// Trys to instantiate a BuildingBlock.
    /// </summary>
    /// <param name="position">Position on which the BuildingBlock will be placed.</param>
    /// <param name="block">The BuildingBlock that will be placed.</param>
    /// <param name="alignment">The rotation which will be used for instantiation.</param>
    /// <returns>True, if the instantiation was successfull</returns>
    public bool TryInstantiateBuildingBlock(Vector3Int position, BuildingBlock block, Orientation.Alignment alignment)
    {
        if (!IsInstantiationAllowed(position, block, alignment)) return false;

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(position, block, alignment);

        _grid.AddBlock(blockDisplay);
        return true;
    }

    /// <inheritdoc cref="TryInstantiateBuildingBlock(Vector3Int, BuildingBlock, Orientation.Alignment)"/>
    public bool TryInstantiateBuildingBlock(Vector3Int position, BuildingBlock block)
    {
        if (!IsInstantiationAllowed(position, block, StandardAlignment)) return false;

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(position, block, StandardAlignment);

        _grid.AddBlock(blockDisplay);
        return true;
    }

    /// <summary>
    /// Destroys every block that can be found in the grid.
    /// </summary>
    public void ClearGrid()
    {
        BuildingBlockDisplay[] buildingBlocksInGrid = GetBlocksInGrid();
        DestroyBlocks(buildingBlocksInGrid);
    }

    /// <summary>
    /// Destroys every block given as a parameter and deletes all its references.
    /// </summary>
    /// <param name="targets">Array of all the blocks that should be destroyed.</param>
    public void DestroyBlocks(BuildingBlockDisplay[] targets)
    {
        foreach (BuildingBlockDisplay target in targets)
        {
            DestroyBlock(target);
        }
    }

    /// <summary>
    /// Destroys the block given as a parameter and deletes all its references.
    /// </summary>
    /// <param name="targets">The block that should be destroyed.</param>
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

    // positive values only
    private void OnValidate()
    {
        if (_size.x < 0) _size.x = 0;
        if (_size.y < 0) _size.y = 0;
        if (_size.z < 0) _size.z = 0;
    }

    // Returns true if the block can be instantiated at the given position 
    private bool IsInstantiationAllowed(Vector3Int originPosition, BuildingBlock block, Orientation.Alignment alignment)
    {
        if (!_grid.IsInsideBuildingLimit(originPosition, block, alignment))
        {
            Debug.Log($"Placing a block at {originPosition} is invalid, since it would clip out of the grid");
            return false;
        }

        if (_grid.IsSpaceOccupied(originPosition, block, alignment))
        {
            Debug.Log($"Block '{block.name}' is overlapping another block at {originPosition}");
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
        Debug.Log("Instantiated: " + blockDisplay);
        return blockDisplay;
    }
}
