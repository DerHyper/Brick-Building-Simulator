using UnityEngine;

public static class CameraToWorld
{
    private static float distanceMultiplier = 0.1f;

    // Calculate the position of the block adjacent to the face of the block the mouse is pointing towards
    public static Vector3Int GetMouseBlockPlacementPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);
        if (raycastHit.collider == null) throw new OutsideGridException();
        Vector3 positionAdjacentToBlock = raycastHit.point + distanceMultiplier*raycastHit.normal;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);
        if (raycastHit.collider == null) throw new OutsideGridException();
        GameObject raycastHitGameObject = raycastHit.collider.gameObject;
        return raycastHitGameObject;
    }
}
