using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
     The fish grid creates all the fish objects and sets their properties.
    */
    public Image background; // The background image
    public Button Freshwater; // The freshwater button
    public Button Saltwater; // The saltwater button
    public Button Exotic; // The exotic button

    public Sprite[] fishtanks; // The fishtanks array
    public FishGrid fishGrid; // The fish grid


    
    // Start is called before the first frame update
    void Start()
    {
        // If fishgrid can accept information from the aquarium manager, it will display the fish
        Freshwater.onClick.AddListener(FreshwaterF);
    }

    public void FreshwaterF()
    {
        background.sprite = fishtanks[0];
        fishGrid.showFishType = "Freshwater";
    }

       public void SaltwaterF()
    {
        background.sprite = fishtanks[1];
        fishGrid.showFishType = "Saltwater";
    }

       public void ExoticF()
    {
        background.sprite = fishtanks[2];
        fishGrid.showFishType = "Exotic";
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure AquariumManager script is implemented to handle filtering and updating the tank display
        // Highlight the currently selected habitat button for clarity
        // Add a background specific to the "Freshwater" habitat for better immersion
        // Add gentle fish animations
        // Prepare a popup or tooltip for fish details when clicked or hovered
        // The fish grid creates all the fish objects and sets their properties
    }
}
