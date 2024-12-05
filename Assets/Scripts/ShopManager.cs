using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
//using UnityEditorInternal;


//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

	public string coinString;
	public TMPro.TMP_Text coinText;

	public TMPro.TMP_Text boughtText;

	public float textDelay;

	public GameObject itemButtonPrefab;

	List<GameObject> buttonGameObjects;

	GameObject itemMenuBackground;

	public Sprite sampleSprite;

	public Sprite leechSprite;
	public Sprite mealwormSprite;

	private List<Item> inventoryItems;
	List<GameObject> inventorybuttonObjects;

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
			
			if (!coroutine.IsUnityNull()){
				StopCoroutine(coroutine);
			} 
			coroutine = RemoveTextDelay();
			StartCoroutine(coroutine);
		}
		UpdateInventory();
	}

	public void Sell(Item item){
		PlayerManager.instance.RemoveItem(item);
		PlayerManager.instance.coins = PlayerManager.instance.coins+item.price;
		boughtText.text = $"Sold {item.itemName} for {item.price} coins";
		if (!coroutine.IsUnityNull()){
				StopCoroutine(coroutine);
			} 
			coroutine = RemoveTextDelay();
			StartCoroutine(coroutine);
		UpdateCoins();
		UpdateInventory();
	}

	private IEnumerator RemoveTextDelay(){
		yield return new WaitForSeconds(textDelay);
		boughtText.text = "";
	}

	/// <summary>
	/// Adds to user's coins
	/// </summary>
	/// <param name="price">the amount to add</param>
	public void Sell(int price) {
		PlayerManager.instance.coins = PlayerManager.instance.coins+price;
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
		
		for (int i = 0; i < PlayerManager.instance.itemsForSale.Count(); i++){
			Item item = PlayerManager.instance.itemsForSale[i];
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

	/// <summary>
	/// Update the inventory view with new information
	/// </summary>
	public void UpdateInventory(){
		GameObject inventoryContent = GameObject.Find("InventoryContent");
		GridLayoutGroup inventoryGrid = inventoryContent.GetComponent<GridLayoutGroup>();

		Vector2 invGridSize;
		if (inventoryGrid) {
			invGridSize = inventoryGrid.cellSize;
		}
		else {
			invGridSize = new Vector2(50f,50f);
			Debug.Log("Couldn't get inventory cell size");
		}

		// scale with screen size
		CanvasScaler canvasScaler = inventoryContent.GetComponentInParent<CanvasScaler>();
		if (canvasScaler){
			float scaleFactor = canvasScaler.transform.lossyScale.x;
			//invGridSize*=scaleFactor;
			Debug.Log($"scale factor: {scaleFactor}");
		}
		else {
			Debug.Log("could not find sell canvas scaler");
		}

		// go through inventory and add
		foreach (KeyValuePair<Item,int> entry in PlayerManager.instance.items){
			Item item = entry.Key;
			int count = entry.Value;

			if(!inventoryItems.Contains(item) && count > 0){// worlds least efficient code
				GameObject itemObj = Instantiate(itemButtonPrefab);
				itemObj.transform.SetParent(inventoryContent.transform);
				itemObj.GetComponent<RectTransform>().localScale = Vector3.one;
				item.buttonHolder = itemObj;
				inventoryItems.Add(item);

				TMPro.TMP_Text tx = itemObj.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
				tx.text = $"{item.itemName}\n{item.price}c\n{count}x";
				tx.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 10f, tx.rectTransform.rect.height);

				Button itemBut = itemObj.GetComponent<Button>();
				

				// setting the sprite for the object
				Image buttonImage = itemBut.transform.Find("Icon").GetComponent<Image>();
				if (buttonImage) {
					buttonImage.rectTransform.sizeDelta = new Vector2(0.4f*invGridSize.x,0.4f*invGridSize.x);
				}
				if (item.itemImage && buttonImage) {
					buttonImage.sprite = item.itemImage;
					buttonImage.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 10f, buttonImage.rectTransform.rect.height);
				}
				else if (buttonImage) {
					buttonImage.sprite = null;
					buttonImage.color = new Color(1f,1f,1f,0f);
				}	

				if (itemBut){ // ensure button component exists
					// Sell item function added to item button
					itemBut.onClick.AddListener(delegate {
						Sell(item);
					});
				}
				else {
					Debug.Log($"Finding button component of item {item.itemName} returned null");
				}
			}
			else if(item.buttonHolder){
				GameObject itemObj = item.buttonHolder;
				if (count > 0){
					TMPro.TMP_Text tx = itemObj.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
					tx.text = $"{item.itemName}\n{item.price}c\n{count}x";
				}
				else {
					Destroy(itemObj);
					item.buttonHolder = null;
					inventoryItems.Remove(item);
				}
			}

		}
		

	}

	void Awake() {
		buttonGameObjects = new List<GameObject>();
		inventoryItems = new List<Item>();
		inventorybuttonObjects = new List<GameObject>();
	}

	void Start() {
		// START DEBUG STATEMENTS
		/*foreach (Fish fish in PlayerManager.instance.availableFish){
			PlayerManager.instance.CatchFish(fish);
		}*/
		// END DEBUG STATEMENTS

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

		// scale with screen size
		CanvasScaler canvasScaler = itemMenuBackground.GetComponentInParent<CanvasScaler>();
		if (canvasScaler){
			float scaleFactor = canvasScaler.transform.lossyScale.x;
			//cellSize*=scaleFactor;
		}
		else {
			Debug.Log("could not find canvas scaler");
		}

		// Place button for each available item
		foreach (Item item in PlayerManager.instance.itemsForSale){
			GameObject itemObj = Instantiate(itemButtonPrefab);
			itemObj.transform.SetParent(itemMenuBackground.transform);
			itemObj.GetComponent<RectTransform>().localScale = Vector3.one;

			// Set text to item name
			TMPro.TMP_Text tx = itemObj.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
			tx.text = $"{item.itemName}\n{item.price}c";
			
			Button itemBut = itemObj.GetComponent<Button>();
			

			// setting the sprite for the object
			Image buttonImage = itemBut.transform.Find("Icon").GetComponent<Image>();
			if (buttonImage) {
				buttonImage.rectTransform.sizeDelta = new Vector2(0.5f*cellSize.x, 0.5f*cellSize.x);
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

		/*inventoryItems = new List<Item>();
		inventorybuttonObjects = new List<GameObject>();
		GameObject inventoryContent = GameObject.Find("InventoryContent");
		GridLayoutGroup inventoryGrid = inventoryContent.GetComponent<GridLayoutGroup>();

		Vector2 invGridSize;
		if (inventoryGrid) {
			invGridSize = inventoryGrid.cellSize;
		}
		else {
			invGridSize = new Vector2(50f,50f);
			Debug.Log("Couldn't get inventory cell size");
		}

		// go through inventory and add
		foreach (KeyValuePair<Item,int> entry in PlayerManager.instance.items){
			Item item = entry.Key;
			int count = entry.Value;
			inventoryItems.Add(item);
			GameObject itemObj = Instantiate(itemButtonPrefab);
			itemObj.transform.SetParent(inventoryContent.transform);

			TMPro.TMP_Text tx = itemObj.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
			tx.text = $"{item.itemName}\n{item.price}c\n{count}x";

			Button itemBut = itemObj.GetComponent<Button>();
			

			// setting the sprite for the object
			Image buttonImage = itemBut.transform.Find("Icon").GetComponent<Image>();
			if (buttonImage) {
				buttonImage.rectTransform.sizeDelta = new Vector2(40f,40f); // TODO: this should not be hardcoded
			}
			if (item.itemImage && buttonImage) {
				buttonImage.sprite = item.itemImage;
			}
			else if (buttonImage) {
				buttonImage.sprite = null;
				buttonImage.color = new Color(1f,1f,1f,0f);
			}	
		}*/
		UpdateInventory();

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
