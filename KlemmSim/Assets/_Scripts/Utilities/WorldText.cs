using UnityEngine;

static class WorldText
{
    static int debugTextSize = 10;
    static Color debugTextColor = Color.white;
    public static TextMesh CreateDebugText(string text, Vector3 localPosition)
    {
        TextMesh textMesh = CreateWorldText(text, localPosition, debugTextSize, debugTextColor);

        GameObject debugObjects = Finder.FindOrCreateGameObjectWithTag("DebugObjects");
        textMesh.gameObject.transform.SetParent(debugObjects.transform);
        return textMesh;
    }


    // Create text in scene
    // Source: https://youtu.be/waEsGu--9P8?si=GNZfMSbodwKZ8iKo&t=336
    public static TextMesh CreateWorldText(
        string text,
        Vector3 localPosition,
        int fontSize,
        Color color
        ) 
    {
        GameObject gameObject = new GameObject("TextObject"+localPosition);
        gameObject.AddComponent<TextMesh>();
        Transform transform = gameObject.transform;
        transform.Rotate(90,0,0);
        transform.localScale = Vector3.one*0.1f;
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        return textMesh;
    }
}
