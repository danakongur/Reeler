using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{	
	/// <summary>
	/// list of each item and their respective button object
	/// </summary>
	private List<(Item,GameObject)> inventoryItems = new List<(Item,GameObject)>();

	public GameObject itemButtonPrefab;
	
	public GameObject content;

	/// <summary>
	/// Function to execute when pressing each fish button
	/// </summary>
	public Action<Item> function;

	/// <summary>
	/// Check if item list contains item
	/// </summary>
	/// <param name="li">inventory items list</param>
	/// <param name="item">item to check for</param>
	/// <returns>list contains item</returns>
	private GameObject ContainsItem(List<(Item,GameObject)> li, Item item){
		foreach ((Item i,GameObject g) in li){
			if (item == i) return g;
		}
		return null;
	}

	/// <summary>
	/// Removes item and gameobject in inventoy list
	/// </summary>
	/// <param name="li">list to remove from</param>
	/// <param name="item">item to remove</param>
	private void RemoveItem(List<(Item,GameObject)> li, Item item){
		foreach ((Item,GameObject) tup in li){
			Item i = tup.Item1;
			GameObject g = tup.Item2;
			if (item == i){
				li.Remove(tup);
				Destroy(g);
				return;
			}
		}
	}

	void PlaceholderFunction(Item item){
		if(function != null) {
			function(item);
		}
		else {
			Debug.Log($"Item {item.itemName} clicked on in inventory");
		}
	}

	// This is almost a copy of the shop manager inventory D:
	/// <summary>
	/// Creates/updates the inventory menu items.
	/// </summary>
	public void UpdateInventory(){
		GridLayoutGroup contentGrid = content.GetComponent<GridLayoutGroup>();
		if (!contentGrid) {Debug.Log($"Couldn't find grid layout"); return;};
		Vector2 cellSize = contentGrid.cellSize;

		foreach (KeyValuePair<Item,int> entry in PlayerManager.instance.items) {
			Item item = entry.Key;
			int count = entry.Value;

			// gets gameobject of item if it has been added
			GameObject button = ContainsItem(inventoryItems, item);

			if(button == null && count > 0){ // button has not been added but item is in inventory
				GameObject itemObj = Instantiate(itemButtonPrefab);
				itemObj.transform.SetParent(content.transform);
				inventoryItems.Add((item,itemObj));

				TMPro.TMP_Text tx = itemObj.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
				tx.text = $"{item.itemName}\n{item.price}c\n{count}x";
				tx.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 10f, tx.rectTransform.rect.height);

				Button itemBut = itemObj.GetComponent<Button>();
				
				Image buttonImage = itemBut.transform.Find("Icon").GetComponent<Image>();
				if (buttonImage) {
					buttonImage.rectTransform.sizeDelta = new Vector2(40f,40f); // TODO: this should not be hardcoded
				}
				if (item.itemImage && buttonImage) {
					buttonImage.sprite = item.itemImage;
					buttonImage.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 10f, buttonImage.rectTransform.rect.height);
				}
				else if (buttonImage) {
					buttonImage.sprite = null;
					buttonImage.color = new Color(1f,1f,1f,0f);
				}


				// The function to execute when item is clicked
				// just set to print what item was clicked
				itemBut.onClick.AddListener(delegate {PlaceholderFunction(item);});


			}
			else if (count <= 0){ // item is now gone, remove it
				RemoveItem(inventoryItems,item);
			}
			else { // item is still in in inventory; only update text
				TMPro.TMP_Text tx = button.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
				tx.text = $"{item.itemName}\n{item.price}c\n{count}x";
			}

		}
	}

	void Start() {
		UpdateInventory();
	}
}
