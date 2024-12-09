using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fish
{
    public string name; // The name of the fish
    public int strength; // The strength attribute of the fish
    public int health; // The health attribute of the fish
    public Sprite fishImage; // The sprite image representing the fish
    public FishItem fishItem; // Inventory item for the fish
    public FishDescription fishDescription; // The description of the fish
    public int price; // The price of the fish

    public Fish(string name, int price, int strength, int health, Sprite image, FishDescription description)
    {
        this.name = name;
        this.strength = strength;
        this.health = health;
        this.fishImage = image;
        this.fishItem = new FishItem(price, name, image);
        this.fishDescription = description;
        this.price = price; // Initialize the price
    }
}
