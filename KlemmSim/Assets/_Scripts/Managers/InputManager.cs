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
        Vector3Int gridPosition = CameraToWorld.GetMouseBlockPlacementPosition();

        gridManager.BuildBlock(gridPosition, block);
    }

    
}
