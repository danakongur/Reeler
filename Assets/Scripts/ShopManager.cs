using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

	public string coinString;
	public TMPro.TMP_Text coinText;

	public List<Item> itemsForSale;

	public GameObject itemButtonPrefab;

	GameObject itemMenuBackground;

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
	/// Buy certain item, subtracts coins and adds to player inventory
	/// </summary>
	/// <param name="item">Item bought</param>
	public void Buy(Item item){
		PlayerManager.instance.coins = PlayerManager.instance.coins-item.price;
		UpdateCoins();
		PlayerManager.instance.AddItem(item);
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

	/// <summary>
	/// Debug function to print player's inventory
	/// </summary>
	public void DebugLogInventory(){
		PlayerManager.instance.PrintItems();
	}

	public void UpdateCoins(){
		coinText.text = $"{coinString} {PlayerManager.instance.coins}";
	}

	void Awake() {
		itemsForSale = new List<Item>
		{
			new Item(13, "Test"),
			new Item(25, "bait 1"),
			new Item(25, "bait 2"),
			new Item(25, "bait 3"),
		};
	}
//newGameObject.transform.SetParent(YourParent.transform)
	void Start() {
		UpdateCoins();

		itemMenuBackground = GameObject.Find("BuyItems");

		// Place button for each available item
		foreach (Item item in itemsForSale){
			GameObject itemObj = Instantiate(itemButtonPrefab);
			itemObj.transform.SetParent(itemMenuBackground.transform);

			// Set text to item name
			TMPro.TMP_Text tx = itemObj.transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
			tx.text = item.itemName;


			Button itemBut = itemObj.GetComponent<Button>();
			if (itemBut){ // ensure button component exists
				// Buy item function added to item button
				itemBut.onClick.AddListener(delegate {Buy(item);});
			}
			else {
				Debug.Log($"Finding button component of item {item.itemName} returned null");
			}
		}

		

		// everything here is for testing purposes
		/*foreach(Item item in itemsForSale){ 
			PlayerManager.instance.AddItem(item);
		}
		bool val = PlayerManager.instance.RemoveItem(itemsForSale.First());
		PlayerManager.instance.PrintItems();*/
	}

	public void LoadMain(){
		Loader.Load(Loader.Scene.Main);
	}
}
