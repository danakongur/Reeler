using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
	Item,
	Bait,
	Fish,
    Boost,
    Heal
}

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
		this.type = ItemType.Item;
	}

	public Item(int price, string itemName, Sprite sprite){
		this.price = price;
		this.itemName = itemName;
		itemImage = sprite;
		this.type = ItemType.Item;
	}
    public int price;
	public string itemName;
	/// <summary>
	/// Item's description
	/// </summary>
	public string description;

	public Sprite itemImage;

	protected ItemType type;

	/// <summary>
	/// Gets the item type of this item
	/// </summary>
	/// <returns>ItemType enum instance</returns>
	public ItemType	GetItemType(){
		return this.type;
	}

	/// <summary>
	/// Only for use in code, very bad solution to a problem
	/// </summary>
	public GameObject buttonHolder;
}

[Serializable]
/// <summary>
/// Itemized caught fish
/// </summary>
public class FishItem : Item {
	public FishItem(int price, string itemName, Sprite sprite) : base(price, itemName, sprite){
		this.type = ItemType.Fish;
	}
}

[Serializable]

public class BaitItem : Item
{
    public BaitItem(int price, string itemName, Sprite sprite) : base(price, itemName, sprite)
    {
        this.type = ItemType.Bait;


    }
}
[Serializable]

public class HealItem : Item
{
    public HealItem(int price, string itemName, Sprite sprite) : base(price, itemName, sprite)
    {
        this.type = ItemType.Heal;


    }
}
[Serializable]

public class BoostItem : Item
{
    public BoostItem(int price, string itemName, Sprite sprite) : base(price, itemName, sprite)
    {
        this.type = ItemType.Boost;


    }
}

