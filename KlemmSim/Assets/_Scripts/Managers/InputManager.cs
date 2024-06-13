using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GridManager GridManager;
    public InventoryManager InventoryManager;

    // TODO: This could be exchanged for an event system
    private void Update() 
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            BuildBlockAtMousePoint();
        }
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            DestroyBlockAtMousePoint();
        }
    }

    // Check if position is valid, if so, place the currently selected block and remove it from inventory
    private void BuildBlockAtMousePoint()
    {
        // TODO: This should be exchanged for normal error handling
        // Exceptions use to much memory to be used in one Frame
        Vector3Int gridPosition;
        try
        {
            gridPosition = CameraToWorld.GetMouseBlockPlacementPosition();
        }
        catch (OutsideGridException)
        {
            Debug.Log("Exception catched: Tried to place BuildingBlock outside the grid.");
            return;
        }
        Debug.Log(gridPosition,this);

        // Get the currently selected block
        BuildingBlock block = InventoryManager.GetSelectedBuildingBlock();
        if (block != null) 
        {
            if (GridManager.TryInstantiateBuildingBlock(gridPosition, block))
            {
                InventoryManager.TryRemove(block);
            }
        }
    }

    private void DestroyBlockAtMousePoint()
    {
        // TODO: This should be exchanged for normal error handling
        // Exceptions use to much memory to be used in one Frame
        GameObject target;
        try
        {
            target = CameraToWorld.GetGameObjectAtMousePosition();
        }
        catch (OutsideGridException)
        {
            Debug.Log("Exception catched: Tried to destroy BuildingBlock outside the grid.");
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
