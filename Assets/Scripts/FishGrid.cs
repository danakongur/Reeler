using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGrid : MonoBehaviour
{
    public GameObject fishPrefab; // Reference to the fish prefab
    public Sprite[] fishSprites; // Array of fish sprites
    // Start is called before the first frame update
    void Start()
    {
        // for loop on the fish sprites
        for (int i = 0; i < fishSprites.Length; i++)
        {
            // Instantiate the fish prefab
            GameObject fish = Instantiate(fishPrefab, transform);
            // Get the FishBehaviour component
            FishBehaviour fishBehaviour = fish.GetComponent<FishBehaviour>();
            // Set the fish sprite
            fishBehaviour.caughtFishImage.sprite = fishSprites[i];

            // Set the fish index caught/uncought
            // We want fish.behavior.iscaught to be true if fish is caught
            // and false if fish is not caught
            fishBehaviour.isCaught = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
