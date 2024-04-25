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

    public static T FindOrCreateComponent<T>(GameObject target) where T : Component
    {
        T foundComponent = target.GetComponent<T>();
        if (foundComponent == null)
        {
            return target.AddComponent<T>();
        }
        return foundComponent;
    }
}
