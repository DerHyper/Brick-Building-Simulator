using System;
using UnityEngine;
using Cinemachine;

public class InputManager : MonoBehaviour
{
    public GridManager GridManager;
    public InventoryManager InventoryManager;
    public MenuManager MenuManager;
    public Orientation.Alignment CurrentAlignment{get; private set;} = Orientation.Alignment.North;
    public CinemachineVirtualCamera VirtualCamera;
    [SerializeField]
    private float _zoomSpeed = 0.1f;
    [SerializeField]
    private float _minZoom = 2.0f;
    [SerializeField]
    private float _maxZoom = 20.0f;

    // TODO: This could be exchanged for an event system
    private void Update()
    {
        CheckInput();
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
        if (Input.GetButton("Vertical")) // Arrow Up/Down keys
        {
            ZoomCamera();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateRight();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.SwitchExitCanvasAvailability();
        }
    }

    private void ZoomCamera()
    {
        var transposer = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 offsetDifference = new Vector3(0, Input.GetAxis("Vertical"), Input.GetAxis("Vertical"))*_zoomSpeed;
        Vector3 rawOffset = transposer.m_FollowOffset + offsetDifference;
        
        float clipedY = Math.Max(_minZoom, Math.Min(rawOffset.y, _maxZoom));
        float clipedZ = Math.Max(_minZoom, Math.Min(rawOffset.z, _maxZoom));
        Vector3 clipedOffset = new(rawOffset.x, clipedY, clipedZ);

        transposer.m_FollowOffset = clipedOffset;
    }

    private void RotateRight()
    {
        CurrentAlignment = Orientation.RotateRight(CurrentAlignment);
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
            if (GridManager.TryInstantiateBuildingBlock(gridPosition, block, CurrentAlignment))
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
            Debug.LogWarning("Tried to destroy GameObject, that is not a BuildingBlock.");
            return;
        } 

        InventoryManager.Add(targetBlock.Block);
        GridManager.DestroyBlock(targetBlock);
    }
    
}
