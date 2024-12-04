using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal;


//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

	public string coinString;
	public TMPro.TMP_Text coinText;

	public TMPro.TMP_Text boughtText;

	public float textDelay;

	public List<Item> itemsForSale;

	public GameObject itemButtonPrefab;

	List<GameObject> buttonGameObjects;

	GameObject itemMenuBackground;

	public Sprite sampleSprite;

	/// <summary>
	/// Subtract from user's coins
	/// </summary>
	/// <param name="price">the amount to subtract</param>
    public void Buy(int price) {
		PlayerManager.instance.coins = PlayerManager.instance.coins-price;
		Debug.Log($"Bought for {price}. Current balance {PlayerManager.instance.coins}");
		UpdateCoins();
	}

	private IEnumerator coroutine;

	/// <summary>
	/// Buy certain item if player can afford it, subtracts coins and adds to player inventory. Silently fails if player does not have enough coins.
	/// </summary>
	/// <param name="item">Item bought</param>
	public void Buy(Item item){
		if(PlayerManager.instance.coins-item.price >= 0){
			PlayerManager.instance.coins = PlayerManager.instance.coins-item.price;
			UpdateCoins();
			PlayerManager.instance.AddItem(item);
			boughtText.text = $"Bought {item.itemName} for {item.price} coins";
			Debug.Log($"started coroutine for buying {item.itemName}");
			
			if (!coroutine.IsUnityNull()){
				StopCoroutine(coroutine);
				Debug.Log("stopping another coroutine");
			} 
			coroutine = RemoveTextDelay();
			StartCoroutine(coroutine);
		}
	}

	private IEnumerator RemoveTextDelay(){
		yield return new WaitForSeconds(textDelay);
		Debug.Log($"deleting text {boughtText.text}");
		boughtText.text = "";
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
		
		for (int i = 0; i < itemsForSale.Count(); i++){
			Item item = itemsForSale[i];
			GameObject itemObj = buttonGameObjects[i];
			Button itemBut = itemObj.GetComponent<Button>();

			// TODO: this just changes the color of the image on the button
			if(PlayerManager.instance.coins - item.price < 0){
				// player can't afford item, make it look different
				itemBut.image.color = new Color(1.0f, 0.8f, 0.8f);
			}
			else {
				itemBut.image.color = new Color(1.0f, 1f, 1f);
			}
		}
	}

	void Awake() {
		itemsForSale = new List<Item>
		{
			new Item(13, "Test"),
			new Item(25, "bait 1", sampleSprite),
			new Item(25, "bait 2"),
			new Item(25, "bait 3"),
			new Item(25, "bait 3"),
			new Item(25, "bait 3"),
			new Item(25, "bait 3"),
		};
		buttonGameObjects = new List<GameObject>();
	}

	void Start() {

		itemMenuBackground = GameObject.Find("BuyItems");

		boughtText.text = "";

		Vector2 cellSize;
		GridLayoutGroup grid = itemMenuBackground.GetComponent<GridLayoutGroup>();
		if (grid) {
			cellSize = grid.cellSize;
		}
		else {
			cellSize = new Vector2(50f,50f);
			Debug.Log("Couldn't get cell size");
		}

		// Place button for each available item
		foreach (Item item in itemsForSale){
			GameObject itemObj = Instantiate(itemButtonPrefab);
			itemObj.transform.SetParent(itemMenuBackground.transform);

			// Set text to item name
			TMPro.TMP_Text tx = itemObj.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
			tx.text = $"{item.itemName}\n{item.price}c";
			
			Button itemBut = itemObj.GetComponent<Button>();
			

			// setting the sprite for the object
			Image buttonImage = itemBut.transform.Find("Icon").GetComponent<Image>();
			if (buttonImage) {
				buttonImage.rectTransform.sizeDelta = new Vector2(50f,50f); // TODO: this should not be hardcoded
			}
			if (item.itemImage && buttonImage) {
				buttonImage.sprite = item.itemImage;
			}
			else if (buttonImage) {
				buttonImage.sprite = null;
				buttonImage.color = new Color(1f,1f,1f,0f);
			}

			if(PlayerManager.instance.coins - item.price < 0){
				// player can't afford item, make it look different
				itemBut.image.color = new Color(1.0f, 0.8f, 0.8f);
			}

			if (itemBut){ // ensure button component exists
				// Buy item function added to item button
				itemBut.onClick.AddListener(delegate {
					Buy(item);
				});
			}
			else {
				Debug.Log($"Finding button component of item {item.itemName} returned null");
			}

			buttonGameObjects.Add(itemObj);
		}

		UpdateCoins();


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
