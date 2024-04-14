using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid3D grid;
    private void Start() {
        grid = new Grid3D(2,3,4);
    }
}
