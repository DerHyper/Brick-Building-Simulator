using UnityEngine;
using SFB;
using System.IO;
using System.Text;

public class SaveManager : MonoBehaviour
{
    public GridManager GridManager;
    public InventoryManager InventoryManager;
    public BlockReferenceManager BlockReferenceManager;
    private const string StdFilePath = ""; // Note: Standard path depending on OS (Windows: C:\Users\<Username>\Documents)

    /// <summary>
    /// Filter to only show relevant file extentions (.json)
    /// </summary>
    private readonly ExtensionFilter[] extensions = new ExtensionFilter[] {
        new("Constructions", "json"),
        new("All Files", "*" ),
    };

    /// <summary>
    /// Starts an import process:
    /// Opens a file browser to select a .json file.
    /// Clears the grid and the inventory.
    /// Loads the .json file onto the grid.
    /// </summary>
    public void Import()
    {
        string saveData;

        // Depending on the current platform either upload or load the file
#if UNITY_WEBGL && !UNITY_EDITOR
        return; // TODO: Add download for WebGL
#else
        string saveFileLocation = OpenImportFileBrowser();
        if (saveFileLocation == "") return; // Cancel: No path was selected

        saveData = ReadJsonToString(saveFileLocation);
#endif

        GridManager.ClearGrid();
        InventoryManager.ClearInventory();
        LoadSaveData(saveData);
    }

    /// <summary>
    /// Starts an export process:
    /// Opens a file browser to select a .json file.
    /// Serializes the blocks on the grid into a JSON-String and saves it. 
    /// </summary>
    public void Export()
    {
        // Open save panle and get save path
        string saveFileLocation = OpenExportFileBrowser();
        if (saveFileLocation == "") return; // Cancel: No Path was selected

        // Serialize every block on the grid into one JSON string
        string saveData = SaveDataToJSON();

        // Depending on the current platform either download or write the file
#if UNITY_WEBGL && !UNITY_EDITOR
        return; // TODO: Add download for WebGL

#else
        File.WriteAllText(saveFileLocation, saveData);

#endif

    }

    // Deserialize the JSON file in a format readable to C#
    private string ReadJsonToString(string saveFileLocation)
    {
        StringBuilder saveData = new();

        // Read each line of the file at saveFileLocation into saveData
        StreamReader streamReader = new(saveFileLocation);
        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            saveData.Append(line);
        }
        streamReader.Close();

        return saveData.ToString();
    }

    private void LoadSaveData(string saveData)
    {
        // Deserialize the saveData
        JsonData jsonData = JsonUtility.FromJson<JsonData>(saveData);
        Debug.Log("Collected Data from file: " + jsonData);

        // Build all blocks saved in jsonData
        foreach (JsonData.RelevantBlockData blockInfo in jsonData.JsonDataBlockInfos)
        {
            // get block information
            Vector3Int position = blockInfo.Position;
            string name = blockInfo.Name;
            Orientation.Alignment alignment = blockInfo.Alignment;
            BuildingBlock blockType = BlockReferenceManager.GetBuildingBlockByName(name);

            GridManager.TryInstantiateBuildingBlock(position, blockType, alignment);
        }
    }

    private string OpenImportFileBrowser()
    {
        string ImportTitle = "Import your construction";
        bool IsMultiselect = false;
        string[] saveFileLocation = StandaloneFileBrowser.OpenFilePanel(ImportTitle, StdFilePath, extensions, IsMultiselect);

        // Catch if no file was selected
        string result = (saveFileLocation.Length == 0) ? "" : saveFileLocation[0];
        return result;
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
        JsonData jsonData = new(blocks);
        string jsonText = JsonUtility.ToJson(jsonData);
        Debug.Log("Collected Block Data: " + jsonText);

        return jsonText;
    }
}
