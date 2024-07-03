using UnityEngine;

/// <summary>
/// Adding this class to a GameObject will center it to the bottom of the grid.
/// </summary>
public class SelfCenterToGridBottom : MonoBehaviour
{
    public GridManager GridManager;
    [SerializeField]
    private int _distanceFromBottom;
    void Start()
    {
        UpdatePosition(GridManager, _distanceFromBottom);
    }

    /// <summary>
    /// Centers the holders GameObject to the bottom of the grid.
    /// </summary>
    public void UpdatePosition(GridManager gridManager, int distanceFromBottom)
    {
        // Get designated position
        Vector3Int gridSize = gridManager.GetSize();
        Vector3 gridCenter = new Vector3(gridSize.x, 0, gridSize.z) * 0.5f;
        Vector3 levitatedGridCenter = gridCenter + Vector3.up * distanceFromBottom;

        // Transform object
        gameObject.transform.position = levitatedGridCenter;
    }
}
