using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.Linq;
using SFB;
using System.IO;

public class SaveManager : MonoBehaviour
{
    // Filter to only show relevant file extentions
    private readonly ExtensionFilter[] extensions = new ExtensionFilter[] {
        new ExtensionFilter("Constructions", "json"),
        new ExtensionFilter("All Files", "*" ),
    };

    public void Export()
    {
        string saveFileLocation = OpenExportFileBrowser();
        if (saveFileLocation == "") return; // Cancel: No Path was selected

        string saveData = GetSaveData();

        // Depending on the current platform either download or write the file
        #if UNITY_WEBGL && !UNITY_EDITOR
            throw new NotImplementedException(); // TODO: Add download for WebGL

        #else
            File.WriteAllText(saveFileLocation, saveData);

        #endif

    }

    private string OpenExportFileBrowser()
    {
        string saveFileLocation = StandaloneFileBrowser.SaveFilePanel("Export your construction", "", "New Contruction", extensions);
        return saveFileLocation;
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
