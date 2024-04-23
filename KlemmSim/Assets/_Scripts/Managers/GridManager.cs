using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid3D grid;
    private void Start()
    {
        grid = new Grid3D(2,2,2);
    }

    public void BuildBlock(Vector3Int position, GameObject value)
    {
        if (grid.isNotInBuildingLimit(position)) 
        {
            Debug.Log(position.ToString() + " is not a valid Position to build.");
            return;
        }
        
        GameObject block = Instantiate(value,position,Quaternion.identity);
        grid.SetGridObject(position, block);
    }
}
