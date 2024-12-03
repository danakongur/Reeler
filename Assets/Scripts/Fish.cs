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
	public Sprite fishImage;
	
	public Fish(string name, int strength, int health){
		this.name = name;
		this.strength = strength;
		this.health = health;
		fishImage = null;
	}
	public Fish(string name, int strength, int health, Sprite image){
		this.name = name;
		this.strength = strength;
		this.health = health;
		fishImage = image;
	}
}
