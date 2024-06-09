using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRandomizer : MonoBehaviour
{
    public int totalNumberOfBlocks = 40;
    public int numberOfBlockTypes = 5;
    private InventoryManager inventoryManager;
    private BlockReferenceManager blockReferenceManager;
    private void Start()
    {
        InitReferences();
        InitInventory();
    }

    private void InitInventory()
    {
        // Generate block types and distrobution
        List<BuildingBlock> pickedBlockTypes = PickRandomBlocks(blockReferenceManager.GetBuildingBlocks(), numberOfBlockTypes);
        int[] distribution = GenerateDistrobutionOfBlocks(totalNumberOfBlocks, numberOfBlockTypes);
        AddAllBlocksToInventory(pickedBlockTypes, distribution);
    }

    private void AddAllBlocksToInventory(List<BuildingBlock> pickedBlockTypes, int[] distribution)
    {
        for (int i = 0; i < distribution.Length; i++)
        {
            BuildingBlock block = pickedBlockTypes[i];
            int times = distribution[i];
            AddBlocks(block, times);
        }
    }

    private void AddBlocks(BuildingBlock block, int times)
    {
        for (int i = 0; i < times; i++)
            inventoryManager.Add(block);
    }

    private int[] GenerateDistrobutionOfBlocks(int totalNumberOfBlocks, int numberOfBlockTypes)
    {
        // TODO: Right now, the distribution is an equal distribution and should be exchanged later.
        // Note: The sum is not equal to totalNumberOfBlocks when not evenly devidable by numberOfBlockTypes
        int[] distribution = new int[numberOfBlockTypes];
        for (int i = 0; i < numberOfBlockTypes; i++)
        {
            distribution[i] = totalNumberOfBlocks/numberOfBlockTypes;
        }

        return distribution;
    }

    // Generate a copy of the originalBlocks and pop random elements into a new list
    private List<BuildingBlock> PickRandomBlocks(List<BuildingBlock> originalBlocks, int numberOfBlocks)
    {
        List<BuildingBlock> possibleBlocksCopy = GenerateListCopy(originalBlocks);
        List<BuildingBlock> pickedBlocks = new List<BuildingBlock>();

        for (int i = 0; i < numberOfBlocks; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, possibleBlocksCopy.Count);
            BuildingBlock randomBlockCopy = Instantiate(possibleBlocksCopy[randomIndex]);
            possibleBlocksCopy.Remove(possibleBlocksCopy[randomIndex]);
            pickedBlocks.Add(randomBlockCopy);
        }

        return pickedBlocks;
    }

    private List<BuildingBlock> GenerateListCopy(List<BuildingBlock> original)
    {
        List<BuildingBlock> copy = new List<BuildingBlock>();
        // Copy each element of the original into the Copy
        foreach (BuildingBlock block in original)
        {
            BuildingBlock blockCopy = Instantiate(block);
            copy.Add(blockCopy);
        }

        return copy;
    }

    private void InitReferences()
    {
        inventoryManager = Finder.FindOrCreateObjectOfType<InventoryManager>();
        blockReferenceManager = Finder.FindOrCreateObjectOfType<BlockReferenceManager>();
    }
}
