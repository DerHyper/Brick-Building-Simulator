using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class BlockReferenceManager : MonoBehaviour
{
    private List<BuildingBlock> _buildingBlocks;
    private Dictionary<string, BuildingBlock> _buildingBlockMap; // Dictionary for fast lookup by name

    private void Awake()
    {
        InitializeList();
        InitializeDictionary();
    }

    /// <summary>
    /// Get the BuildingBlock using its name.
    /// If the name is invalid, get an alternative BuildingBlock.
    /// This method should be used for deserialization purposes.  
    /// </summary>
    /// <param name="name">The name of the BuildingBlock.</param>
    /// <returns>The BuildingBlock with the same name.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Could neither get the original nor any alternative BuildingBlock.</exception>
    public BuildingBlock GetBuildingBlockByName(string name)
    {
        // If the key is invalid, get an alternative block
        // Note: The first block is not necessarily the first block added since it is a hash map.
        if (!_buildingBlockMap.TryGetValue(name, out BuildingBlock block))
        {
            Debug.LogWarning($"Could not get value from key: '{name}'.", this);

            BuildingBlock alternativeBlock = _buildingBlockMap.First().Value;

            if (alternativeBlock == null) throw new ArgumentOutOfRangeException();

            block = alternativeBlock;
        }

        return block;
    }

    public List<BuildingBlock> GetBuildingBlocksCopy()
    {
        List<BuildingBlock> listcopy = new(_buildingBlocks);
        return listcopy;
    }

    private void InitializeList()
    {
        PlayerSettings.WebGL.useEmbeddedResources = true; // Add Resources support to WebGL
        BuildingBlock[] buildingBlockArray = Resources.LoadAll<BuildingBlock>("Scriptable Objects/");
        _buildingBlocks = new List<BuildingBlock>(buildingBlockArray);
    }

    private void InitializeDictionary()
    {
        _buildingBlockMap = new();
        foreach (BuildingBlock buildingBlock in _buildingBlocks)
        {
            if (!_buildingBlockMap.TryAdd(buildingBlock.name, buildingBlock))
            {
                Debug.LogWarning("Could not initilize BuildingBlockMap", this);
            }
        }
    }
}
