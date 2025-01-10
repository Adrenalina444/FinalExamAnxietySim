using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class imageProcessor : MonoBehaviour
{
    public RawImage panelImage; // UI element to display the page
    public RectTransform panelRect; // For resizing the panel if needed

    private Texture2D pdfPage; // Store the single PDF page texture

    // Convert and load the PDF (for single page)
    public void ConvertAndLoadPDF(string pdfPath)
    {
        // Create a temporary folder for storing images
        string tempFolder = Path.Combine(Application.persistentDataPath, "PDFTemp");
        if (!Directory.Exists(tempFolder))
            Directory.CreateDirectory(tempFolder);

        // Convert PDF to image using GhostScript (single page)
        ConvertPDFToImage(pdfPath, tempFolder);

        // Load the converted image (we assume only 1 page)
        LoadImageFromFolder(tempFolder);
    }

    private void ConvertPDFToImage(string pdfPath, string outputFolder)
    {
        string ghostScriptPath = @"C:\Program Files\gs\gs10.04.0\bin\gswin64c.exe"; // Update path if necessary
        string arguments = $"-dNOPAUSE -dBATCH -sDEVICE=png16m -r300 -sOutputFile=\"{outputFolder}/page_001.png\" \"{pdfPath}\"";

        Process process = new Process();
        process.StartInfo.FileName = ghostScriptPath;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        UnityEngine.Debug.Log($"Running GhostScript with arguments: {arguments}");

        process.Start();
        process.WaitForExit();

        // Check if GhostScript executed successfully
        if (process.ExitCode != 0)
        {
            UnityEngine.Debug.LogError($"GhostScript failed with error: {process.StandardError.ReadToEnd()}");
        }
        else
        {
            UnityEngine.Debug.Log("GhostScript completed successfully.");
        }
    }

    private void LoadImageFromFolder(string folderPath)
    {
        UnityEngine.Debug.Log($"Loading image from folder: {folderPath}");

        // Get the single PNG file from the folder (assuming single-page PDF)
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");

        if (imageFiles.Length == 0)
        {
            UnityEngine.Debug.LogError("No image found in the folder.");
            return;
        }

        // Load the first image (only one page is converted)
        string imagePath = imageFiles[0];
        UnityEngine.Debug.Log($"Loading image: {imagePath}");

        byte[] imageData = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(imageData)) // Try to load the image data into the texture
        {
            // Assign the texture to the RawImage component
            panelImage.texture = texture;

            // Preserve aspect ratio and adjust the RectTransform size
            float imageAspect = (float)texture.width / texture.height;
            float panelAspect = panelRect.rect.width / panelRect.rect.height;

            if (imageAspect > panelAspect)
            {
                // Match width, adjust height to maintain aspect ratio
                panelRect.sizeDelta = new Vector2(panelRect.rect.width, panelRect.rect.width / imageAspect);
            }
            else
            {
                // Match height, adjust width to maintain aspect ratio
                panelRect.sizeDelta = new Vector2(panelRect.rect.height * imageAspect, panelRect.rect.height);
            }

            UnityEngine.Debug.Log($"Image loaded and displayed successfully.");
        }
        else
        {
            UnityEngine.Debug.LogError($"Failed to load image: {imagePath}");
        }
    }
}
