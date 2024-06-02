using NUnit.Framework;
using UnityEngine;

public class SelfScaleToGridBottomTest
{
    private GridManager InstantiateGameObjectWithGridManager(Vector3Int size)
    {
        GameObject gameObject = new GameObject();
        GridManager gridManager = gameObject.AddComponent<GridManager>();
        gridManager.CreateOrReplaceGrid(size);

        return gridManager;
        
    }

    [Test]
    public void UpdateScale_bottomScalePositive_Scale()
    {
        // Arrange: 
        // Instantiate a GridManager with a gird has a size of 5x5x5
        GridManager gridManager = InstantiateGameObjectWithGridManager(new Vector3Int(5,5,5));

        // Instantiate a GameObject with a SelfScaleToGridBottom Component
        GameObject testObject = new GameObject();
        SelfScaleToGridBottom testComponent = testObject.AddComponent<SelfScaleToGridBottom>();
        float bottomScale = 1;
        float yScale = 1;
        Vector3 expectedScale = new Vector3(5, 1, 5);

        // Act
        testComponent.UpdateScale(gridManager, bottomScale, yScale);

        // Assert
        Assert.AreEqual(expectedScale, testObject.transform.localScale);
    }

    [Test]
    public void UpdateScale_bottomScaleFraction_Scale()
    {
        // Arrange: 
        // Instantiate a GridManager with a gird has a size of 5x5x5
        GridManager gridManager = InstantiateGameObjectWithGridManager(new Vector3Int(5,5,5));

        // Instantiate a GameObject with a SelfScaleToGridBottom Component
        GameObject testObject = new GameObject();
        SelfScaleToGridBottom testComponent = testObject.AddComponent<SelfScaleToGridBottom>();
        float bottomScale = 0.1f;
        float yScale = 1;
        Vector3 expectedScale = new Vector3(0.5f, 1, 0.5f);

        // Act
        testComponent.UpdateScale(gridManager, bottomScale, yScale);

        // Assert
        Assert.AreEqual(expectedScale, testObject.transform.localScale);
    }

    [Test]
    public void UpdateScale_bottomScaleNegative_Scale()
    {
        // Arrange: 
        // Instantiate a GridManager with a gird has a size of 5x5x5
        GridManager gridManager = InstantiateGameObjectWithGridManager(new Vector3Int(5,5,5));

        // Instantiate a GameObject with a SelfScaleToGridBottom Component
        GameObject testObject = new GameObject();
        SelfScaleToGridBottom testComponent = testObject.AddComponent<SelfScaleToGridBottom>();
        float bottomScale = -1;
        float yScale = 1;
        Vector3 expectedScale = new Vector3(-5, 1, -5);

        // Act
        testComponent.UpdateScale(gridManager, bottomScale, yScale);

        // Assert
        Assert.AreEqual(expectedScale, testObject.transform.localScale);
    }
}
