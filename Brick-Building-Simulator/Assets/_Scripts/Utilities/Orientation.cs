using UnityEngine;

/// <summary>
/// Orientation in 4 directions called Alignment.
/// Also contains methods to work with different Alignments.
/// </summary>
public static class Orientation
{
    public enum Alignment
    {
        North,
        East,
        South,
        West
    }

    /// <summary>
    /// Return the given Alignment rotated to the right.
    /// </summary>
    public static Alignment RotateRight(Alignment direction)
    {
        return direction switch
        {
            Alignment.East => Alignment.South,
            Alignment.South => Alignment.West,
            Alignment.West => Alignment.North,
            _ => Alignment.East,
        };
    }

    /// <summary>
    /// Return the given Alignment rotated to the left.
    /// </summary>
    public static Alignment RotateLeft(Alignment direction)
    {
        return direction switch
        {
            Alignment.East => Alignment.North,
            Alignment.South => Alignment.East,
            Alignment.West => Alignment.South,
            _ => Alignment.West,
        };
    }

    /// <summary>
    /// Returns the given alignment as a Quaternion.
    /// </summary>
    public static Quaternion ToQuaternion(Alignment direction)
    {
        return direction switch
        {
            Alignment.East => Quaternion.Euler(0, 90, 0),
            Alignment.South => Quaternion.Euler(0, 180, 0),
            Alignment.West => Quaternion.Euler(0, 270, 0),
            _ => Quaternion.Euler(0, 0, 0),
        };
    }

    /// <summary>
    /// Calculates the offset that has to be added to the current position to realign a block with its original position on the grid.
    /// </summary>
    /// <param name="direction">Tageted rotation.</param>
    /// <param name="block">Block that will be rotated.</param>
    /// <returns>Offset that has to be added.</returns>
    public static Vector3Int GetRotationOffset(Alignment direction, BuildingBlock block)
    {
        return direction switch
        {
            Alignment.East => new Vector3Int(0, 0, block.SizeX) / block.SizeX,
            Alignment.South => new Vector3Int(block.SizeX, 0, block.SizeX) / block.SizeX,
            Alignment.West => new Vector3Int(block.SizeX, 0, 0) / block.SizeX,
            _ => new Vector3Int(0, 0, 0) / block.SizeX,
        };
    }

    /// <inheritdoc />
    /// <param name="blockDisplay">Block that will be rotated.</param>
    public static Vector3Int GetRotatedSize(Alignment alignment, BuildingBlockDisplay blockDisplay)
    {
        return GetRotatedSize(alignment, blockDisplay.Block);
    }

    /// <summary>
    /// Calculates the size of a block after being rotated.
    /// </summary>
    /// <param name="alignment">Tageted rotation.</param>
    /// <param name="block">Block that will be rotated.</param>
    /// <returns>Size the rotated block.</returns>
    public static Vector3Int GetRotatedSize(Alignment alignment, BuildingBlock block)
    {
        var coveredVoxels = alignment switch
        {
            Alignment.East => new Vector3Int(block.SizeZ, block.SizeY, -block.SizeX),
            Alignment.South => new Vector3Int(-block.SizeX, block.SizeY, -block.SizeZ),
            Alignment.West => new Vector3Int(-block.SizeZ, block.SizeY, block.SizeX),
            _ => new Vector3Int(block.SizeX, block.SizeY, block.SizeZ),
        };
        return coveredVoxels;
    }
}
