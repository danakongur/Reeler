using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishHover : MonoBehaviour
{
    private Vector3 originalScale; // To store the fish's original size
    public float hoverScaleFactor = 1.2f; // How much bigger the fish gets
    private bool isHovered = false;

    // Specify the name of the aquarium scene
    private string aquariumSceneName = "Aquarium";

    void Start()
    {
        // Save the original scale of the fish
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        // Check if the current scene is the aquarium scene
        if (SceneManager.GetActiveScene().name == aquariumSceneName)
        {
            // Make the fish slightly bigger when hovered
            transform.localScale = originalScale * hoverScaleFactor;
            isHovered = true;
        }
    }

    void OnMouseExit()
    {
        // Return the fish to its original size
        if (isHovered)
        {
            transform.localScale = originalScale;
            isHovered = false;
        }
    }

    void OnMouseDown()
    {
        // Optional: Add behavior when the fish is clicked
        if (SceneManager.GetActiveScene().name == aquariumSceneName)
        {
            Debug.Log("Fish clicked in Aquarium Scene: " + gameObject.name);
        }
    }
}
