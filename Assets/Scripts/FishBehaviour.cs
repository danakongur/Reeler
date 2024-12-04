using UnityEngine;
using UnityEngine.UI;

public class FishBehaviour : MonoBehaviour
{
    public Image caughtFishImage; // The actual fish image
    public Image questionMarkImage; // The question mark image
    public bool isCaught = false; // Tracks if the fish has been caught

    void Start()
    {
        //UpdateFishImage();
    }

    public void CatchFish()
    {
        isCaught = true;
        //UpdateFishImage();
    }

    private void Update()
    {
        if (isCaught)
        {
            caughtFishImage.enabled = true;
            questionMarkImage.enabled = false;
            caughtFishImage.color = Color.white; // Restore original color for caught fish
        }
        else
        {
            caughtFishImage.enabled = true;
            questionMarkImage.enabled = true;
            caughtFishImage.color = Color.black; // Set color to black for uncaught fish
        }
    }
}
