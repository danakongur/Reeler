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


    
    // Start is called before the first frame update
    void Start()
    {
        // Set the background image to the freshwater background
        // Set the freshwater button to be highlighted
        // Add gentle fish animations
        // Prepare a popup or tooltip for fish details when clicked or hovered
        // The fish grid creates all the fish objects and sets their properties
        //Freshwater.onClick.AddListener(OnButtonClick);
       // Saltwater.onClick.AddListener(OnButtonClick);
        //Exotic.onClick.AddListener(OnButtonClick);
    }

    public void FreshwaterF()
    {
        background.sprite = fishtanks[0];
    }

       public void SaltwaterF()
    {
        background.sprite = fishtanks[1];
    }

       public void ExoticF()
    {
        background.sprite = fishtanks[2];
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
