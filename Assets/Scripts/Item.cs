using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>
/// Class of item with price and name
/// </summary>
public class Item
{

/// <summary>
/// Takes price and item's name
/// </summary>
	public Item(int price, string itemName){
		this.price = price;
		this.itemName = itemName;
		itemImage = null;
	}

	public Item(int price, string itemName, Sprite sprite){
		this.price = price;
		this.itemName = itemName;
		itemImage = sprite;
	}
    public int price;
	public string itemName;

	public Sprite itemImage;

	/// <summary>
	/// Only for use in code
	/// </summary>
	public GameObject buttonHolder;
}

[Serializable]
/// <summary>
/// Itemized caught fish
/// </summary>
public class FishItem : Item {
	public FishItem(int price, string itemName, Sprite sprite) : base(price, itemName, sprite){
		
	}
}
