using System;
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
            GameObject go = GameObject.Instantiate(new GameObject());
            return go.AddComponent<InputManager>();
        }
        return inputManager;
    }

    public static GridManager FindGridManager()
    {
        GridManager gridManager = GameObject.FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            GameObject go = GameObject.Instantiate(new GameObject());
            return go.AddComponent<GridManager>();
        }
        return gridManager;
    }

    public static InventoryManager FindInventoryManager()
    {
        InventoryManager inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            GameObject go = GameObject.Instantiate(new GameObject());
            return go.AddComponent<InventoryManager>();
        }
        return inventoryManager;
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

    public static GameObject FindOrCreateGameObjectWithTag(string tag)
    {
        GameObject foundGameObject = GameObject.FindGameObjectWithTag(tag);
        if (foundGameObject == null)
        {
            GameObject newGameObject = GameObject.Instantiate(new GameObject());
            newGameObject.name = tag.ToString()+"Object";
            newGameObject.tag = tag;
            return newGameObject;
        }
        return foundGameObject;
    }

    public static GameObject FindOrCreateGameObjectWithName(string name)
    {
        GameObject foundGameObject = GameObject.Find(name);
        if (foundGameObject == null)
        {
            GameObject newGameObject = GameObject.Instantiate(new GameObject());
            newGameObject.name = name;
            return newGameObject;
        }
        return foundGameObject;
    }

    // Finds all BuildingBlocks under the BuildingBlocks parent.
    // May return an empty Array
    public static BuildingBlockDisplay[] FindBuildingBlocks()
    {
        GameObject buildingblockParent = Finder.FindOrCreateGameObjectWithTag("BuildingBlocks");
        BuildingBlockDisplay[] blocks = buildingblockParent.GetComponentsInChildren<BuildingBlockDisplay>();

        return blocks;
    }
}
