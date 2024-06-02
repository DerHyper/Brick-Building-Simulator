using NUnit.Framework;
using UnityEngine;

public class SelfCenterToGridBottomTest
{
    private GridManager InstantiateGameObjectWithGridManager(Vector3Int size)
    {
        GameObject gameObject = new GameObject();
        GridManager gridManager = gameObject.AddComponent<GridManager>();
        gridManager.CreateOrReplaceGrid(size);

        return gridManager;
        
    }

    [Test]
    public void UpdatePosition_5x5x5Grid_CenterAtBottom()
    {
        // Arrange: 
        // Instantiate a GridManager with a gird has a size of 5x5x5
        GridManager gridManager = InstantiateGameObjectWithGridManager(new Vector3Int(5,5,5));

        // Instantiate a GameObject with a SelfCenterToGridBottom Component, the distance from the bottom should be 0
        GameObject testObject = new GameObject();
        SelfCenterToGridBottom testComponent = testObject.AddComponent<SelfCenterToGridBottom>();
        int distanceFromBottom = 0;
        Vector3 expectedPosition = new Vector3(2.5f, 0, 2.5f);

        // Act
        testComponent.UpdatePosition(gridManager, distanceFromBottom);

        // Assert
        Assert.AreEqual(expectedPosition, testObject.transform.position);
    }

    [Test]
    public void UpdatePosition_5x5x5Grid1Distance_CenterWithDistance()
    {
        // Arrange: 
        // Instantiate a GridManager with a gird has a size of 5x5x5
        GridManager gridManager = InstantiateGameObjectWithGridManager(new Vector3Int(5,5,5));

        // Instantiate a GameObject with a SelfCenterToGridBottom Component, the distance from the bottom should be 1
        GameObject testObject = new GameObject();
        SelfCenterToGridBottom testComponent = testObject.AddComponent<SelfCenterToGridBottom>();
        int distanceFromBottom = 1;
        Vector3 expectedPosition = new Vector3(2.5f, 1, 2.5f);

        // Act
        testComponent.UpdatePosition(gridManager, distanceFromBottom);

        // Assert
        Assert.AreEqual(expectedPosition, testObject.transform.position);
    }
}
