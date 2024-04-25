using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BuildingBlockDisplay : MonoBehaviour
{
    public BuildingBlock block;
    public int sizeX;
    public int sizeY;
    public int sizeZ;
    public Transform model;
    private BoxCollider boxCollider;
    
    void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        this.sizeX = block.sizeX;
        this.sizeY = block.sizeY;
        this.sizeZ = block.sizeZ;
        this.model = block.model;
        Vector3 dimendions = new Vector3(block.sizeX, block.sizeY, block.sizeZ);
        boxCollider = Finder.FindOrCreateComponent<BoxCollider>(gameObject);
        boxCollider.size = dimendions;
        boxCollider.center = dimendions*0.5f;
    }

}
