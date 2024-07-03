using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRandomizer : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public BlockReferenceManager BlockReferenceManager;
    [SerializeField]
    private int MeanNumberOfBlocks = 40; // Mean amount of blocks per type
    [SerializeField]
    private int StandardDeviation = 5;
    [SerializeField]
    private int MinBlocks = 2;
    [SerializeField]
    private int MaxBlocks = 20;
    [SerializeField]
    private int NumberOfBlockTypes = 5;
    private void Start()
    {
        InitInventory();
    }

    private void InitInventory()
    {
        // Generate block types and distribution
        List<BuildingBlock> pickedBlockTypes = PickRandomBlocks(BlockReferenceManager.GetBuildingBlocks(), NumberOfBlockTypes);
        int[] distribution = GenerateDistributionOfBlocks();
        AddAllBlocksToInventory(pickedBlockTypes, distribution);
    }

    // Add the multiple blocks multiple times
    private void AddAllBlocksToInventory(List<BuildingBlock> pickedBlockTypes, int[] distribution)
    {
        for (int i = 0; i < distribution.Length; i++)
        {
            BuildingBlock block = pickedBlockTypes[i];
            int times = distribution[i];
            AddBlocks(block, times);
        }
    }

    // Add the same block multiple times
    private void AddBlocks(BuildingBlock block, int times)
    {
        for (int i = 0; i < times; i++)
            InventoryManager.Add(block);
    }

    private int[] GenerateDistributionOfBlocks()
    {
        int[] distribution = new int[NumberOfBlockTypes];
        for (int i = 0; i < NumberOfBlockTypes; i++)
            distribution[i] = NormalRange(MeanNumberOfBlocks, StandardDeviation, MinBlocks, MaxBlocks);

        return distribution;
    }

    // Generates a pick from a normal distribution within a range
    private int NormalRange(float mean, float standardDeviation, int minBlocks, int maxBlocks)
    {
        int pick = (int)PickNormalDistribution(mean, standardDeviation);
        int limitedPick = Math.Max(minBlocks, Math.Min(maxBlocks, pick)); // minBlocks <= limitedPick <= maxBlocks

        return limitedPick;
    }

    private List<BuildingBlock> PickRandomBlocks(List<BuildingBlock> possibleBlocks, int numberOfBlocks)
    {
        List<BuildingBlock> pickedBlocks = new();

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

    /// <summary>
    /// Generates a pick from a normal distribution  using the Box–Muller transform for one dimension
    /// Reference: https://de.wikipedia.org/wiki/Box-Muller-Methode
    /// </summary>
    /// <param name="mean">Increasing this value will increase the likelihood, that the returned value is higher.</param>
    /// <param name="standardDeviation">Increasing this value will make the result more random.</param>
    /// <returns>A random number, picked from a normal distribution</returns>
    private float PickNormalDistribution(float mean, float standardDeviation)
    {
        // Generate two random numbers (0,1)
        float u1 = UnityEngine.Random.Range(float.Epsilon, 1.0f - float.Epsilon);
        float u2 = UnityEngine.Random.Range(float.Epsilon, 1.0f - float.Epsilon);

        // Generate a random number from normal distrubution
        float x1 = (float)(Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2));
        float scaledX1 = mean + standardDeviation * x1;

        return scaledX1;
    }
}
