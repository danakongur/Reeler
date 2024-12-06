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
    public TextMeshProUGUI reelDescription;
    int debuff = 0;
    void Start()
    {

        //var playerInfo = GameObject.Find("Playerinfo");
        //playerHealth = playerInfo.transform.Find("Health").GetComponent<TextMeshProUGUI>();

        info.nameText.text = name;
        health = maxHealth;
        UpdateText();
    }
    public int GetHealth()
    {
        return health;
    }
    public void Pull() {
        other.LoseHealth(strength-debuff);
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
		Debug.Log($"fish debuff {other.debuff}, {debuff}");
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
