using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fish
{
	public string name;
	public int strength;
	public int health;
	public int price;
	public Sprite fishImage;

	/// <summary>
	/// Inventory item for fish
	/// </summary>
	public FishItem fishItem;
	
	public Fish(string name, int price, int strength, int health){
		this.name = name;
		this.strength = strength;
		this.health = health;
		fishImage = null;
		fishItem = new FishItem(price, name, null);
	}
	public Fish(string name, int price, int strength, int health, Sprite image){
		this.name = name;
		this.strength = strength;
		this.health = health;
		fishImage = image;
		fishItem = new FishItem(price, name, image);
	}
}
