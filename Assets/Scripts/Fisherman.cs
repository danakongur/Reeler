using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Fisherman_Animator : MonoBehaviour
{
    public string name;
    public int maxHealth;
    public int strength;
    public AttackInfo info;
    public Fish_Animator other;
    private int health;
    public TextMeshProUGUI reelDescription;
    int debuff = 0;
	/// <summary>
	/// The boost currently on the user
	/// </summary>
	float boost = 1;

    void Start()
    {

        //var playerInfo = GameObject.Find("Playerinfo");
        //playerHealth = playerInfo.transform.Find("Health").GetComponent<TextMeshProUGUI>();

        info.nameText.text = name;
        health = PlayerManager.instance.health;
		maxHealth = PlayerManager.instance.maxHealth;
		strength = PlayerManager.instance.strength;
        UpdateText();
    }
    public int GetHealth()
    {
        return PlayerManager.instance.health;
    }
    public void Pull(bool critical) {
		float mod;
		if (critical) {
			mod = 1.5f;
		}
		else {
			mod = 1f;
		}
		int predamage = strength-debuff;
		float randDMG = Random.Range(0.85f,1f);
		int damage = (int)Mathf.Round(mod*(predamage*randDMG));
		Debug.Log($"Fisherman does {mod*(predamage*randDMG)} damage");
        other.LoseHealth(damage); // multiply by crticial modifier and damage randomness (rounded to nearest integer)
        other.UpdateText();
    }
    public void Reel()
    {
        other.GainDebuff();
        other.UpdateText();
    }
    public void Flee()
    {
        transform.Translate(2 * Vector3.left * Time.deltaTime);
    }
    // Update is called once per frame
    public void UpdateText()
    {	
		health = PlayerManager.instance.health;
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
    public void LoseHealth(int otherAttack)
    {
        //health -= otherAttack - debuff;
		PlayerManager.instance.health -= otherAttack - debuff;
    }
    public void GainHealth(int healAmount)
    {
        //health += healAmount;
		PlayerManager.instance.health += healAmount;
    }
    public void LoseDebuff()
    {
        if (debuff > 0)
        {
            debuff -= 1;
        }
    }
    public void gainDebuff()
    {
        if (debuff <= strength - 1)
        {
            debuff += 1;
        }
    }

	public void SetBoost(float boost) {
		float newstr = strength*boost;
		strength = (int)Mathf.Round(newstr);
	}

	public void RemoveBoost(){
		strength = PlayerManager.instance.strength;
	}
}
