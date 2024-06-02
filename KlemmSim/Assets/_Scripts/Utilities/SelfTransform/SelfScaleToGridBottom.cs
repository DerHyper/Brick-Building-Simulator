using System.Threading.Tasks;
using UnityEngine;

public class SelfScaleToGridBottom : MonoBehaviour
{
    [SerializeField]
    private float scalingFactor = 0.1f; // Dependent on the dimensions of the object this component is used on (cube:1, plane:0.1, ...)
    [SerializeField]
    private float yScale = 1;
    void Start()
    {
        GridManager gridManager = Finder.FindOrCreateObjectOfType<GridManager>();
        UpdateScale(gridManager, scalingFactor, yScale);
    }

    public void UpdateScale(GridManager gridManager, float scalingFactor, float yScale)
    {
        Vector3Int gridSize = gridManager.GetSize();
        Vector3 scale = new Vector3(gridSize.x, 0, gridSize.z) * scalingFactor + new Vector3(0, yScale, 0);
        gameObject.transform.localScale = scale;
    }
}
