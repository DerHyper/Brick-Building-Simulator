using Codice.Client.Common;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public InputManager InputManager;
    [SerializeField]
    private Material _ghostMaterial;
    private GameObject _blockGhost;

    private void Start()
    {
        _blockGhost = CreateBlockGhost();
    }

    private void Update() {
        UpdateGhostTransform();
    }

    public void SetGhostRotation(Orientation.Alignment alignment)
    {
        Quaternion direction = Orientation.ToQuaternion(alignment);
        _blockGhost.transform.rotation = direction;
    }

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
        GameObject blockGhost = GameObject.Instantiate(new GameObject(), parent);
        return blockGhost;
    }

    private void UpdateGhostTransform()
    {
        if(!CameraToWorld.TryGetMouseBlockPlacementPosition(out Vector3Int gridPosition))
        {
            Debug.LogWarning("Tried to place block outside of grid.");
            return;
        }

        if (InventoryManager.GetSelectedBuildingBlock() != null)
        {
            Orientation.Alignment currentAlignment = InputManager.CurrentAlignment;
            _blockGhost.transform.position = Vector3.Lerp(_blockGhost.transform.position, gridPosition + Orientation.GetRotationOffset(currentAlignment, InventoryManager.GetSelectedBuildingBlock()), 0.12f);
            Quaternion direction = Orientation.ToQuaternion(currentAlignment);
            _blockGhost.transform.rotation = Quaternion.Lerp(_blockGhost.transform.rotation, direction, 0.12f);
        }

    }
}
