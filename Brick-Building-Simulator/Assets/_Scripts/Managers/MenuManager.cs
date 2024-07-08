using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ExitCanvas;
    public GameObject ImportCanvas;
    public GameObject ExportCanvas;
    public GameObject ExitButton;
    public SaveManager SaveManager;
    void Start()
    {
        InitiateExitMenu();
        ImportCanvas.SetActive(false);
        ExportCanvas.SetActive(false);
    }

    /// <summary>
    /// If the ExitCanvas is open, calling this funktion closes it. 
    /// If the ExitCanvas is closed, calling this funktion opens it.
    /// </summary>
    public void SwitchExitCanvasAvailability()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        bool availability = ExitCanvas.activeSelf;
        ExitCanvas.SetActive(!availability);
#endif
    }

    /// <summary>
    /// If the ExitCanvas is open, calling this funktion closes it. 
    /// If the ExitCanvas is closed, calling this funktion opens it.
    /// </summary>
    public void SwitchImportCanvasAvailability()
    {
        bool availability = ImportCanvas.activeSelf;
        ImportCanvas.SetActive(!availability);
    }

    /// <summary>
    /// If the ExitCanvas is open, calling this funktion closes it. 
    /// If the ExitCanvas is closed, calling this funktion opens it.
    /// </summary>
    public void SwitchExportCanvasAvailability()
    {
        bool availability = ExportCanvas.activeSelf;
        ExportCanvas.SetActive(!availability);
    }

    /// <summary>
    /// Exit the Program.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Start import process.
    /// </summary>
    public void Import()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        SaveManager.ImportWithFileBrowser();
#else
        SwitchImportCanvasAvailability();
#endif
    }

    /// <summary>
    /// Start export process.
    /// </summary>
    public void Export()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        SaveManager.ExportWithFileBrowser();
#else
        SwitchExportCanvasAvailability();
        ExportWebGL();
#endif
    }

    /// <summary>
    /// Start import process for WebGL.
    /// </summary>
    public void ImportWebGL()
    {
        string saveData = ImportCanvas.GetComponentInChildren<TMP_InputField>().text;
        SaveManager.ImportWithText(saveData);
    }

    /// <summary>
    /// Start export process for WebGL.
    /// </summary>
    public void ExportWebGL()
    {
        string saveData = SaveManager.SaveDataToJSON();
        ExportCanvas.GetComponentInChildren<TMP_InputField>().text = saveData;
    }

    private void InitiateExitMenu()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ExitButton.SetActive(false); // No exit on WebGL
#endif
        ExitCanvas.SetActive(false);
    }
}
