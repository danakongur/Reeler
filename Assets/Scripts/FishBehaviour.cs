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

    void Start()
    {
        UpdateFishImage();
        fishName.SetActive(false); // Hide the fish name initially

        // Set up the button's onClick listener
        Button itemBut = gameObject.GetComponent<Button>();
        if (itemBut)
        {
            itemBut.onClick.AddListener(OnFishClicked);
        }
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
        if (fishName.activeSelf)
        {
            fishName.SetActive(false);
            fishDescriptionTextComponent.text = ""; // Hide the description
        }
        else
        {
            fishName.SetActive(true);
            //fishDescriptionTextComponent.text = fish.fishDescriptionTextComponent.description; // Show the description
        }
        // Handle the fish click event
        Debug.Log("Fish clicked!");
        // For example, show the fish name or load a new scene
    }

    public void Update()
    {
        // Update fish OnClick event
        // Use UpdateFishImage() to update the fish image
        UpdateFishImage();
    }
}
