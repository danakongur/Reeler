using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fisherman_Animator : MonoBehaviour
{
    public int maxHealth;
    public int str;
    public TextMeshProUGUI text;
    public TextMeshProUGUI debuffText;
   
    bool playerTurn = true;
    int debuff = 0;
    void Start()
    {
        int health = maxHealth;
        text.text = health.ToString() + "/" + maxHealth.ToString();
        debuffText.text = debuff.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
