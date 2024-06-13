using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRandomizer : MonoBehaviour
{
    public int MeanNumberOfBlocks = 40; // Mean amount of blocks of one type
    public int StandardDeviation = 5;
    public int MinBlocks = 2;
    public int MaxBlocks = 20;
    public int NumberOfBlockTypes = 5;
    public InventoryManager InventoryManager;
    public BlockReferenceManager BlockReferenceManager;
    private void Start()
    {
        InitInventory();
    }

    private void InitInventory()
    {
        // Generate block types and distrobution
        List<BuildingBlock> pickedBlockTypes = PickRandomBlocks(BlockReferenceManager.GetBuildingBlocks(), NumberOfBlockTypes);
        int[] distribution = GenerateDistrobutionOfBlocks();
        AddAllBlocksToInventory(pickedBlockTypes, distribution);
    }

    // Add the multible blocks multible times
    private void AddAllBlocksToInventory(List<BuildingBlock> pickedBlockTypes, int[] distribution)
    {
        for (int i = 0; i < distribution.Length; i++)
        {
            BuildingBlock block = pickedBlockTypes[i];
            int times = distribution[i];
            AddBlocks(block, times);
        }
    }

    // Add the same block multible times
    private void AddBlocks(BuildingBlock block, int times)
    {
        for (int i = 0; i < times; i++)
            InventoryManager.Add(block);
    }

    private int[] GenerateDistrobutionOfBlocks()
    {
        int[] distribution = new int[NumberOfBlockTypes];
        for (int i = 0; i < NumberOfBlockTypes; i++)
            distribution[i] = NormalRange(MeanNumberOfBlocks, StandardDeviation, MinBlocks, MaxBlocks);
        
        return distribution;
    }

    // Generates a pick from a normal distrobution within a range
    private int NormalRange(float mean, float standardDeviation, int minBlocks, int maxBlocks)
    {
        int pick = (int)PickNormalDistribution(mean, standardDeviation);
        int limitedPick = Math.Max(minBlocks, Math.Min(maxBlocks, pick)); // minBlocks <= limitedPick <= maxBlocks

        return limitedPick;
    }

    private List<BuildingBlock> PickRandomBlocks(List<BuildingBlock> possibleBlocks, int numberOfBlocks)
    {
        List<BuildingBlock> pickedBlocks = new List<BuildingBlock>();

        for (int i = 0; i < numberOfBlocks; i++)
        {
            // Pick a random Block
            int randomIndex = UnityEngine.Random.Range(0, possibleBlocks.Count);
            BuildingBlock randomBlockCopy = possibleBlocks[randomIndex];
            
            // Pop it into the list of pickedBlocks
            possibleBlocks.Remove(possibleBlocks[randomIndex]);
            pickedBlocks.Add(randomBlockCopy);
        }

        return pickedBlocks;
    }

    // Generates a pick from a normal distrubution using the Box–Muller transform for one dimension
    // Reference: https://de.wikipedia.org/wiki/Box-Muller-Methode
    private double PickNormalDistribution(float mean, float standardDeviation)
    {
        // Generate two random numbers [0,1]
        float u1 = UnityEngine.Random.Range(0,1.0f); 
        float u2 = UnityEngine.Random.Range(0,1.0f);

        // Generate a random number from normal distrubution
        float z0 =  (float)(Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2));
        double x0 = mean + standardDeviation * z0;

        return x0;
    }
}
