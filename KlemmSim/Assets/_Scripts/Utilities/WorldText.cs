using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Source: https://youtu.be/waEsGu--9P8?si=GNZfMSbodwKZ8iKo&t=336
class WorldText
{
    // Create Text in World
    public static TextMesh CreateWorldText(
        string text,
        Vector3 localPosition,
        int fontSize,
        Color color
        ) 
    {
        GameObject gameObject = new GameObject("TextObject");
        gameObject.AddComponent<TextMesh>();
        Transform transform = gameObject.transform;
        transform.Rotate(90,0,0);
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
