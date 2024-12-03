using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
