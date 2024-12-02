using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item with price and name
/// </summary>
public class Item
{

/// <summary>
/// Takes price and item's name
/// </summary>
	public Item(int price, string itemName){
		this.price = price;
		this.itemName = itemName;
	}
    public int price;
	public string itemName;
}
