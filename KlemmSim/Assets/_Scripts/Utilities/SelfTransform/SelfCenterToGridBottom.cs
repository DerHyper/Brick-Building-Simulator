using UnityEngine;

public class SelfCenterToGridBottom : MonoBehaviour
{
    public int distanceFromBottom;
    void Start()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector3Int gridSize = Finder.FindOrCreateObjectOfType<GridManager>().Size;
        Vector3 gridCenter = new Vector3(gridSize.x, 0, gridSize.z) * 0.5f;
        Vector3 levitatedGridCenter = gridCenter + Vector3.up * distanceFromBottom;
        gameObject.transform.position = levitatedGridCenter;
    }
}
