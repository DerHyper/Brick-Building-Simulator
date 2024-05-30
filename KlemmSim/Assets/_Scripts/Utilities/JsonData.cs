using System.Collections.Generic;
using UnityEngine;
using System;

// List of information that needs to be saved for every block
[Serializable]
public class JsonData
{
    public List<RelevantBlockData> jsonDataBlockInfos;

    // Constructor creates a List entry for every BuildingBlockDisplay in the given Array.
    // Each entry contains data, relevant for saving and loading blocks into the scene
    public JsonData(BuildingBlockDisplay[] blocks)
    {
        jsonDataBlockInfos = new List<RelevantBlockData>();
        foreach (BuildingBlockDisplay block in blocks)
        {
            RelevantBlockData jsonDataBlockInfo = new RelevantBlockData(block.name, block.position);
            jsonDataBlockInfos.Add(jsonDataBlockInfo);
        }
    }

    // relevant data for saving and loading blocks into the scene
    [Serializable]
    public class RelevantBlockData
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public Vector3Int position;

        public RelevantBlockData(string name, Vector3Int position)
        {
            this.name = name;
            this.position = position;
        }
    }
}