using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// PlayerManager has information that persists between scenes
/// </summary>
public class PlayerManager : MonoBehaviour
{	
	public int startcoins;
	public int coins;
	public static PlayerManager instance;

	/// <summary>
	/// List of tuples with items and integers for the amount of each item in inventory
	/// </summary>
	public Dictionary<Item,int> items;

	public Fish[] availableFish;

	private Dictionary<Fish,bool> caughtFish;
    
	void Awake() {
		// when going back to main screen, it tries to create a new player manager
		if (instance != null && instance != this){
			Destroy(gameObject); // Destroy new instance
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
		items = new Dictionary<Item, int>();

		coins = startcoins;

		caughtFish = new Dictionary<Fish, bool>();

		foreach(Fish fish in availableFish){
			fish.fishItem = new FishItem(fish.price, fish.name, fish.fishImage);
			caughtFish[fish] = false;
		}
	}

	/// <summary>
	/// Add fish to inventory and catch list
	/// </summary>
	/// <param name="fish">Fish from availableFish to catch</param>
	public void CatchFish(Fish fish) {
		AddFish(fish);
		AddItem(fish.fishItem);
	}

	/// <summary>
	/// Adds fish to catch list
	/// </summary>
	/// <param name="fish">Fish from availableFish to set as caught</param>
	public void AddFish(Fish fish){
		caughtFish[fish] = true;
	}

	/// <summary>
	/// Check if fish has been caught
	/// </summary>
	/// <param name="fish">Fish from availableFish to check</param>
	/// <returns></returns>
	public bool IsCaught(Fish fish) {
		return caughtFish[fish];
	}

	/// <summary>
	/// Adds item to player inventory
	/// </summary>
	/// <param name="item">item to add</param>
	public void AddItem(Item item){
		int old;
		bool worked = items.TryGetValue(item, out old);
		items[item] = old+1;
	}

	public Fish GetFishByName(string name){
		foreach(Fish fish in availableFish){
			if (fish.name == name){
				return fish;
			}
		}
		return null;
	}

	/// <summary>
	/// Removes item from player's inventory
	/// </summary>
	/// <param name="item">Item to remove</param>
	/// <returns>Success/failure</returns>
	public bool RemoveItem(Item item){
		int old;
		items.TryGetValue(item, out old);
		if (old > 0){ 
			// there is at least one of this item in inventory
			items[item] = old-1;
			return true;
		}
		else if (old < 0){ // value should never go below 0
			items[item] = 0;
		}
		// 0 means either not in dictionary or the count is 0, could not remove
		return false;
	}

	public void PrintItems(){
		Debug.Log("Player inventory start");
		foreach(KeyValuePair<Item,int> entry in items)
		{
			Debug.Log($"Item: {entry.Key.itemName}, count: {entry.Value}");
		} 
		Debug.Log("Player inventory end");
	}

	public void ResetPlayerManager() {
		Debug.Log($"restart player manager");
		items = new Dictionary<Item,int>();
		coins = startcoins;
		foreach (Fish key in caughtFish.Keys.ToArray())
		{
			caughtFish[key] = false;
		}
	}


	// TODO: may be unnecessary, remove
	public void LoadMain(){
		Loader.Load(Loader.Scene.Main);
	}

	public void LoadBattle(){
		Loader.Load(Loader.Scene.Battle);
	}

	public void LoadAquarium(){
		Loader.Load(Loader.Scene.Aquarium);
	}
	public void LoadShop(){
		Loader.Load(Loader.Scene.Shop);
	}
	
}
