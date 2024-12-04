using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fish_Animator : MonoBehaviour
{
    public string name;
    public int maxHealth;
    public int strength;
    public AttackInfo info;
    private int health;
    int debuff = 0;
    public Fisherman_Animator other;

    void Start()
    {

        //var playerInfo = GameObject.Find("Playerinfo");
        //playerHealth = playerInfo.transform.Find("Health").GetComponent<TextMeshProUGUI>();

        info.nameText.text = name;
        health = maxHealth;
        updateText();
    }
    public void updateText()
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
    public string Predict()
    {
        var prob = Random.Range(0,10);
        if (prob<=8)
        {
            return "Struggle";
        }
        else {
            return "Retreat";
        }
            }
    public void Struggle()
    {
        other.LoseHealth(strength);
        other.updateText();
    }
    public void Reel()
    {
        other.gainDebuff();
        other.updateText();
    }

    public void loseHealth(int otherAttack)
    {
        health -= otherAttack;
    }
    public void gainHealth(int healAmount)
    {
        health += healAmount;
    }
    public void loseDebuff()
    {
        if (debuff > 0) {
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

}
