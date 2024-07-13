using UnityEngine;
using System.IO;
using System;

public class SnapshotCameraTest : MonoBehaviour
{
    public bool autoSearchBlockTypes = true;
    public BuildingBlock[] blocksToSnapshot;
    [HideInInspector]
    public Color backgroundColor = Color.clear;
    [HideInInspector]
    public Vector3 position = new Vector3(0, 0, 1);
    [HideInInspector]
    public Vector3 rotation = new Vector3(345.8529f, 313.8297f, 14.28433f);
    [HideInInspector]
    public Vector3 scale = new Vector3(1, 1, 1);

    private GameObject objectToSnapshot;
    private int currentArrayIndex = 0;
    private SnapshotCamera snapshotCamera;
    private Texture2D texture;

    void Start()
    {
        if (autoSearchBlockTypes)
        {
            blocksToSnapshot = Resources.LoadAll<BuildingBlock>("Scriptable Objects/");
        }
        snapshotCamera = SnapshotCamera.MakeSnapshotCamera("Default");
        UpdateCurrentObjectToSnapshot();
    }

    void OnGUI()
    {
        GUI.TextField(new Rect(10, 5, 275, 21), "Press \"Spacebar\" to save, \"<- or ->\" to change current.");

        if (texture != null)
        {
            GUI.backgroundColor = Color.clear;
            GUI.Box(new Rect(10, 32, texture.width, texture.height), texture);
        }
    }

    public void UpdatePreview()
    {
        if (objectToSnapshot != null)
        {
            // Destroy the texture to prevent a memory leak
            // For a bit of fun you can try removing this and watching the memory profiler while for example continuously changing the rotation to trigger UpdatePreview()
            UnityEngine.Object.Destroy(texture);

            // Take a new snapshot of the objectToSnapshot
            texture = snapshotCamera.TakeObjectSnapshot(objectToSnapshot, backgroundColor, position, Quaternion.Euler(rotation), scale, width: 512, height: 512);
        }
    }

    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") < 0 && currentArrayIndex > 0) // Left pressed, Not the first Object
            {
                currentArrayIndex--;
                UpdateCurrentObjectToSnapshot();
            }
            if (Input.GetAxis("Horizontal") > 0 && currentArrayIndex < blocksToSnapshot.Length - 1) // Right pressed, Not the last Object
            {
                currentArrayIndex++;
                UpdateCurrentObjectToSnapshot();
            }
        }

        // Save a PNG of the snapshot when pressing space
        if (Input.GetKeyUp(KeyCode.Space))
        {
            UpdatePreview();
            string fileName = blocksToSnapshot[currentArrayIndex].name;
            string directoryPath = Path.Combine(Application.dataPath, "../Assets/Resources/Images/Icons");
            FileInfo fi = SnapshotCamera.SavePNG(texture, fileName, directoryPath);

            Debug.Log(string.Format("Snapshot {0} saved to {1}", fileName, directoryPath));
        }
    }

    private void UpdateCurrentObjectToSnapshot()
    {
        // Exchange to new Object
        BuildingBlock newBlock = blocksToSnapshot[currentArrayIndex];
        Destroy(objectToSnapshot);
        objectToSnapshot = Instantiate(newBlock.Model).gameObject;

        // Fix Scale by fitting it to the Object
        float maxSize = Math.Max(Math.Max(newBlock.SizeX, newBlock.SizeY), newBlock.SizeZ);
        scale = new(1f / (maxSize), 1f / maxSize, 1f / maxSize);

        // Fix Position by calculating middlepoint
        float midX;
        if (newBlock.SizeX > newBlock.SizeZ)
        {
            midX = -0.25f;
        }
        else if (newBlock.SizeX < newBlock.SizeZ)
        {
            midX = 0.25f;
        }
        else
        {
            midX = 0;
        }
        
        float midY = -.5f; // Normaly centers to upper corner
        float midZ = 1;

        position = new(midX, midY, midZ);
        UpdatePreview();
    }
}
