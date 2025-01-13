using System.Collections;
using UnityEngine;

public class MoveStartPosition : MonoBehaviour
{
    public Transform player; // Reference to the player transform
    public Transform targetPosition; // Target position to teleport to
    public GameObject objectToDestroy; // The GameObject to destroy
    public float fadeDuration = 1f; // Duration of fade-in and fade-out

    private bool isTeleporting = false;
    private Texture2D fadeTexture; // Texture for the fade effect
    private float fadeAlpha = 0; // Current alpha value of the fade
    private bool isFading = false;

    private void Start()
    {
        // Create a black texture for the fade
        fadeTexture = new Texture2D(1, 1);
        fadeTexture.SetPixel(0, 0, Color.black);
        fadeTexture.Apply();
    }

    private void OnGUI()
    {
        if (!isFading) return;

        // Draw the fade texture
        GUI.color = new Color(0, 0, 0, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }

    public void TeleportPlayer()
    {
        if (!isTeleporting)
        {
            StartCoroutine(TeleportWithFadeCoroutine());
        }
    }

    private IEnumerator TeleportWithFadeCoroutine()
    {
        isTeleporting = true;

        // Fade out
        yield return StartCoroutine(Fade(1));

        // Move the player to the target position
        if (player != null && targetPosition != null)
        {
            player.position = targetPosition.position;
            player.rotation = targetPosition.rotation; //Align rotation with the target
        }

        // Destroy the specified GameObject
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }

        // Fade in
        yield return StartCoroutine(Fade(0));

        isTeleporting = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        isFading = true;

        float startAlpha = fadeAlpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeAlpha = targetAlpha;
        isFading = targetAlpha != 0; // Stop fading if fadeAlpha is 0
    }
}
