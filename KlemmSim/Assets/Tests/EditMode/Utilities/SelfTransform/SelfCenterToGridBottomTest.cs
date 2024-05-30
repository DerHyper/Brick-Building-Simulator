using NUnit.Framework;
using UnityEngine;

public class SelfCenterToGridBottomTest
{
    private GameObject InstantiateGameObjectWithGridManager(Vector3Int size)
    {
        GameObject gameObject = new GameObject();
        
        // Change parameters while GameObject is inactive
        gameObject.SetActive(false);
        GridManager gridManager = gameObject.AddComponent<GridManager>();
        gridManager.Size = size;
        gameObject.SetActive(true);

        return gameObject;
        
    }

    [Test]
    public void UpdatePosition_5x5x5Grid_CenterAtBottom()
    {
        // Arrange: Instantiate a GameObject with a GridManager and a SelfCenterToGridBottom Component.
        // The gird has a size of 5x5x5
        GameObject testObject = InstantiateGameObjectWithGridManager(new Vector3Int(5,5,5));
        SelfCenterToGridBottom testComponent = testObject.AddComponent<SelfCenterToGridBottom>();
        Vector3 expectedPosition = new Vector3(2.5f, 0, 2.5f);

        // Act
        testComponent.UpdatePosition();

        // Assert
        Assert.AreEqual(testObject.transform.position, expectedPosition);
    }

    [Test]
    public void UpdatePosition_5x5x5Grid1Distance_CenterWithDistance()
    {
        // Arrange: Instantiate a GameObject with a GridManager and a SelfCenterToGridBottom Component.
        // The gird has a size of 5x5x5, distanceFromBottom is 1
        GameObject testObject = InstantiateGameObjectWithGridManager(new Vector3Int(5,5,5));
        SelfCenterToGridBottom testComponent = testObject.AddComponent<SelfCenterToGridBottom>();
        testComponent.distanceFromBottom = 1;
        Vector3 expectedPosition = new Vector3(2.5f, 1, 2.5f);

        // Act
        testComponent.UpdatePosition();

        // Assert
        Assert.AreEqual(testObject.transform.position, expectedPosition);
    }
}
