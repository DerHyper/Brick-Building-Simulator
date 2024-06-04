using UnityEngine;

public class InputManager : MonoBehaviour
{
    GridManager gridManager;
    public BuildingBlock block;

    private void Start()
    {
        gridManager = Finder.FindOrCreateObjectOfType<GridManager>();
    }

    // Could be exchanged for an event system
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

    private void BuildBlockAtMousePoint()
    {
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
        gridManager.InstantiateBuildingBlockAtPosition(gridPosition, block);
    }

    private void DestroyBlockAtMousePoint()
    {
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

        gridManager.DestroyBlock(targetBlock);
    }
    
}
