using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fish_Animator : MonoBehaviour
{
    public string name;
    int maxHealth;
    public int strength;
    public AttackInfo info;
    private int health;
    private int maxDebuff = 3;
    public int debuff = 0;
    public Fisherman_Animator other;
	Fish fishObject;

    public void SetFishObject(Fish fish){
		fishObject = fish;
	}

	public Fish GetFishObject(){
		return fishObject;
	}
	

    void Awake()
    {
        Debug.Log(health);
        //var playerInfo = GameObject.Find("Playerinfo");
        //playerHealth = playerInfo.transform.Find("Health").GetComponent<TextMeshProUGUI>();

		// TODO: replace with getting a fish instance from the start
		//fishObject = PlayerManager.instance.GetFishByName("Carp");
		int randIndex = Random.Range(0,PlayerManager.instance.availableFish.Length);
		fishObject = PlayerManager.instance.availableFish[randIndex];

		name = fishObject.name;
		maxHealth = fishObject.health;
		strength = fishObject.strength;
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		if (spriteRenderer) spriteRenderer.sprite = fishObject.fishImage;

        info.nameText.text = name;
        health = maxHealth;
        UpdateText();
        //Debug.Log(health.ToString()+" health");
    }
    public void UpdateText()
    {
        info.healthText.text = health.ToString() + "/" + maxHealth.ToString();
        info.healthBar.fillAmount = health / (float)maxHealth;
        if (debuff > 0)
        {
            info.attackText.text = strength.ToString() + " - " + debuff.ToString();
        }
        else
        {
            info.attackText.text = strength.ToString();
        }
    }
    public int GetHealth()
    {
        return health;
    }
    public string Predict()
    {
        
        fishObject = PlayerManager.instance.GetFishByName(name);

        maxHealth = fishObject.health;
        strength = fishObject.strength;
        
        var nextMove = "";
        var description = "";
        var prob = Random.Range(0,10);
        Debug.Log(other.strength);
        if (prob <= 1 && health < maxHealth)
        {
            nextMove = "Retreat";
            description = "30% chance of escape";

        }
        else if (prob <= 3 && (health < maxHealth))
        {
            nextMove = "Absorb Nutrients";
            description = "(Heals: " + other.strength +")";
        }
        else if (prob <= 4 && debuff > 0))
        {
            nextMove = "Cleanse";
            description = "(removes 1 Weakened from"+name+")";
        }
        else
        {
            nextMove = "Struggle";
            description = "(Does " + strength + " DMG)";
        }
        info.predictionText.text = name + " will attempt to " + nextMove + " " + description;
        return nextMove;

        
       }
<<<<<<< Updated upstream
    public void Struggle(bool critical)
    {	
        other.LoseHealth(strength-debuff);
=======
    public void Struggle()
    {
        other.LoseHealth(strength - ((strength * debuff) / 5));
>>>>>>> Stashed changes
        other.UpdateText();
    }
    public void Absorb()
    {
        GainHealth(other.strength/2);
        LoseDebuff();
        UpdateText();
    }
    public void Retreat()
    {
        var prob = Random.Range(0, 10);
        if (prob <= 3)
        {
            Debug.Log("Escape success");
        } else
        {
            Debug.Log("Escape fail");


        }
    }

    public void LoseHealth(int otherAttack)
    {
        health -= otherAttack;
    }
    public void GainHealth(int healAmount)
    {
        health += healAmount;
    }
    public void LoseDebuff()
    {
        if (debuff > 0) {
            debuff -= 1;
        }
    }
    public void GainDebuff()
    {
        if (debuff < maxDebuff)
        {
            debuff += 1;
        }
    }


}
