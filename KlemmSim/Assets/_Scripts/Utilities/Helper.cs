using System;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    // Calls the method or every possible position between the start and end position   
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

    // DoWithinRange but with an aditional parameter 
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

    // DoWithinRange but with return type
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

    // DoWithinRange but with an aditional parameter and return type
    public static List<OutType> DoWithinRange<InType,OutType>(Vector3Int startPosition, Vector3Int endOffset, Func<Vector3Int, InType, OutType> method, InType parameter)
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
