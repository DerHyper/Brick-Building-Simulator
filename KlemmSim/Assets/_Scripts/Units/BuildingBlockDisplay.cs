using UnityEngine;

public class BuildingBlockDisplay : MonoBehaviour
{
    public BuildingBlock Block;
    public Vector3Int Position;
    public new string name;
    private BoxCollider _boxCollider;
    
    public void UpdateDisplay(Vector3Int position, BuildingBlock block)
    {
        this.Block = block;
        this.Position = position;
        UpdateVariables();
        UpdateCollider();
        UpdatePosition();
    }

    public new string ToString()
    {
        string info = "{Name: "+name+", BuildingBlock: "+Block+", Position: "+Position+"}";
        return info;
    }

    private void UpdateCollider()
    {
        Vector3 dimendions = new Vector3(Block.SizeX, Block.SizeY, Block.SizeZ);
        _boxCollider = Finder.FindOrCreateComponent<BoxCollider>(gameObject);
        _boxCollider.size = dimendions;
    }

    private void UpdatePosition()
    {
        Vector3 dimendions = new Vector3(Block.SizeX, Block.SizeY, Block.SizeZ);
        gameObject.transform.localPosition += dimendions*0.5f;
    }

    private void UpdateVariables()
    {
        this.name = Block.name;
    }
}
