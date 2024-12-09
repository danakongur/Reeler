using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FishDescription: MonoBehaviour
{
    public string fishName; // The name of the fish
    public string habitat; // The habitat of the fish
    public string diet; // The diet of the fish
    public string behavior; // The behavior of the fish
    public string conservationStatus; // The conservation status of the fish
    public int sellValue; // The worth/value of the fish

    public FishDescription(string fishName, string habitat, string diet, string behavior, string conservationStatus, int sellValue)
    {
        this.fishName = fishName;
        this.habitat = habitat;
        this.diet = diet;
        this.behavior = behavior;
        this.conservationStatus = conservationStatus;
        this.sellValue = sellValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
