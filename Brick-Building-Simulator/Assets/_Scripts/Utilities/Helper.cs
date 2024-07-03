using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class with general helper methods.
/// </summary>
public static class Helper
{
    /// <returns></returns>
    /// <inheritdoc cref="Helper.DoWithinRange{InType, OutType}(Vector3Int, Vector3Int, Func{Vector3Int, InType, OutType}, InType)"/>  
    public static void DoWithinRange<T>(Vector3Int startPosition, Vector3Int endOffset, Action<Vector3Int> method)
    {
        int xCurrent = 0;
        while (Math.Abs(endOffset.x) - Math.Abs(xCurrent) > 0)
        {
            int yCurrent = 0;
            while (Math.Abs(endOffset.y) - Math.Abs(yCurrent) > 0)
            {
                int zCurrent = 0;
                while (Math.Abs(endOffset.z) - Math.Abs(zCurrent) > 0)
                {
                    Vector3Int currentOffset = new Vector3Int(xCurrent, yCurrent, zCurrent);
                    Vector3Int positionWithOffset = startPosition + currentOffset;
                    method(positionWithOffset);

                    if (endOffset.z >= 0) zCurrent++;
                    else zCurrent--;
                }

                if (endOffset.y >= 0) yCurrent++;
                else yCurrent--;
            }

            if (endOffset.x >= 0) xCurrent++;
            else xCurrent--;
        }
    }

    /// <returns></returns>
    /// <inheritdoc cref="Helper.DoWithinRange{InType, OutType}(Vector3Int, Vector3Int, Func{Vector3Int, InType, OutType}, InType)"/>
    public static void DoWithinRange<T>(Vector3Int startPosition, Vector3Int endOffset, Action<Vector3Int, T> method, T parameter)
    {
        int xCurrent = 0;
        while (Math.Abs(endOffset.x) - Math.Abs(xCurrent) > 0)
        {
            int yCurrent = 0;
            while (Math.Abs(endOffset.y) - Math.Abs(yCurrent) > 0)
            {
                int zCurrent = 0;
                while (Math.Abs(endOffset.z) - Math.Abs(zCurrent) > 0)
                {
                    Vector3Int currentOffset = new Vector3Int(xCurrent, yCurrent, zCurrent);
                    Vector3Int positionWithOffset = startPosition + currentOffset;
                    method(positionWithOffset, parameter);
                    if (endOffset.z >= 0) zCurrent++;
                    else zCurrent--;
                }

                if (endOffset.y >= 0) yCurrent++;
                else yCurrent--;
            }

            if (endOffset.x >= 0) xCurrent++;
            else xCurrent--;
        }
    }

    /// <inheritdoc cref="Helper.DoWithinRange{InType, OutType}(Vector3Int, Vector3Int, Func{Vector3Int, InType, OutType}, InType)"/>
    public static List<OutType> DoWithinRange<OutType>(Vector3Int startPosition, Vector3Int endOffset, Func<Vector3Int, OutType> method)
    {
        List<OutType> result = new List<OutType>();

        int xCurrent = 0;
        while (Math.Abs(endOffset.x) - Math.Abs(xCurrent) > 0)
        {
            int yCurrent = 0;
            while (Math.Abs(endOffset.y) - Math.Abs(yCurrent) > 0)
            {
                int zCurrent = 0;
                while (Math.Abs(endOffset.z) - Math.Abs(zCurrent) > 0)
                {
                    Vector3Int currentOffset = new Vector3Int(xCurrent, yCurrent, zCurrent);
                    Vector3Int positionWithOffset = startPosition + currentOffset;
                    result.Add(method(positionWithOffset));
                    if (endOffset.z >= 0) zCurrent++;
                    else zCurrent--;
                }

                if (endOffset.y >= 0) yCurrent++;
                else yCurrent--;
            }

            if (endOffset.x >= 0) xCurrent++;
            else xCurrent--;
        }

        return result;
    }

    /// <summary>
    /// Calls the method or every possible position between the start and end position  
    /// </summary>
    /// <typeparam name="InType">The type of the parameter used by the method given as a parameter.</typeparam>
    /// <typeparam name="OutType">The output type returned by the method given as a parameter.</typeparam>
    /// <param name="startPosition">Position, which will be used as starting point when iterating.</param>
    /// <param name="endOffset">Offset, which will be iterated towards to. Can be positive or negative.</param>
    /// <param name="method">Will be run for every step of the iteration.</param>
    /// <param name="parameter">Used by the method given as a parameter.</param>
    /// <returns>List of all the outputs from the given method.</returns>
    public static List<OutType> DoWithinRange<InType, OutType>(Vector3Int startPosition, Vector3Int endOffset, Func<Vector3Int, InType, OutType> method, InType parameter)
    {
        List<OutType> result = new List<OutType>();

        int xCurrent = 0;
        while (Math.Abs(endOffset.x) - Math.Abs(xCurrent) > 0)
        {
            int yCurrent = 0;
            while (Math.Abs(endOffset.y) - Math.Abs(yCurrent) > 0)
            {
                int zCurrent = 0;
                while (Math.Abs(endOffset.z) - Math.Abs(zCurrent) > 0)
                {
                    Vector3Int currentOffset = new Vector3Int(xCurrent, yCurrent, zCurrent);
                    Vector3Int positionWithOffset = startPosition + currentOffset;
                    result.Add(method(positionWithOffset, parameter));
                    if (endOffset.z >= 0) zCurrent++;
                    else zCurrent--;
                }

                if (endOffset.y >= 0) yCurrent++;
                else yCurrent--;
            }

            if (endOffset.x >= 0) xCurrent++;
            else xCurrent--;
        }

        return result;
    }
}
