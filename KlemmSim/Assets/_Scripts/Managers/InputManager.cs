using System.Numerics;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GridManager GridManager;
    public InventoryManager InventoryManager;
    [SerializeField]
    private GameObject _blockGhost;
    private Orientation.Alignment _currentAlignment = Orientation.Alignment.North;

    // TODO: This could be exchanged for an event system
    private void Update()
    {
        CheckInput();
        UpdateGhostPosition();
    }

    private void UpdateGhostPosition()
    {
        if(!CameraToWorld.TryGetMouseBlockPlacementPosition(out Vector3Int gridPosition))
        {
            Debug.LogWarning("Tried to place block outside of grid.");
            return;
        }

        if (InventoryManager.GetSelectedBuildingBlock() != null)
        {
            _blockGhost.transform.position = UnityEngine.Vector3.Lerp(_blockGhost.transform.position, gridPosition + Orientation.GetRotationOffset(_currentAlignment, InventoryManager.GetSelectedBuildingBlock()), 0.12f);
            UnityEngine.Quaternion direction = Orientation.ToQuaternion(_currentAlignment);
            _blockGhost.transform.rotation = UnityEngine.Quaternion.Lerp(_blockGhost.transform.rotation, direction, 0.12f);
        }

    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            BuildBlockAtMousePoint();
        }
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            DestroyBlockAtMousePoint();
        }
        if (Input.GetButtonDown("Vertical")) // Arrow keys
        {
            SetCurrentRotation(Orientation.RotateRight(_currentAlignment));
        }
    }

    private void SetCurrentRotation(Orientation.Alignment newAlignment)
    {
        _currentAlignment = newAlignment;
        UnityEngine.Quaternion direction = Orientation.ToQuaternion(newAlignment);
        _blockGhost.transform.rotation = direction;
    }

    // Check if position is valid, if so, place the currently selected block and remove it from inventory
    private void BuildBlockAtMousePoint()
    {
        if(!CameraToWorld.TryGetMouseBlockPlacementPosition(out Vector3Int gridPosition))
        {
            Debug.LogWarning("Tried to place block outside of grid.");
            return;
        }

        Debug.Log(gridPosition,this);

        // Get the currently selected block
        BuildingBlock block = InventoryManager.GetSelectedBuildingBlock();
        if (block != null) 
        {
            if (GridManager.TryInstantiateBuildingBlock(gridPosition, block, _currentAlignment))
            {
                InventoryManager.TryRemove(block);
            }
        }
    }

    private void DestroyBlockAtMousePoint()
    {
        if(!CameraToWorld.TryGetGameObjectAtMousePosition(out GameObject target))
        {
            Debug.LogWarning("Tried to destroy BuildingBlock outside the grid.");
            return;
        }

        Debug.Log(target,this);
        BuildingBlockDisplay targetBlock = target.GetComponent<BuildingBlockDisplay>();

        if (targetBlock == null)
        {
            Debug.Log("Tried to destroy GameObject, that is not a BuildingBlock.");
            return;
        } 

        InventoryManager.Add(targetBlock.Block);
        GridManager.DestroyBlock(targetBlock);
    }
    
}
