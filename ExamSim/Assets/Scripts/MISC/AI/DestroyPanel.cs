using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPanel : MonoBehaviour
{
 
    // This function will be called when the button is clicked
    public void DestroyInstantiatedPanel()
    {
        // Destroy the parent game object (the instantiated panel)
        Destroy(transform.parent.gameObject);
    }
}



