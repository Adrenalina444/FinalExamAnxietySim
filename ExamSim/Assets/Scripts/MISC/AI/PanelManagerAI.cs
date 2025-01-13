using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManagerAI : MonoBehaviour
{

    // Reference to the panel prefab
    public GameObject panelPrefab;

    // The parent where the panel should be instantiated, usually the Canvas
    public Transform panelParent;

    // This method instantiates the panel
    public void InstantiatePanel()
    {
        if (panelPrefab != null && panelParent != null)
        {
            // Instantiate the panel at the parent position
            Instantiate(panelPrefab, panelParent);
        }
        else
        {
            Debug.LogError("PanelPrefab or PanelParent is not assigned!");
        }
    }

    public void DestroyPanelAI()
    {
        Destroy(panelPrefab);
    }
}


