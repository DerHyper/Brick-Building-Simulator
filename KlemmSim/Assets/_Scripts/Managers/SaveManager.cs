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
    private GridManager gridManager;
    private InventoryManager inventoryManager;

    // Filter to only show relevant file extentions
    private readonly ExtensionFilter[] extensions = new ExtensionFilter[] {
        new ExtensionFilter("Constructions", "json"),
        new ExtensionFilter("All Files", "*" ),
    };

    public void Start() 
    {
        // Get relevant managers
        gridManager = Finder.FindGridManager();
        inventoryManager = Finder.FindInventoryManager();
    }

    public void Import()
    {   
        string saveData;

        // Depending on the current platform either upload or load the file
        #if UNITY_WEBGL && !UNITY_EDITOR
            throw new NotImplementedException(); // TODO: Add download for WebGL

        #else
            string saveFileLocation = OpenImportFileBrowser();
            if (saveFileLocation == "") return; // Cancel: No Path was selected

            saveData = ReadJsonToString(saveFileLocation);
        #endif

        gridManager.ClearGrid();
        LoadSaveData(saveData);
    }

    // Deserialize the JSON file in a format readable to C#
    private string ReadJsonToString(string saveFileLocation)
    {
        string saveData = "";

        // Read each line of the file at saveFileLocation into saveData
        StreamReader streamReader = new StreamReader(saveFileLocation);
        while(!streamReader.EndOfStream) saveData += streamReader.ReadLine();
        streamReader.Close( ); 

        Debug.Log("Collected Data from file: "+saveData);
        return saveData;
    }

    private void LoadSaveData(string saveData)
    {
        // Deserialize the saveData
        JsonData jsonData = JsonUtility.FromJson<JsonData>(saveData); 

        // Build all blocks saved in jsonData
        foreach(JsonData.JsonDataBlockInfo blockInfo in jsonData.jsonDataBlockInfos)
        {
            // get block information
            Vector3Int position = blockInfo.position;
            string name = blockInfo.name;
            BuildingBlock blockType = inventoryManager.GetBuildingBlockFromName(name);
            
            gridManager.BuildBlock(position, blockType);
        }
    }

    private string OpenImportFileBrowser()
    {
        // Open file browser for loading the construction, using the std. path and only showing relevant file extentions
        // Only one file can be selected
        string[] saveFileLocation = StandaloneFileBrowser.OpenFilePanel("Import your construction", "", extensions, false);
        
        // Catching errors if no file was selected
        if(saveFileLocation.Length == 0) return "";
        else return saveFileLocation[0];
    }

    public void Export()
    {
        string saveFileLocation = OpenExportFileBrowser();
        if (saveFileLocation == "") return; // Cancel: No Path was selected

        string saveData = SaveDataToJSON();

        // Depending on the current platform either download or write the file
        #if UNITY_WEBGL && !UNITY_EDITOR
            throw new NotImplementedException(); // TODO: Add download for WebGL

        #else
            File.WriteAllText(saveFileLocation, saveData);

        #endif

    }

    private string OpenExportFileBrowser()
    {
        // Open file browser for saving the construction, using the std. path and only showing relevant file extentions
        string saveFileLocation = StandaloneFileBrowser.SaveFilePanel("Export your construction", "", "New Construction", extensions);
        return saveFileLocation;
    }

    private string SaveDataToJSON()
    {
        // Get all Building Blocks
        BuildingBlockDisplay[] blocks = Finder.FindBuildingBlocks();
        
        // Put the BuildingBlocks in a format that can be serialized into a JSON file
        JsonData jsonData = new JsonData(blocks);
        string jsonText = JsonUtility.ToJson(jsonData);
        Debug.Log("Collected Block Data: "+jsonText);
        
        return jsonText;
    }
}
