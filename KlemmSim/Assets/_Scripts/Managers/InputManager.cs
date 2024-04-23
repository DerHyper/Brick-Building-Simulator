using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GridManager gridManager;
    public GameObject block;

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

        gridManager.BuildBlock(gridPosition, block);
    }

    
}
