using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackInfo : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI predictionText;
    public Image healthBar;
    public TextMeshProUGUI habitatText; // New field for habitat
    public TextMeshProUGUI dietText; // New field for diet
    public TextMeshProUGUI behaviorText; // New field for behavior
    public TextMeshProUGUI conservationStatusText; // New field for conservation status
    public TextMeshProUGUI worthText; // New field for worth

    public void UpdateAttackInfo(Fish fish)
    {
        nameText.text = fish.name;
        healthText.text = $"Health: {fish.health}";
        attackText.text = $"Strength: {fish.strength}";
        predictionText.text = fish.fishDescription.behavior;
        habitatText.text = $"Habitat: {fish.fishDescription.habitat}";
        dietText.text = $"Diet: {fish.fishDescription.diet}";
        behaviorText.text = $"Behavior: {fish.fishDescription.behavior}";
        conservationStatusText.text = $"Conservation Status: {fish.fishDescription.conservationStatus}";
        worthText.text = $"Worth: {fish.fishDescription.sellValue} coins";
    }
}
