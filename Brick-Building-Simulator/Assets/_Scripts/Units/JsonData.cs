using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Serializable class containung information that needs to be saved as a .json.
/// </summary>
[Serializable]
public class JsonData
{
    /// <summary>
    /// Creates a entry for every BuildingBlockDisplay in the given Array.
    /// Each entry contains data, relevant for saving and loading blocks into the scene.
    /// </summary>
    public JsonData(BuildingBlockDisplay[] blocks)
    {
        JsonDataBlockInfos = new List<RelevantBlockData>();
        foreach (BuildingBlockDisplay block in blocks)
        {
            RelevantBlockData jsonDataBlockInfo = new RelevantBlockData(block.name, block.Position, block.Alignment);
            JsonDataBlockInfos.Add(jsonDataBlockInfo);
        }
    }

    public List<RelevantBlockData> JsonDataBlockInfos;

    public override string ToString()
    {
        string infos = "";
        foreach (var blockData in JsonDataBlockInfos)
        {
            infos += $"{blockData},\n";
        }

        return $"{{{infos}}}";
    }

    /// <summary>
    /// Relevant data for saving and loading blocks into the scene
    /// </summary>
    [Serializable]
    public class RelevantBlockData
    {
        [SerializeField]
        public string Name;
        [SerializeField]
        public Vector3Int Position;
        [SerializeField]
        public Orientation.Alignment Alignment;

        public RelevantBlockData(string name, Vector3Int position, Orientation.Alignment alignment)
        {
            this.Name = name;
            this.Position = position;
            this.Alignment = alignment;
        }

        public override string ToString()
        {
            string infos = $"{{Name: {Name}, Position: {Position}, Alignment: {Alignment}}}";

            return infos;
        }
    }
}