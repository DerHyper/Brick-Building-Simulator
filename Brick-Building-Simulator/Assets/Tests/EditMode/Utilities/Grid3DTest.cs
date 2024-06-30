using NUnit.Framework;
using UnityEngine;

public class Grid3DTest
{
    [Test]
    public void IsNotInBuildingLimit_InLimit_True()
    {
        Grid3D grid = new Grid3D(5,5,5);
        Vector3Int vector = new Vector3Int(2,2,2);

        bool result = grid.IsInsideBuildingLimit(vector);

        Assert.IsTrue(result);
    }

    [Test]
    public void IsNotInBuildingLimit_InLimitLowerEdgecase_True()
    {
        Grid3D grid = new Grid3D(5,5,5);
        Vector3Int vector = Vector3Int.zero;

        bool result = grid.IsInsideBuildingLimit(vector);

        Assert.IsTrue(result);
    }

    [Test]
    public void IsNotInBuildingLimit_InLimitUpperEdgecase_True()
    {
        int xMax = 5;
        int yMax = 5;
        int zMax = 5;
        Grid3D grid = new Grid3D(xMax, yMax, zMax);
        Vector3Int vector = new Vector3Int(xMax-1, yMax-1, zMax-1);

        bool result = grid.IsInsideBuildingLimit(vector);

        Assert.IsTrue(result);
    }

    [Test]
    public void IsNotInBuildingLimit_UnderLimit_False()
    {
        Grid3D grid = new Grid3D(5,5,5);
        Vector3Int vector = new Vector3Int(-3, 2, 2);

        bool result = grid.IsInsideBuildingLimit(vector);

        Assert.IsFalse(result);
    }

    [Test]
    public void IsNotInBuildingLimit_AboveLimit_False()
    {
        int xMax = 5;
        int yMax = 5;
        int zMax = 5;
        Grid3D grid = new Grid3D(xMax, yMax, zMax);
        Vector3Int vector = new Vector3Int(xMax+2, yMax-1, zMax-1);

        bool result = grid.IsInsideBuildingLimit(vector);

        Assert.IsFalse(result);
    }

    [Test]
    public void IsNotInBuildingLimit_AboveLimitEdgecase_False()
    {
        int xMax = 5;
        int yMax = 5;
        int zMax = 5;
        Grid3D grid = new Grid3D(xMax, yMax, zMax);
        Vector3Int vector = new Vector3Int(xMax, yMax, zMax);

        bool result = grid.IsInsideBuildingLimit(vector);

        Assert.IsFalse(result);
    }
}
