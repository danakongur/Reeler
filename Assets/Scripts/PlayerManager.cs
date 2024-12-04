using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerManager has information that persists between scenes
/// </summary>
public class PlayerManager : MonoBehaviour
{
	public int coins = 100;
	public static PlayerManager instance;

	/// <summary>
	/// List of tuples with items and integers for the amount of each item in inventory
	/// </summary>
	private Dictionary<Item,int> items;

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

		caughtFish = new Dictionary<Fish, bool>();

		foreach(Fish fish in availableFish){
			caughtFish[fish] = false;
		}
	}

	public void AddFish(Fish fish){
		caughtFish[fish] = true;
	}

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
