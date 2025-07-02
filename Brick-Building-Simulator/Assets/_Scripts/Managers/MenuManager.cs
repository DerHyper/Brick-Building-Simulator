using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// provides a wrapper for access to and from menus. He takes care of opening and closing panels within the software.
/// </summary>
public class MenuManager : MonoBehaviour
{
    public GameObject ExitCanvas;
    public GameObject ImportCanvas;
    public GameObject ExportCanvas;
    public GameObject OptionsCanvas;
    public GameObject ExitButton;
    public GameObject MainCanvas;
    public SaveManager SaveManager;

    /// <summary>
    /// Changes the reference resolution by its factor.
    /// Default is 1, higher number results in smaller UI
    /// </summary>
    public float StandaloneReferenceResolutionFactor = 1;
    void Start()
    {
        InitiateMenuScale();
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
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_EDITOR
        bool availability = ExitCanvas.activeSelf;
        ExitCanvas.SetActive(!availability);
#endif
    }

    /// <summary>
    /// If the OptionCanvas is open, calling this funktion closes it. 
    /// If the OptionCanvas is closed, calling this funktion opens it.
    /// </summary>
    public void SwitchOptionCanvasAvailability()
    {
        bool availability = OptionsCanvas.activeSelf;
        OptionsCanvas.SetActive(!availability);
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
    /// Note: When using import via text, ImportWithText should be called after pressing the import button.
    /// </summary>
    public void Import()
    {
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_EDITOR
        SaveManager.ImportWithFileBrowser();
#else
        SwitchOptionCanvasAvailability();
        SwitchImportCanvasAvailability();
#endif
    }

    /// <summary>
    /// Start export process.
    /// </summary>
    public void Export()
    {
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_EDITOR
        SaveManager.ExportWithFileBrowser();
#else
        SwitchOptionCanvasAvailability();
        SwitchExportCanvasAvailability();
        ExportWithText();
#endif
    }

    /// <summary>
    /// Start import process for text.
    /// </summary>
    public void ImportWithText()
    {
        string saveData = ImportCanvas.GetComponentInChildren<TMP_InputField>().text;
        SaveManager.ImportWithText(saveData);
    }

    /// <summary>
    /// Start export process for text.
    /// </summary>
    public void ExportWithText()
    {
        string saveData = SaveManager.SaveDataToJSON();
        ExportCanvas.GetComponentInChildren<TMP_InputField>().text = saveData;
    }

    /// <summary>
    /// Copy content of export menu text field to clipboard.
    /// </summary>
    public void CopyExportText()
    {
        // TODO: Implement
    }

    private void InitiateExitMenu()
    {
#if (UNITY_WEBGL || UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
        ExitButton.SetActive(false); // No exit on WebGL, Mobile
#endif
        ExitCanvas.SetActive(false);
    }

    /// <summary>
    /// Change menu scales based on device.
    /// </summary>
    private void InitiateMenuScale()
    {
        HashSet<GameObject> canvases = new() { ExitCanvas, ImportCanvas, ExportCanvas, OptionsCanvas, MainCanvas};

        foreach (var canvas in canvases)
        {
            Vector2 currentResolution = canvas.GetComponent<CanvasScaler>().referenceResolution;

#if !(UNITY_ANDROID || UNITY_IOS) // No Mobile
            canvas.GetComponent<CanvasScaler>().referenceResolution = currentResolution * StandaloneReferenceResolutionFactor;
#endif
        }
    }
}
