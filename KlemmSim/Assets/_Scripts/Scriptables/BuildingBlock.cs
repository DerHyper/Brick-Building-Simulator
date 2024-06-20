using UnityEngine;

[CreateAssetMenu(fileName = "BuildingBlock", menuName = "ScriptableObjects/BuildingBlock", order = 1)]
public class BuildingBlock : ScriptableObject
{
    public int SizeX;
    public int SizeY;
    public int SizeZ;
    public Transform Model;
    public Sprite Icon;

    public new string ToString()
    {
        string info = "{Name: "+name+", Size: ("+SizeX+","+SizeY+","+SizeZ+")}";
        return info;
    }
}
