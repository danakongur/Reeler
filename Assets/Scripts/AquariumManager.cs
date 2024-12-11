using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AquariumManager : MonoBehaviour
{
    /*
     Ensure AquariumManager script is implemented to handle filtering and updating the tank display.
 Highlight the currently selected habitat button for clarity.
 Add a background specific to the "Freshwater" habitat for better immersion.
 Add gentle fish animations.
 Prepare a popup or tooltip for fish details when clicked or hovered.
    Add a fish count display.
    */
    public TextMeshProUGUI fishCountText;
    public int fishCount = 0;
    public int maxFishCount = 8;
    // Start is called before the first frame update
    void Start()
    {
        // Set the fish count text
        fishCountText.text = "Fish Count: " + fishCount + "/" + maxFishCount;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the fish count text
        fishCountText.text = "Fish Count: " + fishCount + "/" + maxFishCount;
        // Check if the tank is full
        if (fishCount >= maxFishCount)
        {
            // Display a message or handle the full tank scenario
            Debug.Log("Tank is full!");
        } // else, continue adding fish
    }
}
