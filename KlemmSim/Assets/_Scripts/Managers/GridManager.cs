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

    public void BuildBlock(Vector3Int position, int value)
    {
        grid.SetGridObject(position, value);
    }
}
