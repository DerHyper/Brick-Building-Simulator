using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GridManager gridManager;
    public BuildingBlock block;

    private void Start()
    {
        gridManager = Finder.FindGridManager();
    }

    // Could be exchanged for an event system
    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            BuildBlockAtMousePoint();
        }
        if (Input.GetMouseButtonDown(1))
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
            Debug.Log("Exception catched: Tried to place BuildingBrick outside the grid.");
            return;
        }

        Debug.Log(gridPosition,this);
        gridManager.BuildBlock(gridPosition, block);
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
