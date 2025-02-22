using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


public class GridManagerTest
{
    // Note: These tests expect that the scene already has
    // a GridManager with a BuildingBlockParent in it
    [SetUp]
    public void SetUpScene()
    {
        SceneManager.LoadScene("InventoryTestScene");
    }

    [TearDown]
    public void TearDownScene()
    {
        SceneManager.UnloadSceneAsync("InventoryTestScene");
    }

    private GridManager InitiateGridManager(Grid3D grid)
    {
        GridManager gridManager = new GameObject().AddComponent<GridManager>();
        gridManager.ReplaceGrid(grid);
        return gridManager;
    }
    private BuildingBlock InitiateBuildingBlock(Vector3Int size)
    {
        BuildingBlock block = ScriptableObject.CreateInstance<BuildingBlock>();
        block.Model = new GameObject().transform;
        block.SizeX = size.x;
        block.SizeY = size.y;
        block.SizeZ = size.z;

        return block;
    }

    [UnityTest] // Test: Block can be placed
    public IEnumerator InstantiateBuildingBlockAtPosition_InsideGrid_InstantiateBlock()
    {
        // Arrange
        // Instantiate Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = new GameObject().AddComponent<GridManager>();
        yield return null; // Skiping Frame: Wait for start Method to run
        gridManager.ReplaceGrid(grid);
        // Instantiate Building Block
        BuildingBlock originalBlock = InitiateBuildingBlock(Vector3Int.one);
        Vector3Int position = new Vector3Int(2,2,2);

        // Act
        gridManager.TryInstantiateBuildingBlock(position, originalBlock);

        // Assert
        BuildingBlock blockReferenceInGrid = grid.GetBuildingBlock(position);
        Assert.AreEqual(originalBlock, blockReferenceInGrid);
        yield return null;
    }

    [UnityTest] // Test: Block can't be placed, if it clipps out of the grid
    public IEnumerator InstantiateBuildingBlockAtPosition_ClippingOutGrid_NoBlockInstantiated()
    {
        // Arrange
        // Instantiate Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = new GameObject().AddComponent<GridManager>();
        yield return null; // Skiping Frame: Wait for start Method to run
        gridManager.ReplaceGrid(grid);
        // Instantiate Building Block
        BuildingBlock originalBlock = InitiateBuildingBlock(new Vector3Int(1,1,4)); // 1x1x4 block 
        Vector3Int position = new Vector3Int(2,2,2); // block should be clipping out at 2;2;5
        yield return null; // Skip another frame to ensure that the block is initialized correctly
        // Act
        gridManager.TryInstantiateBuildingBlock(position, originalBlock);

        // Assert
        BuildingBlockDisplay[] blocksInGrid = gridManager.GetBlocksInGrid();
        Assert.IsEmpty(blocksInGrid);
        yield return null;
    }

    [UnityTest] // Test: Block can't be placed inside another block
    public IEnumerator InstantiateBuildingBlockAtPosition_SameSpaceOccupied_NoOverride()
    {
        // Arrange
        // Instantiate Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = new GameObject().AddComponent<GridManager>();
        yield return null; // Skiping Frame: Wait for start Method to run
        gridManager.ReplaceGrid(grid);
        // Instantiate original Block
        BuildingBlock originalBlock = InitiateBuildingBlock(Vector3Int.one);
        Vector3Int originalPosition = Vector3Int.zero;
        gridManager.TryInstantiateBuildingBlock(originalPosition, originalBlock);
        // Instantiate blocked Block
        BuildingBlock blockedBlock = InitiateBuildingBlock(Vector3Int.one);
        
        // Act
        // Try override original block
        gridManager.TryInstantiateBuildingBlock(originalPosition, blockedBlock);

        // Assert
        // No override happened
        BuildingBlock blockReferenceInGrid = grid.GetBuildingBlock(originalPosition);
        Assert.AreSame(originalBlock, blockReferenceInGrid);
        yield return null;
    }
    
    [UnityTest] // Test: Block can't be placed inside overlaping block
    public IEnumerator InstantiateBuildingBlockAtPosition_Overlaping_NoOverride()
    {
        // Arrange
        // Instantiate Grid and GridManager
        // Instantiate Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = new GameObject().AddComponent<GridManager>();
        yield return null; // Skiping Frame: Wait for start Method to run
        gridManager.ReplaceGrid(grid);
        // Instantiate original Block that overrides the whole Grid
        BuildingBlock originalBlock = InitiateBuildingBlock(new Vector3Int(3,3,3));
        Vector3Int originalPosition = Vector3Int.zero;
        gridManager.TryInstantiateBuildingBlock(originalPosition, originalBlock);
        // Instantiate blocked Block
        BuildingBlock blockedBlock = InitiateBuildingBlock(Vector3Int.one);
        Vector3Int blockedPosition = new Vector3Int(2,2,2);
        
        // Act
        // Try override overlaping block
        gridManager.TryInstantiateBuildingBlock(blockedPosition, blockedBlock);

        // Assert
        // No override happened
        BuildingBlock blockReferenceInGrid = grid.GetBuildingBlock(blockedPosition);
        Assert.AreSame(originalBlock, blockReferenceInGrid);
        yield return null;
    }



    [UnityTest] // Test: Clearing grid doesnt with no objets doesnt throw exceptions
    public IEnumerator ClearGrid_EmptyGrid_NoException()
    {
        // Arrange
        // Instantiate Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = InitiateGridManager(grid);

        // Act
        TestDelegate act = () => gridManager.ClearGrid();

        // Assert
        Assert.That(act, Throws.Nothing);
        yield return null;
    }

    [UnityTest] // Test Clearing grid works for one block
    public IEnumerator ClearGrid_BlockOnGrid_GridEmpty()
    {
        // Arrange
        // Instantiate Block, Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = InitiateGridManager(grid);
        BuildingBlock block = InitiateBuildingBlock(Vector3Int.one);
        Vector3Int position = new Vector3Int(2,2,2);
        // Place Block in Grid
        gridManager.TryInstantiateBuildingBlock(position, block);

        // Act
        gridManager.ClearGrid();
        yield return null; // Skiping Frame: Destroying Objects happens at the and of a frame

        // Assert
        BuildingBlockDisplay[] blocksInGrid = gridManager.GetBlocksInGrid();
        Assert.IsEmpty(blocksInGrid);
    }

    [UnityTest] // Test DestroyBlock works for one block
    public IEnumerator DestroyBlock_BlockOnGrid_GridEmpty()
    {
        // Arrange
        // Instantiate Block, Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = InitiateGridManager(grid);
        BuildingBlock block = InitiateBuildingBlock(Vector3Int.one);
        Vector3Int position = new Vector3Int(2,2,2);
        // Place Block in Grid, then get it
        gridManager.TryInstantiateBuildingBlock(position, block);
        BuildingBlockDisplay justPlacedBlock = gridManager.GetBlocksInGrid()[0];

        // Act
        gridManager.DestroyBlock(justPlacedBlock);
        yield return null; // Skiping Frame: Destroying Objects happens at the and of a frame

        // Assert
        BuildingBlockDisplay[] blocksInGrid = gridManager.GetBlocksInGrid();
        Assert.IsEmpty(blocksInGrid);
    }

    [UnityTest] // Test DestroyBlocks Destroys exactly the mentioned Blocks
    public IEnumerator DestroyBlocks_BlockOnGrid_GridEmpty()
    {
        // Arrange
        // Instantiate Block, Grid and GridManager
        Grid3D grid = new Grid3D(5,5,5);
        GridManager gridManager = new GameObject().AddComponent<GridManager>();
        yield return null; // Skiping Frame: Wait for start Method to run
        gridManager.ReplaceGrid(grid);
        BuildingBlock blockData = InitiateBuildingBlock(Vector3Int.one);
        Vector3Int position0 = new Vector3Int(2,2,1);
        Vector3Int position1 = new Vector3Int(2,2,2);
        Vector3Int position2 = new Vector3Int(2,2,3);
        // Place 3 Block in Grid, then get them
        gridManager.TryInstantiateBuildingBlock(position0, blockData);
        gridManager.TryInstantiateBuildingBlock(position1, blockData);
        gridManager.TryInstantiateBuildingBlock(position2, blockData);
        BuildingBlockDisplay[] justPlacedBlocks = gridManager.GetBlocksInGrid();
        BuildingBlockDisplay[] blocksToDestroy = justPlacedBlocks.Take(2).ToArray(); // First 2 Blocks
        BuildingBlockDisplay blockThatShouldNotBeDestroyed = justPlacedBlocks.Last();

        // Act
        gridManager.DestroyBlocks(blocksToDestroy);
        yield return null; // Skiping Frame: Destroying Objects happens at the and of a frame

        // Assert
        // Only the one Block should not be destroyed
        BuildingBlockDisplay blockFoundInGrid = gridManager.GetBlocksInGrid()[0];
        Assert.AreSame(blockThatShouldNotBeDestroyed, blockFoundInGrid);
    }
}
