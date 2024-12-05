using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fish_Animator : MonoBehaviour
{
    public string name;
    int maxHealth;
    int strength;
    public AttackInfo info;
    private int health;
    int debuff = 0;
    public Fisherman_Animator other;
	Fish fishObject;
	

    void Start()
    {

        //var playerInfo = GameObject.Find("Playerinfo");
        //playerHealth = playerInfo.transform.Find("Health").GetComponent<TextMeshProUGUI>();

		// TODO: replace with getting a fish instance from the start
		fishObject = PlayerManager.instance.GetFishByName(name);

		maxHealth = fishObject.health;
		strength = fishObject.strength;
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		if (spriteRenderer) spriteRenderer.sprite = fishObject.fishImage;

        info.nameText.text = name;
        health = maxHealth;
        UpdateText();
    }
    public void UpdateText()
    {
        info.healthText.text = health.ToString() + "/" + maxHealth.ToString();
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
        var nextMove = "";
        var prob = Random.Range(0,10);
        if (prob <= 1 && health>=maxHealth)
        {
            nextMove = "Retreat";
        }
        else if (prob <= 3 && (health < maxHealth || debuff != 0))
        {
            nextMove = "Absorb Nutrients";
        }
        else
        {
            nextMove = "Struggle";
        }
        info.predictionText.text = name + " will attempt to "+nextMove;
        return nextMove;

    }
    public void Struggle()
    {
        other.LoseHealth(strength-debuff);
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
        if (debuff <= strength - 1)
        {
            debuff += 1;
        }
    }

}
