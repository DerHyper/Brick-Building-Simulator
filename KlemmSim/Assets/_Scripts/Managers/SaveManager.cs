using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    GridManager gridManager;

    private void Start() 
    {
        gridManager = Finder.FindGridManager();    
    }

    public void Export()
    {
        string saveData = GetSaveData();
    }

    private string GetSaveData()
    {
        // Find BuildingBlocks 
        GameObject buildingblockParent = Finder.FindOrCreateGameObjectWithTag("BuildingBlocks");
        BuildingBlockDisplay[] blocks = buildingblockParent.GetComponentsInChildren<BuildingBlockDisplay>();
        
        // Put the BuildingBlocks in a format that can be serialized into a JSON file
        JsonData jsonData = new JsonData(blocks);
        string jsonText = JsonUtility.ToJson(jsonData);
        Debug.Log("Collected Block Data: "+jsonText);
        
        return jsonText;
    }
}
