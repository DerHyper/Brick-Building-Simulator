using UnityEngine;

/// <summary>
/// Data container that stores general information about a block type.
/// </summary>
[CreateAssetMenu(fileName = "BuildingBlock", menuName = "ScriptableObjects/BuildingBlock", order = 1)]
public class BuildingBlock : ScriptableObject
{
    public int SizeX;
    public int SizeY;
    public int SizeZ;
    public Transform Model;
    public Sprite Icon;

    public override string ToString()
    {
        string info = $"{{Name: {name}, Size: ({SizeX},{SizeY},{SizeZ})}}";
        return info;
    }
}
