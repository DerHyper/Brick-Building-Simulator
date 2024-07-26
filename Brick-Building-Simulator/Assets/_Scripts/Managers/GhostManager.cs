using UnityEngine;

/// <summary>
/// Controls the block block ghost, a transparent block indicating the position a block will be placed.
/// </summary>
public class GhostManager : MonoBehaviour
{
    public Orientation.Alignment CurrentAlignment;
    [SerializeField]
    private Material _ghostMaterial;
    [SerializeField, Min(0)]
    private float _repositionSpeed = 0.12f;
    [SerializeField, Min(0)]
    private float _rotationSpeed = 0.12f;
    private GameObject _blockGhost;
    private BuildingBlock _blockGhostBlock;

    private void Start()
    {
        _blockGhost = CreateBlockGhost();
    }

    private void Update()
    {
        UpdateGhostTransform();
    }

    /// <summary>
    /// Replaces the current BlockGhost with a new one.
    /// </summary>
    /// <param name="block">The new BlockGhost BuildingBlock</param>
    public void ReplaceCurrentGhost(BuildingBlock block)
    {
        _blockGhostBlock = block;
        DestroyGhostChildren();
        InstantiateGhostChild(block);
    }

    private void InstantiateGhostChild(BuildingBlock block)
    {
        Transform newChild = Instantiate(block.Model, _blockGhost.transform);
        MeshRenderer[] meshRenderers = newChild.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = _ghostMaterial;
        }
    }

    private void DestroyGhostChildren()
    {
        for (int i = 0; i < _blockGhost.transform.childCount; i++)
        {
            Destroy(_blockGhost.transform.GetChild(i).gameObject);
        }
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

        if (_blockGhostBlock == null)
        {
            return;
        }

        // Transform position over multiple frames
        Vector3 newPosition = gridPosition + Orientation.GetRotationOffset(CurrentAlignment, _blockGhostBlock);
        _blockGhost.transform.position = Vector3.Lerp(_blockGhost.transform.position, newPosition, Time.deltaTime*_repositionSpeed);

        // Transform rotation over multiple frames
        Quaternion newDirection = Orientation.ToQuaternion(CurrentAlignment);
        _blockGhost.transform.rotation = Quaternion.Lerp(_blockGhost.transform.rotation, newDirection, Time.deltaTime*_rotationSpeed);
    }
}
