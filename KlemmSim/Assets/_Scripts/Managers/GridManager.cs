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
        if (grid.isNotInBuildingLimit(position))
        {
            Debug.Log(position.ToString() + " is not a valid Position to build.");
            return;
        }

        BuildingBlockDisplay blockDisplay = InstantiateBuildingBlock(position, block);

        grid.SetGridObject(position, blockDisplay);
    }

    // Instantiates a new building block as a child of the "BuildingBlocks"-GameObject
    private static BuildingBlockDisplay InstantiateBuildingBlock(Vector3Int position, BuildingBlock block)
    {
        Transform parent =  Finder.FindOrCreateGameObjectWithTag("BuildingBlocks").transform;

        GameObject blockGameObject = Instantiate(block.model, position, Quaternion.identity, parent).gameObject;
        BuildingBlockDisplay blockDisplay = blockGameObject.AddComponent<BuildingBlockDisplay>();
        blockDisplay.UpdateDisplay(block);

        return blockDisplay;
    }
}
