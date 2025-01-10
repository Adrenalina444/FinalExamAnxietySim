using SimpleFileBrowser;
using UnityEngine;

public class openFile : MonoBehaviour
{
    public imageProcessor processor; // Reference to the imageProcessor script

    // Opens the file browser to select a PDF
    public void OpenFileBrowser()
    {
        FileBrowser.ShowLoadDialog(
            onSuccess: (paths) =>
            {
                if (paths.Length > 0)
                {
                    HandleFile(paths[0]); // Pass the selected file path to HandleFile
                }
                else
                {
                    Debug.LogWarning("No file selected.");
                }
            },
            onCancel: () => { Debug.Log("File selection cancelled"); },
            pickMode: FileBrowser.PickMode.Files,
            allowMultiSelection: false,
            initialPath: null,
            initialFilename: null,
            title: "Select a PDF",
            loadButtonText: "Open"
        );
    }

    // Handles the selected file path (called from the file browser)
    public void HandleFile(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            Debug.Log($"Selected file: {filePath}");

            if (processor != null)
            {
                processor.ConvertAndLoadPDF(filePath); // Pass the file to the imageProcessor
            }
            else
            {
                Debug.LogWarning("Processor not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid file path.");
        }
    }

    // Handles the currently selected file (called by the Submit button)
    public void HandleFile()
    {
        if (FileBrowser.Success && FileBrowser.Result.Length > 0)
        {
            string selectedFilePath = FileBrowser.Result[0]; // Get the first selected file
            HandleFile(selectedFilePath); // Call the parameterized HandleFile
        }
        else
        {
            Debug.LogWarning("No file selected or operation cancelled.");
        }
    }
}
