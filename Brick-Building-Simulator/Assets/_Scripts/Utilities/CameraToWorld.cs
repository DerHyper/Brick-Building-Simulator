using UnityEngine;

/// <summary>
/// Helper class for Camera-Input related purposes.
/// </summary>
public static class CameraToWorld
{
    private const float s_distanceMultiplier = 0.1f;

    /// <summary>
    /// Calculate the position of the block adjacent to the face of the block the mouse is pointing towards
    /// </summary>
    /// <param name="mouseBlockPlacementPosition">Position of the block adjacent to the face of the block the mouse is pointing towards.</param>
    /// <returns>False if the mouse is not pointing towards any Collider.</returns>
    public static bool TryGetMouseBlockPlacementPosition(out Vector3Int mouseBlockPlacementPosition)
    {
        RaycastHit mouseHit = GetMouseRaycastHit();

        // Check Error
        if (mouseHit.collider == null)
        {
            mouseBlockPlacementPosition = new();
            return false;
        }

        Vector3 positionAdjacentToBlock = mouseHit.point + s_distanceMultiplier * mouseHit.normal;
        Vector3Int flooredPosition = Vector3Int.FloorToInt(positionAdjacentToBlock);
        mouseBlockPlacementPosition = flooredPosition;
        return true;
    }

    /// <summary>
    /// Gets the GameObject at the current mouse position, if there is any.
    /// </summary>
    /// <param name="raycastHitGameObject">GameObject at the current mouse position.</param>
    /// <returns>True if there is an Object.</returns>
    public static bool TryGetGameObjectAtMousePosition(out GameObject raycastHitGameObject)
    {
        RaycastHit mouseHit = GetMouseRaycastHit();

        // Check Error
        if (mouseHit.collider == null)
        {
            raycastHitGameObject = new();
            return false;
        }

        raycastHitGameObject = mouseHit.collider.gameObject;
        return true;
    }

    private static RaycastHit GetMouseRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);

        return raycastHit;
    }
}
