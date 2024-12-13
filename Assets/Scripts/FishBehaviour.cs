using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishBehaviour : MonoBehaviour
{
    public Image caughtFishImage; // The actual fish image
    public Image questionMarkImage; // The question mark image
    public bool isCaught = false; // Tracks if the fish has been caught
    public GameObject fishName; // The fish name GameObject
    public string fishNameText; // The fish name text
    public TextMeshProUGUI fishNameTextComponent; // The fish name text component
    public TextMeshProUGUI fishDescriptionTextComponent; // The fish description text component
    public Fish fish; // The fish object
    public GameObject infoBoxPanel; // Reference to the InfoBoxPanel


    void Awake()
    {
        //infoBoxPanel = GameObject.Find("InfoBoxPanel"); // Find the InfoBoxPanel
        infoBoxPanel = FindObjectOfType<InfoboxBehavior>().gameObject;
    }

    void Start()
    {
        infoBoxPanel.SetActive(false);

        Button itemBut = gameObject.GetComponent<Button>();
        if (itemBut)
        {
            itemBut.onClick.AddListener(OnFishClicked);
        }

    }
    void Update()
    {
        UpdateFishImage();
        //fishName.SetActive(false); // Hide the fish name initially

        //if(infoBoxPanel.activeSelf == true){
        //infoBoxPanel.SetActive(false); // Hide the InfoBoxPanel initially
        //}

        // Set up the button's onClick listener

    }

    public void CatchFish()
    {
        isCaught = true;
        UpdateFishImage();
    }

    private void UpdateFishImage()
    {
        if (isCaught)
        {
            caughtFishImage.enabled = true;
            questionMarkImage.enabled = false;
            fishNameTextComponent.text = fishNameText;
            caughtFishImage.color = Color.white; // Restore original color for caught fish
        }
        else
        {
            caughtFishImage.enabled = true;
            questionMarkImage.enabled = true;
            caughtFishImage.color = Color.black; // Set color to black for uncaught fish
        }
    }

    private void OnFishClicked()
    {
        if (infoBoxPanel.activeSelf == false && isCaught == true)
        {
           infoBoxPanel.SetActive(true);
           InfoboxBehavior infoBox = infoBoxPanel.GetComponent<InfoboxBehavior>();
           infoBox.fishNameTextComponent.text = "Name: " + fish.name;
           infoBox.fishSellValueTextComponent.text = "Sell Value: " + fish.price.ToString(); // Show the description
           infoBox.fishHabitatTextComponent.text = "Habitat: " + fish.fishDescription.habitat;
           infoBox.fishDietTextComponent.text = "Diet: " + fish.fishDescription.diet;
           infoBox.fishBehaviorTextComponent.text = "Behavior: " + fish.fishDescription.behavior;
           infoBox.fishConservationStatusTextComponent.text = "Conservation Status: " + fish.fishDescription.conservationStatus;
        }
        else
        {
            //infoBoxPanel.SetActive(true);
            //fishNameTextComponent.text = fish.name;
            //fishDescriptionTextComponent.text = GetComponent<FishDescription>().sellValue.ToString(); // Show the description
        }
    }
}
