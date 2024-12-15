using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need to update grid to follow type of fish in each fishtank
public class FishGrid : MonoBehaviour
{
  public GameObject fishPrefab; // Reference to the fish prefab
  public string showFishType; // Reference to the fish type

  void Start()
  {
    // Go through all fish in the game categorized by habitat
    // Todo: Need to update to reference the fish type in each fishtank
    foreach (Fish fish in PlayerManager.instance.availableFish)
    {

      GameObject fishObj = Instantiate(fishPrefab, transform);
      FishBehaviour fishBehaviour = fishObj.GetComponent<FishBehaviour>();

      // set fish sprite and catch status and name
      fishBehaviour.caughtFishImage.sprite = fish.fishImage;
      fishBehaviour.fishNameText = fish.name;
      fishBehaviour.isCaught = PlayerManager.instance.IsCaught(fish);
	  if (!fishBehaviour.isCaught)
	  	{
			FishHover a = fishObj.GetComponent<FishHover>();
			if(a)
				a.hoverScaleFactor = 1;
		}
      fishBehaviour.fish = fish;
      fishBehaviour.tag = fish.type;
      Debug.Log($"fish name: {fish.name}, fish catch status: {fishBehaviour.isCaught}");

      if (fish.type == "Saltwater" || fish.type == "Exotic")
      {
        fishObj.SetActive(false);
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (showFishType == "Freshwater")
    {
      for (int i = transform.childCount - 1; i >= 0; i--)
      {
        //Destroy(transform.GetChild(i).gameObject);
        transform.GetChild(i).gameObject.SetActive(true);
      }

      foreach (Fish fish in PlayerManager.instance.availableFish)
      {
        if (fish.type == "Saltwater" || fish.type == "Exotic")
        {
          for (int i = transform.childCount - 1; i >= 0; i--)
          {
            if (transform.GetChild(i).tag == "Saltwater" || transform.GetChild(i).tag == "Exotic")
            {
              transform.GetChild(i).gameObject.SetActive(false);
            }
          }
        }
      }
    }

    // foreach (Fish fish in PlayerManager.instance.availableFish)
    // {
    //   if (fish.type == "Freshwater")
    //   {
    //     GameObject fishObj = Instantiate(fishPrefab, transform);
    //     FishBehaviour fishBehaviour = fishObj.GetComponent<FishBehaviour>();

    //     // set fish sprite and catch status and name
    //     fishBehaviour.caughtFishImage.sprite = fish.fishImage;
    //     fishBehaviour.fishNameText = fish.name;
    //     fishBehaviour.isCaught = PlayerManager.instance.IsCaught(fish);
    //     fishBehaviour.fish = fish;
    //     showFishType = "";
    //     Debug.Log($"fish name: {fish.name}, fish catch status: {fishBehaviour.isCaught}");
    //   }
    //}
    if (showFishType == "Saltwater")
    {
      for (int i = transform.childCount - 1; i >= 0; i--)
      {
        //Destroy(transform.GetChild(i).gameObject);
        transform.GetChild(i).gameObject.SetActive(true);
      }

      foreach (Fish fish in PlayerManager.instance.availableFish)
      {
        if (fish.type == "Freshwater" || fish.type == "Exotic")
        {
          for (int i = transform.childCount - 1; i >= 0; i--)
          {
            if (transform.GetChild(i).tag == "Freshwater" || transform.GetChild(i).tag == "Exotic")
            {
              transform.GetChild(i).gameObject.SetActive(false);
            }
          }
        }
      }
    }
    if (showFishType == "Exotic")
    {
      for (int i = transform.childCount - 1; i >= 0; i--)
      {
        //Destroy(transform.GetChild(i).gameObject);
        transform.GetChild(i).gameObject.SetActive(true);
      }

      foreach (Fish fish in PlayerManager.instance.availableFish)
      {
        if (fish.type == "Saltwater" || fish.type == "Freshwater")
        {
          for (int i = transform.childCount - 1; i >= 0; i--)
          {
            if (transform.GetChild(i).tag == "Freshwater" || transform.GetChild(i).tag == "Saltwater")
            {
              transform.GetChild(i).gameObject.SetActive(false);
            }
          }
        }
      }
    }
  }
}
