using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage; // Reference to the health bar image
    public Sprite[] healthBarSprites; // Array of health bar sprites (0 for empty, 6 for full)
    public int maxHealth = 100; // Maximum health value
    private int currentHealth; // Current health value (0 to maxHealth)

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the health bar with full health
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Method to set the current health value
    public void SetHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();
    }

    // Method to update the health bar image based on the current health value
    private void UpdateHealthBar()
    {
        // Calculate the appropriate sprite index based on the current health
        int spriteIndex = Mathf.RoundToInt((float)currentHealth / maxHealth * (healthBarSprites.Length - 1));
        healthBarImage.sprite = healthBarSprites[spriteIndex];
    }
}
