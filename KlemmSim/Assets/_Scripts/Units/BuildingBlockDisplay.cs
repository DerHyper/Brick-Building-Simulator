using UnityEngine;

public class BuildingBlockDisplay : MonoBehaviour
{
    public BuildingBlock Block;
    public Vector3Int Position;
    public new string name;
    public Orientation.Alignment Alignment;
    private BoxCollider _boxCollider;
    
    public BuildingBlockDisplay UpdateDisplay(BuildingBlock block, Vector3Int position, Orientation.Alignment alignment)
    {
        this.Block = block;
        this.Position = position;
        this.Alignment = alignment;
        UpdateVariables();
        UpdateCollider();
        UpdatePosition();
        return this;
    }

    public new string ToString()
    {
        string info = "{Name: "+name+", BuildingBlock: "+Block+", Position: "+Position+", Alignment: "+Alignment+"}";
        return info;
    }

    private void UpdateCollider()
    {
        Vector3 dimendions = new Vector3(Block.SizeX, Block.SizeY, Block.SizeZ);
        _boxCollider = Finder.FindOrCreateComponent<BoxCollider>(gameObject);
        _boxCollider.size = dimendions;
        _boxCollider.center = dimendions/2.0f;
    }

    private void UpdatePosition()
    {
        Vector3 offset = Orientation.GetRotationOffset(Alignment, Block);
        gameObject.transform.localPosition += offset;
    }

    private void UpdateVariables()
    {
        this.name = Block.name;
    }
}
