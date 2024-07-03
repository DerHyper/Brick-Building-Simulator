using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public InputManager InputManager;
    [SerializeField]
    private Material _ghostMaterial;
    [SerializeField]
    private float _repositionSpeed = 0.12f;
    [SerializeField]
    private float _rotationSpeed = 0.12f;
    private GameObject _blockGhost;

    private void Start()
    {
        _blockGhost = CreateBlockGhost();
    }

    private void Update()
    {
        UpdateGhostTransform();
    }

    /// <summary>
    /// Replaces the current BlockGhost model with a new one.
    /// </summary>
    /// <param name="newModel">The new BlockGhost model</param>
    public void ReplaceCurrentGhost(Transform newModel)
    {
        for (int i = 0; i < _blockGhost.transform.childCount; i++)
        {
            Destroy(_blockGhost.transform.GetChild(i).gameObject);
        }

        Transform newChild = Instantiate(newModel, _blockGhost.transform);
        newChild.GetComponentInChildren<MeshRenderer>().material = _ghostMaterial;
    }
    private GameObject CreateBlockGhost()
    {
        Transform parent = Finder.FindOrCreateGameObjectWithName("Environment").transform;
        GameObject blockGhost = Instantiate(new GameObject() { name = "BlockGhost" }, parent);
        return blockGhost;
    }

    private void UpdateGhostTransform()
    {
        if (!CameraToWorld.TryGetMouseBlockPlacementPosition(out Vector3Int gridPosition))
        {
            return;
        }

        if (InventoryManager.GetSelectedBuildingBlock() != null)
        {
            // Transform position over multiple frames
            Orientation.Alignment currentAlignment = InputManager.CurrentAlignment;
            Vector3 newPosition = gridPosition + Orientation.GetRotationOffset(currentAlignment, InventoryManager.GetSelectedBuildingBlock());
            _blockGhost.transform.position = Vector3.Lerp(_blockGhost.transform.position, newPosition, _repositionSpeed);

            // Transform rotation over multiple frames
            Quaternion newDirection = Orientation.ToQuaternion(currentAlignment);
            _blockGhost.transform.rotation = Quaternion.Lerp(_blockGhost.transform.rotation, newDirection, _rotationSpeed);
        }

    }
}
