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
		// Go through all fish in the game
		foreach(Fish fish in PlayerManager.instance.availableFish) {
			GameObject fishObj = Instantiate(fishPrefab, transform);
			FishBehaviour fishBehaviour = fishObj.GetComponent<FishBehaviour>();

			// set fish sprite and catch status and name
			fishBehaviour.caughtFishImage.sprite = fish.fishImage;
      fishBehaviour.fishNameText = fish.name;
			fishBehaviour.isCaught = PlayerManager.instance.IsCaught(fish);
			Debug.Log($"fish name: {fish.name}, fish catch status: {fishBehaviour.isCaught}");
		}
    }

    // Update is called once per frame
    void Update()
    {
		
    }
}
