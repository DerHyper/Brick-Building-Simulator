using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GridManager GridManager;
    public InventoryManager InventoryManager;
    private Orientation.Alignment currentAlignment = Orientation.Alignment.North;

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
        if (Input.GetButtonDown("Vertical"))
        {
            currentAlignment = Orientation.RotateRight(currentAlignment);
        }
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
            if (GridManager.TryInstantiateBuildingBlock(gridPosition, block, currentAlignment))
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
