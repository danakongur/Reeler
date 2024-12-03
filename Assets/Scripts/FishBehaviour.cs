using UnityEngine;
using UnityEngine.UI;

public class FishBehaviour : MonoBehaviour
{
    public Image caughtFishImage; // The actual fish image
    public Image questionMarkImage; // The question mark image
    public bool isCaught = false; // Tracks if the fish has been caught

    void Start()
    {
        UpdateFishImage();
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
        }
        else
        {
            caughtFishImage.enabled = false;
            questionMarkImage.enabled = true;
        }
    }
}
