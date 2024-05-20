using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BuildingBlockDisplay : MonoBehaviour
{
    public BuildingBlock block;
    public Vector3Int position;
    public new string name;
    private int sizeX;
    private int sizeY;
    private int sizeZ;
    private Transform model;
    private BoxCollider boxCollider;
    
    public void UpdateDisplay(Vector3Int position, BuildingBlock block)
    {
        this.block = block;
        this.position = position;
        UpdateVariables();
        UpdateCollider();
        UpdatePosition();
    }

    private void UpdateCollider()
    {
        Vector3 dimendions = new Vector3(block.sizeX, block.sizeY, block.sizeZ);
        boxCollider = Finder.FindOrCreateComponent<BoxCollider>(gameObject);
        boxCollider.size = dimendions;
    }

     private void UpdatePosition()
    {
        Vector3 dimendions = new Vector3(block.sizeX, block.sizeY, block.sizeZ);
        gameObject.transform.localPosition += dimendions*0.5f;
    }

    private void UpdateVariables()
    {
        this.sizeX = block.sizeX;
        this.sizeY = block.sizeY;
        this.sizeZ = block.sizeZ;
        this.model = block.model;
        this.name = block.name;
    }
}
