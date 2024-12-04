using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fisherman_Animator : MonoBehaviour
{
    public string name;
    public int maxHealth;
    public int strength;
    public AttackInfo info;
    public Fish_Animator other;
    private int health;

    bool playerTurn = true;
    int debuff = 0;
    void Start()
    {

        //var playerInfo = GameObject.Find("Playerinfo");
        //playerHealth = playerInfo.transform.Find("Health").GetComponent<TextMeshProUGUI>();

        info.nameText.text = name;
        health = maxHealth;
        updateText();
    }
    public void Pull() {
        other.loseHealth(strength);
        other.updateText();
    }
    public void Reel() {
        other.gainDebuff();
        other.updateText();
    }

    // Update is called once per frame
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
    public void LoseHealth(int otherAttack)
    {
        health -= otherAttack - debuff;
    }
    public void GainHealth(int healAmount)
    {
        health += healAmount;
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


}
