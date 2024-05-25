using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<BuildingBlock> buildingBlocks;
    
    // Dictionary for fast lookup by name (Names in a map should never be used twice!) 
    private Dictionary<string,BuildingBlock> buildingBlockMap;

    private void Start()
    {
        InializeDictionary();
    }

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

    public BuildingBlock GetBuildingBlockFromName(string name)
    {
        // If Key is invalid, get an alternative block (fist is not realy first, scince it is a HashMap)
        // If alternative block is also not working create a new one
        if(!buildingBlockMap.TryGetValue(name, out BuildingBlock block))
        {
            Debug.LogWarning("Could get value from key: '"+name+"'.", this);
            
            BuildingBlock alternativeBlock = buildingBlockMap.First().Value;

            if (alternativeBlock != null) block = alternativeBlock;
            else block = new BuildingBlock();
        }

        return block;
    }
}
