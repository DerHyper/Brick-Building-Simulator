using UnityEngine;

public static class Orientation
{
    public enum Alignment
    {
        North,
        East,
        South,
        West
    }

    public static Alignment RotateRight(Alignment direction)
    {
        switch (direction)
        {
            default:
            case Alignment.North: 
                return Alignment.East;
            case Alignment.East: 
                return Alignment.South;
            case Alignment.South: 
                return Alignment.West;
            case Alignment.West: 
                return Alignment.North;
        }
    }

    public static Alignment RotateLeft(Alignment direction)
    {
        switch (direction)
        {
            default:
            case Alignment.North: 
                return Alignment.West;
            case Alignment.East: 
                return Alignment.North;
            case Alignment.South: 
                return Alignment.East;
            case Alignment.West: 
                return Alignment.South;
        }
    }

    public static Quaternion ToQuaternion(Alignment direction)
    {
        switch (direction)
        {
            default:
            case Alignment.North: 
                return Quaternion.Euler(0,0,0);
            case Alignment.East: 
                return Quaternion.Euler(0,90,0);
            case Alignment.South: 
                return Quaternion.Euler(0,180,0);
            case Alignment.West: 
                return Quaternion.Euler(0,270,0);
        }
    }

    public static Vector3Int GetRotationOffset(Alignment direction, BuildingBlock block)
    {
        switch (direction)
        {
            default:
            case Alignment.North: 
                return new Vector3Int(0,0,0)/block.SizeX;
            case Alignment.East: 
                return new Vector3Int(0,0,block.SizeX)/block.SizeX;
            case Alignment.South: 
                return new Vector3Int(block.SizeX,0,block.SizeX)/block.SizeX;
            case Alignment.West: 
                return new Vector3Int(block.SizeX,0,0)/block.SizeX;
        }
    }

    public static Vector3Int GetRotatedSize(Alignment alignment, BuildingBlockDisplay blockDisplay)
    {
        return GetRotatedSize(alignment, blockDisplay.Block);
    }

    public static Vector3Int GetRotatedSize(Alignment alignment, BuildingBlock block)
    {
        Vector3Int coveredVoxels;
        switch (alignment)
        {
            default:
            case Orientation.Alignment.North:
                coveredVoxels = new Vector3Int(block.SizeX, block.SizeY, block.SizeZ);
                break;
            case Orientation.Alignment.East:
                coveredVoxels = new Vector3Int(block.SizeZ, block.SizeY, -block.SizeX);
                break;
            case Orientation.Alignment.South:
                coveredVoxels = new Vector3Int(-block.SizeX, block.SizeY, -block.SizeZ);
                break;
            case Orientation.Alignment.West:
                coveredVoxels = new Vector3Int(-block.SizeZ, block.SizeY, block.SizeX);
                break;
        }

        return coveredVoxels;
    }
}
