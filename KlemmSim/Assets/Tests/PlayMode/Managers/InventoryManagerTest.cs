using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class InventoryManagerTest
{
    // Note: These tests expect that the scene already has
    // a InventoryManager with a itemPrefab in it
    // This is because WebGL does not have access to any prefabs in memory
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

    [UnityTest] // Add block, Remove same block returns "true"
    public IEnumerator Remove_RemoveExistingBlock_ReturnTrue()
    {
        InventoryManager inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        yield return null; // Wait for initialization
        BuildingBlock existingBlock = new BuildingBlock();
        inventoryManager.Add(existingBlock);
        yield return null; // Wait for initialization

        bool successful = inventoryManager.TryRemove(existingBlock);

        Assert.IsTrue(successful);
        yield return null;
    }

    [UnityTest] // Remove not existing block returns "false"
    public IEnumerator Remove_RemoveNotExistingBlock_ReturnFalse()
    {
        InventoryManager inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        yield return null; // Wait for initialization
        BuildingBlock notExistingBlock = new BuildingBlock();
        // No item was added

        bool successful = inventoryManager.TryRemove(notExistingBlock);

        Assert.IsFalse(successful);
        yield return null;
    }

    [UnityTest] // Add block, remove other block returns "false"
    public IEnumerator Remove_RemoveOtherBlock_ReturnFalse()
    {
        InventoryManager inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        yield return null; // Wait for initialization
        BuildingBlock existingBlock = new BuildingBlock();
        inventoryManager.Add(existingBlock);
        yield return null; // Wait for initialization
        BuildingBlock notExistingBlock = new BuildingBlock(); // This block will not be added

        bool successful = inventoryManager.TryRemove(notExistingBlock);

        Assert.IsFalse(successful);
        yield return null;
    }

    [UnityTest] // Add 2 block, remove 3 returns {true, true, false}
    public IEnumerator Remove_AddAndRemoveMultibleBlocks_ReturnWorksAccordingly()
    {
        InventoryManager inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        yield return null; // Wait for initialization
        BuildingBlock[] blocks = InitBlocks(3);
        inventoryManager.Add(blocks[0]);
        inventoryManager.Add(blocks[1]);
        // the third Block will not be added 
        bool[] results = new bool[3];
        bool[] expectedResults = {true, true, false};
        yield return null; // Wait for initialization

        results[0] = inventoryManager.TryRemove(blocks[0]);
        results[1] = inventoryManager.TryRemove(blocks[1]);
        results[2] = inventoryManager.TryRemove(blocks[2]);

        Assert.AreEqual(expectedResults, results);
        yield return null;
    }

    // Init an array of blocks with different names
    private BuildingBlock[] InitBlocks(int amount)
    {
        BuildingBlock[] blocks = new BuildingBlock[amount];
        for (int i=0; i<amount; i++)
        {
            blocks[i] = new BuildingBlock{name = "Block" + i.ToString()};
        }

        return blocks;
    }
}
