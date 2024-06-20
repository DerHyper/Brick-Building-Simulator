using UnityEngine;

public static class CameraToWorld
{
    private const float s_distanceMultiplier = 0.1f;

    // Calculate the position of the block adjacent to the face of the block the mouse is pointing towards
    public static bool TryGetMouseBlockPlacementPosition(out Vector3Int mouseBlockPlacementPosition)
    {
        RaycastHit mouseHit = GetMouseRaycastHit();

        // Check Error
        if (mouseHit.collider == null)
        {
            mouseBlockPlacementPosition = new Vector3Int();
            return false;
        }

        Vector3 positionAdjacentToBlock = mouseHit.point + s_distanceMultiplier * mouseHit.normal;
        Vector3Int flooredPosition = Vector3Int.FloorToInt(positionAdjacentToBlock);
        mouseBlockPlacementPosition = flooredPosition;
        return true;
    }

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);
        Vector3 raycastHitPoint = raycastHit.point;
        return raycastHitPoint;
    }

    public static bool TryGetGameObjectAtMousePosition(out GameObject raycastHitGameObject)
    {
        RaycastHit mouseHit = GetMouseRaycastHit();

        // Check Error
        if (mouseHit.collider == null)
        {
            raycastHitGameObject = new GameObject();
            return false;
        }
        
        raycastHitGameObject = mouseHit.collider.gameObject;
        return true;
    }

    // May return null
    private static RaycastHit GetMouseRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);

        return raycastHit;
    }
}
