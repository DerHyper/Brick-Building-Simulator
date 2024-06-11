using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryRandomizer : MonoBehaviour
{
    public int totalNumberOfBlocks = 40;
    public int numberOfBlockTypes = 5;
    public InventoryManager inventoryManager;
    public BlockReferenceManager blockReferenceManager;
    private void Start()
    {
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
}
