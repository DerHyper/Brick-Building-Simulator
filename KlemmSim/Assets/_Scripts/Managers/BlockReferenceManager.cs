using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockReferenceManager : MonoBehaviour
{
    [SerializeField]
    private List<BuildingBlock> buildingBlocks;
    
    // Dictionary for fast lookup by name
    // Note: Names in a map should never be used twice!
    private Dictionary<string,BuildingBlock> buildingBlockMap;

    private void Awake()
    {
        InializeDictionary();
    }

    // Inialize the Dictionary by mapping from the name of the block to the block 
    private void InializeDictionary()
    {
        buildingBlockMap = new Dictionary<string, BuildingBlock>();
        foreach (BuildingBlock buildingBlock in buildingBlocks)
        {
            if(!buildingBlockMap.TryAdd(buildingBlock.name, buildingBlock))
            {
                Debug.LogWarning("Could not initilize BuildingBlockMap", this);
            }
        }
    }

    public BuildingBlock GetBuildingBlockByName(string name)
    {
        // If the key is invalid, get an alternative block
        // Note: The first block is not necessarily the first block added since it is a hash map.
        // If alternative block is also not working, create a new one
        if(!buildingBlockMap.TryGetValue(name, out BuildingBlock block))
        {
            Debug.LogWarning("Could get value from key: '"+name+"'.", this);
            
            BuildingBlock alternativeBlock = buildingBlockMap.First().Value;

            if (alternativeBlock != null) block = alternativeBlock;
            else block = new BuildingBlock();
        }

        return block;
    }

    public List<BuildingBlock> GetBuildingBlocks()
    {
        return buildingBlocks;
    }
}
