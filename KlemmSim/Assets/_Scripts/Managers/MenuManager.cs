using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ExitCanvas;
    public GameObject ExitButton;
    public SaveManager SaveManager;
    void Start()
    {
        InitiateExitMenu();
    }

    private void InitiateExitMenu()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            ExitButton.SetActive(false); // No exit on WebGL
        #endif
            ExitCanvas.SetActive(false);
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
    /// Exit the Program.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Start import process.
    /// </summary>
    public void InitiateImport()
    {
        SaveManager.Import();
    }

    /// <summary>
    /// Start export process.
    /// </summary>
    public void InitiateExport()
    {
        SaveManager.Export();
    }
}
