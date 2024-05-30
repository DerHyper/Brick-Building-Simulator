using UnityEngine;

public class SelfScaleToGridBottom : MonoBehaviour
{
    public float scalingFactor;
    void Start()
    {
        Vector3Int gridSize = Finder.FindOrCreateObjectOfType<GridManager>().Size;
        Vector3 scale = new Vector3(gridSize.x, 1, gridSize.z)*scalingFactor;
        gameObject.transform.localScale = scale;
    }

}
