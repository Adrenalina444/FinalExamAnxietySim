using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    [Tooltip("The build index of the scene to load. Set this dynamically if needed.")]
    public int sceneBuildIndex; // Scene index to load

    private void Awake()
    {
        // Ensure only one instance of SceneController exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist SceneController across scenes
    }

    // Method to dynamically set the scene index
    public void SetSceneIndex(int index)
    {
        sceneBuildIndex = index;
    }

    // Method to load the specified scene
    public void WhatSceneToLoad()
    {
        if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index: " + sceneBuildIndex + ". Please check your Build Settings.");
        }
    }

    public void QuitSim()
    {
        Application.Quit();
    }
}
