using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// List of information that needs to be saved for every block
[Serializable]
public class JsonData
{
    public List<JsonDataBlockInfo> jsonDataBlockInfos;

    // Constructor creates a List entry for every BuildingBlockDisplay in the given Array.
    // Each entry contains data, relevant for saving and loading blocks into the scene
    public JsonData(BuildingBlockDisplay[] blocks)
    {
        jsonDataBlockInfos = new List<JsonDataBlockInfo>();
        foreach (BuildingBlockDisplay block in blocks)
        {
            JsonDataBlockInfo jsonDataBlockInfo = new JsonDataBlockInfo(block.name, block.position);
            jsonDataBlockInfos.Add(jsonDataBlockInfo);
        }
    }

    // relevant data for saving and loading blocks into the scene
    [Serializable]
    public class JsonDataBlockInfo
    {
        [SerializeField]
        string name;
        [SerializeField]
        Vector3Int position;

        public JsonDataBlockInfo(string name, Vector3Int position)
        {
            this.name = name;
            this.position = position;
        }
    }
}