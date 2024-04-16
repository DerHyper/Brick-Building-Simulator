using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Finder
{


    public static InputManager FindInputManager()
    {
        InputManager inputManager = GameObject.FindObjectOfType<InputManager>();
        if (inputManager == null)
        {
            GameObject go = Object.Instantiate(new GameObject());
            return go.AddComponent<InputManager>();
        }
        return inputManager;
    }

    public static GridManager FindGridManager()
    {
        GridManager gridManager = GameObject.FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            GameObject go = Object.Instantiate(new GameObject());
            return go.AddComponent<GridManager>();
        }
        return gridManager;
    }
}
