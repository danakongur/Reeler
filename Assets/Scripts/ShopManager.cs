using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

	public string coinString;
	public TMPro.TMP_Text coinText;

	public List<Item> itemsForSale;

	/// <summary>
	/// Subtract from user's coins
	/// </summary>
	/// <param name="price">the amount to subtract</param>
    public void Buy(int price) {
		PlayerManager.instance.coins = PlayerManager.instance.coins-price;
		Debug.Log($"Bought for {price}. Current balance {PlayerManager.instance.coins}");
		UpdateCoins();
	}

	/// <summary>
	/// Adds to user's coins
	/// </summary>
	/// <param name="price">the amount to add</param>
	public void Sell(int price) {
		PlayerManager.instance.coins = PlayerManager.instance.coins+price;
		Debug.Log($"Sold for {price}. Current balance {PlayerManager.instance.coins}");
		UpdateCoins();
	}

	public void UpdateCoins(){
		coinText.text = $"{coinString} {PlayerManager.instance.coins}";
	}

	void Awake() {
		itemsForSale = new List<Item>
		{
			new Item(13, "Test")
		};
	}

	void Start() {
		UpdateCoins();

		// everything here is for testing purposes
		foreach(Item item in itemsForSale){ 
			PlayerManager.instance.AddItem(item);
		}
		bool val = PlayerManager.instance.RemoveItem(itemsForSale.First());
		PlayerManager.instance.PrintItems();
	}

	public void LoadMain(){
		Loader.Load(Loader.Scene.Main);
	}
}
