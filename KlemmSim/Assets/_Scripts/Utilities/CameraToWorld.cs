using UnityEngine;

public static class CameraToWorld
{
    private const float s_distanceMultiplier = 0.1f;

    // Calculate the position of the block adjacent to the face of the block the mouse is pointing towards
    public static Vector3Int GetMouseBlockPlacementPosition()
    {
        RaycastHit mouseHit = GetMouseRaycastHit();
        Vector3 positionAdjacentToBlock = mouseHit.point + s_distanceMultiplier * mouseHit.normal;
        Vector3Int flooredPosition = Vector3Int.FloorToInt(positionAdjacentToBlock);
        return flooredPosition;
    }

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);
        Vector3 raycastHitPoint = raycastHit.point;
        return raycastHitPoint;
    }

    public static GameObject GetGameObjectAtMousePosition()
    {
        RaycastHit mouseHit = GetMouseRaycastHit();
        GameObject raycastHitGameObject = mouseHit.collider.gameObject;
        return raycastHitGameObject;
    }

    private static RaycastHit GetMouseRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);
        if (raycastHit.collider == null) throw new OutsideGridException();
        return raycastHit;
    }
}
