using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector3Int Size;
    private Grid3D grid;
    private void Start()
    {
        grid = new Grid3D(Size.x, Size.y, Size.z);
    }

    public void BuildBlock(Vector3Int position, BuildingBlock block)
    {
        if (ContainsBuildErrors(position, block)) return;

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(position, block);

        SetGridObjects(position, block, blockDisplay);

    }

    private void SetGridObjects(Vector3Int position, BuildingBlock block, BuildingBlockDisplay blockDisplay)
    {
        for (int xOffset = 0; xOffset < block.sizeX; xOffset++)
            for (int yOffset = 0; yOffset < block.sizeY; yOffset++)
                for (int zOffset = 0; zOffset < block.sizeZ; zOffset++)
                {
                    Vector3Int offset = new Vector3Int(xOffset, yOffset, zOffset);
                    Vector3Int positionWithOffset = position + offset;
                    grid.SetGridObject(positionWithOffset, blockDisplay);
                }
    }

    private bool ContainsBuildErrors(Vector3Int position, BuildingBlock block)
    {
        if (grid.IsNotInBuildingLimit(position))
        {
            Debug.Log(position.ToString() + " is not a valid Position to build.");
            return true;
        }

        if (!CanOccupieSpace(position, block))
        {
            Debug.Log("Block '" + block.name + "' is overlaping another Block near " + position.ToString());
            return true;
        }

        return false;
    }

    // Returns false if any block inside the specified space is being occupied.
    private bool CanOccupieSpace(Vector3Int position, BuildingBlock block)
    {
        for (int xOffset = 0; xOffset < block.sizeX; xOffset++)
            for (int yOffset = 0; yOffset < block.sizeY; yOffset++)
                for (int zOffset = 0; zOffset < block.sizeZ; zOffset++)
                {
                    Vector3Int offset = new Vector3Int(xOffset, yOffset, zOffset);
                    Vector3Int positionWithOffset = position + offset;
                    if (grid.IsOccupied(positionWithOffset)) return false;
                }

        return true;
    }

    // Instantiates a new building block as a child of the "BuildingBlocks"-GameObject
    private static BuildingBlockDisplay InstantiateBuildingBlock(Vector3Int position, BuildingBlock block)
    {
        Transform parent =  Finder.FindOrCreateGameObjectWithTag("BuildingBlocks").transform;

        GameObject blockGameObject = Instantiate(block.model, position, Quaternion.identity, parent).gameObject;
        BuildingBlockDisplay blockDisplay = blockGameObject.AddComponent<BuildingBlockDisplay>();
        blockDisplay.UpdateDisplay(position, block);

        return blockDisplay;
    }

    public void DestroyBlock(BuildingBlockDisplay target)
    {
        Vector3Int position = target.position;
        BuildingBlock block = target.block;
        DestroyGridObjects(position, block);
        Destroy(target.gameObject);
    }

    private void DestroyGridObjects(Vector3Int position, BuildingBlock block)
    {
        for (int xOffset = 0; xOffset < block.sizeX; xOffset++)
            for (int yOffset = 0; yOffset < block.sizeY; yOffset++)
                for (int zOffset = 0; zOffset < block.sizeZ; zOffset++)
                {
                    Vector3Int offset = new Vector3Int(xOffset, yOffset, zOffset);
                    Vector3Int positionWithOffset = position + offset;
                    grid.DestroyGridObject(positionWithOffset);
                }
    }
}
