using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BlockReferenceManagerTest
{
    // Note: This tests expects that the scene already has
    // a BlockReferenceManager with a block named "1x1x1" in it
    // and that no block with the name "NonExistingBlock" is in it.
    // This is because we do not have acces to any blocks in memory

    [SetUp]
    public void SetUpScene()
    {
        SceneManager.LoadScene("BuildingScene");
    }

    [UnityTest]
    public IEnumerator GetBuildingBlockByName_BlockExists_ReturnBlock()
    {
        BlockReferenceManager blockReferenceManager = Finder.FindOrCreateObjectOfType<BlockReferenceManager>();
        string nameOfExistingBlock = "1x1x1";

        BuildingBlock block = blockReferenceManager.GetBuildingBlockByName(nameOfExistingBlock);

        Assert.AreEqual(nameOfExistingBlock, block.name);
        yield return null;
    }

    [UnityTest]
    public IEnumerator GetBuildingBlockByName_BlockDoesNotExist_ReturnOtherBlock()
    {
        BlockReferenceManager blockReferenceManager = Finder.FindOrCreateObjectOfType<BlockReferenceManager>();
        string nameOfBlock = "NonExistingBlock";

        BuildingBlock block = blockReferenceManager.GetBuildingBlockByName(nameOfBlock);

        Assert.AreNotEqual(nameOfBlock, block.name);
        yield return null;
    }
}
