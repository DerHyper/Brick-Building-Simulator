using UnityEngine;

public static class Finder
{
    public static T FindOrCreateObjectOfType<T>() where T : Component
    {
        T foundObject = GameObject.FindObjectOfType<T>();
        if (foundObject == null)
        {
            GameObject go = GameObject.Instantiate(new GameObject());
            return go.AddComponent<T>();
        }
        return foundObject;
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
