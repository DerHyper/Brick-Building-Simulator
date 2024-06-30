using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockReferenceManager : MonoBehaviour
{
    [SerializeField]
    private List<BuildingBlock> _buildingBlocks;
    
    // Dictionary for fast lookup by name
    // Note: Names in a map should never be used twice!
    private Dictionary<string,BuildingBlock> _buildingBlockMap;

    private void Awake()
    {
        InializeDictionary();
    }

    public BuildingBlock GetBuildingBlockByName(string name)
    {
        // If the key is invalid, get an alternative block
        // Note: The first block is not necessarily the first block added since it is a hash map.
        // If alternative block is also not working, create a new one
        if(!_buildingBlockMap.TryGetValue(name, out BuildingBlock block))
        {
            Debug.LogWarning("Could get value from key: '"+name+"'.", this);
            
            BuildingBlock alternativeBlock = _buildingBlockMap.First().Value;

            if (alternativeBlock != null) block = alternativeBlock;
            else block = new BuildingBlock();
        }

        return block;
    }

    public List<BuildingBlock> GetBuildingBlocks()
    {
        return _buildingBlocks;
    }

    // Inialize the Dictionary by mapping from the name of the block to the block 
    private void InializeDictionary()
    {
        _buildingBlockMap = new Dictionary<string, BuildingBlock>();
        foreach (BuildingBlock buildingBlock in _buildingBlocks)
        {
            if(!_buildingBlockMap.TryAdd(buildingBlock.name, buildingBlock))
            {
                Debug.LogWarning("Could not initilize BuildingBlockMap", this);
            }
        }
    }
}
