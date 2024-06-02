using UnityEngine;

public interface IGridManager
{
    public void InstantiateBuildingBlockAtPosition(Vector3Int position, BuildingBlock block);

    // Destroys every Block that can be found in this Grid
    public void ClearGrid();

    // Destroy all BuildingBlocks inside the targets array
    public void DestroyBlocks(BuildingBlockDisplay[] targets);

    // Destroy the target BuildingBlock
    public void DestroyBlock(BuildingBlockDisplay target);

    public Vector3Int GetSize();

    public void CreateOrReplaceGrid(Vector3Int size);
    public void CreateOrReplaceGrid(Grid3D grid);

}
