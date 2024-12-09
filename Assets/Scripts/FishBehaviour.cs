using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishBehaviour: MonoBehaviour
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


void Awake(){
        infoBoxPanel = GameObject.Find("InfoBoxPanel"); // Find the InfoBoxPanel

}
    void Update()
    {
        UpdateFishImage();
        fishName.SetActive(false); // Hide the fish name initially

        if(infoBoxPanel.activeSelf == true){
            infoBoxPanel.SetActive(false); // Hide the InfoBoxPanel initially
        }

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


        if (infoBoxPanel.activeSelf)
        {
            //infoBoxPanel.SetActive(false);
            fishDescriptionTextComponent.text = ""; // Hide the description
        }
        else
        {
            //infoBoxPanel.SetActive(true);
            fishNameTextComponent.text = fish.name;
            fishDescriptionTextComponent.text = GetComponent<FishDescription>().sellValue.ToString(); // Show the description
        }
    }
}
