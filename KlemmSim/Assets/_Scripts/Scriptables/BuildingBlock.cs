using UnityEngine;

[CreateAssetMenu(fileName = "BuildingBlock", menuName = "ScriptableObjects/BuildingBlock", order = 1)]
public class BuildingBlock : ScriptableObject
{
    public int sizeX;
    public int sizeY;
    public int sizeZ;
    public Transform model;
    public Sprite icon;

    public new string ToString()
    {
        string info = "{Name: "+name+", Size: ("+sizeX+","+sizeX+","+sizeX+")}";
        return info;
    }
}
