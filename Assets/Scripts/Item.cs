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
	}

	public Item(int price, string itemName, Sprite sprite){
		this.price = price;
		this.itemName = itemName;
		itemImage = sprite;
	}
    public int price;
	public string itemName;
	/// <summary>
	/// Item's description
	/// </summary>
	public string description;

	public Sprite itemImage;

	/// <summary>
	/// Gets the item type of this item
	/// </summary>
	/// <returns>ItemType enum instance</returns>
	public virtual ItemType GetItemType(){
		return ItemType.Item;
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
		
	}
	public override ItemType GetItemType(){
		return ItemType.Fish;
	}
}

[Serializable]

public class BaitItem : Item
{	
	public int healthReduction;
    public BaitItem(int price, string itemName, Sprite sprite, int healthReduction) : base(price, itemName, sprite)
    {
		this.healthReduction = healthReduction;
    }
	public override ItemType GetItemType(){
		return ItemType.Bait;
	}
}
[Serializable]

public class HealItem : Item
{
	public int healAmount;
    public HealItem(int price, string itemName, Sprite sprite, int healAmount) : base(price, itemName, sprite)
    {
    }
	public override ItemType GetItemType(){
		return ItemType.Heal;
	}
}
[Serializable]

public class BoostItem : Item
{	
	public float boostAmount;
	/// <summary>
	/// Item that boosts the user's strength
	/// </summary>
	/// <param name="price"></param>
	/// <param name="itemName"></param>
	/// <param name="sprite"></param>
	/// <param name="boostAmount">a float multiplier for the strength (for example, 20% -> 1.2)</param>
	/// <returns></returns>
    public BoostItem(int price, string itemName, Sprite sprite, float boostAmount) : base(price, itemName, sprite)
    {
		this.boostAmount = boostAmount;
    }
	public override ItemType GetItemType(){
		return ItemType.Boost;
	}
}

