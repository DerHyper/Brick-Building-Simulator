using UnityEngine;
using SFB;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public GridManager GridManager;
    public InventoryManager InventoryManager;
    public BlockReferenceManager BlockReferenceManager;

    private const string StdFilePath = ""; // Note: Standard path depending on OS (Windows: C:\Users\<Username>\Documents)
    // Filter to only show relevant file extentions (.json)
    private readonly ExtensionFilter[] extensions = new ExtensionFilter[] {
        new ExtensionFilter("Constructions", "json"),
        new ExtensionFilter("All Files", "*" ),
    };

    public void Import()
    {   
        string saveData;

        // Depending on the current platform either upload or load the file
        #if UNITY_WEBGL && !UNITY_EDITOR
            throw new NotImplementedException(); // TODO: Add download for WebGL

        #else
            string saveFileLocation = OpenImportFileBrowser();
            if (saveFileLocation == "") return; // Cancel: No path was selected

            saveData = ReadJsonToString(saveFileLocation);
        #endif

        GridManager.ClearGrid();
        InventoryManager.ClearInventory();
        LoadSaveData(saveData);
    }

    public void Export()
    {
        // Open save panle and get save path
        string saveFileLocation = OpenExportFileBrowser();
        if (saveFileLocation == "") return; // Cancel: No Path was selected

        // Serialize every block on the grid into one JSON string
        string saveData = SaveDataToJSON();

        // Depending on the current platform either download or write the file
        #if UNITY_WEBGL && !UNITY_EDITOR
            throw new NotImplementedException(); // TODO: Add download for WebGL

        #else
            File.WriteAllText(saveFileLocation, saveData);

        #endif

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
        foreach(JsonData.RelevantBlockData blockInfo in jsonData.JsonDataBlockInfos)
        {
            // get block information
            Vector3Int position = blockInfo.position;
            string name = blockInfo.name;
            BuildingBlock blockType = BlockReferenceManager.GetBuildingBlockByName(name);
            
            GridManager.TryInstantiateBuildingBlock(position, blockType);
        }
    }

    private string OpenImportFileBrowser()
    {
        string ImportTitle = "Import your construction";
        bool IsMultiselect = false;
        string[] saveFileLocation = StandaloneFileBrowser.OpenFilePanel(ImportTitle, StdFilePath, extensions, IsMultiselect);
        
        // Catch if no file was selected
        if(saveFileLocation.Length == 0) return "";
        else return saveFileLocation[0];
    }

    private string OpenExportFileBrowser()
    {
        string ExportTitle = "Export your construction";
        string DefaultName = "New Construction";
        string saveFileLocation = StandaloneFileBrowser.SaveFilePanel(ExportTitle, StdFilePath, DefaultName, extensions);

        return saveFileLocation;
    }

    private string SaveDataToJSON()
    {
        // Get all Building Blocks
        BuildingBlockDisplay[] blocks = GridManager.GetBlocksInGrid();
        
        // Put the BuildingBlocks in a format that can be serialized into a JSON file
        JsonData jsonData = new JsonData(blocks);
        string jsonText = JsonUtility.ToJson(jsonData);
        Debug.Log("Collected Block Data: "+jsonText);
        
        return jsonText;
    }
}
