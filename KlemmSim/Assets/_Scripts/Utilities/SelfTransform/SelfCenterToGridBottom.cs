using UnityEngine;

public class SelfCenterToGridBottom : MonoBehaviour
{
    [SerializeField]
    private int distanceFromBottom;
    public GridManager gridManager;
    void Start()
    {
        UpdatePosition(gridManager, distanceFromBottom);
    }

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
