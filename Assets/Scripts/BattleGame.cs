using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleGame : MonoBehaviour
{
    public enum State {playerturn, fishturn, win, caught, lose, Playerflee, FishRetreat}
    public Buttons buttons;
    public bool pressed;
    public State battleState;
    public string selectedMove;
	public Item selectedItem;
    public Fisherman_Animator Fisherman;
    public Fish_Animator Fish;
    public EndResultInfo endInfo;
    public TextMeshProUGUI reelDescription;
    public GameObject inventory;
	public TextMeshProUGUI criticalHitText;

	public GameObject descriptionBox;
    private bool gameIsgoing = true;
    /// <summary>
    /// Chance of a critical hit (ex. 5% => 0.05)
    /// </summary>
    public float criticalChance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnSystem());
        buttons.pull.onClick.AddListener(Pull); //() => { pressed = true; }
        buttons.reel.onClick.AddListener(Reel); //() => { pressed = true; }
        buttons.flee.onClick.AddListener(Flee); //() => { pressed = true; }

		// Setting the function to call when item button is pressed
		InventoryPopup popupscript = inventory.GetComponent<InventoryPopup>();
		if(popupscript){
			popupscript.function = ItemClicked;
		}
		else {
			Debug.Log("Couldn't find InventoryPopup component");
		}
    }

    public void Pull()
    {
        //InventoryPopup
        HideItem(inventory);
        if (battleState == State.playerturn)
        {
                pressed = true;
                selectedMove = "Pull";
                
        }
    }
    void Reel()
    {
        HideItem(inventory);
            if (pressed == false)
            {
                pressed = true;
                selectedMove = "Reel";

        }
    }
    void Item()
    {
        //if (pressed == false)
            //pressed = true; // when you have selected an item
            //selectedMove = "Reel";

    }


	/// <summary>
	/// Called when an item in the item menu is clicked
	/// </summary>
	/// <param name="item">Item that was clicked</param>
	void ItemClicked(Item item){
		if (pressed == false) {
			Debug.Log($"item clicked yay");
			pressed = true;
			selectedMove = "Item";
			selectedItem = item;
			HideItem(inventory);
		}

	}

    void Flee()
    {
		HideItem(descriptionBox);
        HideItem(inventory);
        //if (pressed == false)
        //pressed = true; // not neccesary just play animation and wait 3-5 sec and leave
        //selectedMove = "Reel";
        if (pressed == false)
        {
            pressed = true;
            selectedMove = "Flee";
			
        }

    }




    public void Win()
    {
		float healmod = 0.2f; // how much to heal player, 0.2 means 20%
		HideItem(descriptionBox);
        RevealItem(endInfo.gameObject);
		criticalHitText.gameObject.SetActive(false);

		// win screen image
		endInfo.reward.sprite = Fish.GetFishObject().fishImage;

        HideItem(Fish.info.gameObject);
        HideItem(Fish.gameObject);
        endInfo.title.text = "You Beat up the " + Fish.name + "...";
        endInfo.description.text = $"Gain 2 coins and gain {(healmod)*100}% health";

		// Adds fish to inventory
		PlayerManager.instance.CatchFish(this.Fish.GetFishObject());
		PlayerManager.instance.coins += 2;
		Debug.Log($"new health: {(int)Mathf.Round(PlayerManager.instance.health + (healmod*PlayerManager.instance.maxHealth))}, max health:{PlayerManager.instance.maxHealth}");
		PlayerManager.instance.health = Mathf.Min((int)Mathf.Round(PlayerManager.instance.health + (healmod*PlayerManager.instance.maxHealth)), PlayerManager.instance.maxHealth);
    }
    public void Lose()
    {
		HideItem(descriptionBox);
        RevealItem(endInfo.gameObject);
		criticalHitText.gameObject.SetActive(false);

		// lose screen image
		endInfo.reward.sprite = Fish.GetFishObject().fishImage;
        
        HideItem(Fisherman.info.gameObject);
        HideItem(Fisherman.gameObject);
        endInfo.title.text = "The " + Fish.name + " Beat you up!";
        endInfo.description.text = "Medicare is expensive, Lose 2 coins";
		PlayerManager.instance.SubtractCoins(2);
    }
    void HideItem(GameObject a)
    {
        a.SetActive(false);
    }
    void RevealItem(GameObject a)
    {
        a.SetActive(true);
    }

    void Catch()
    {
    }

    public void LoadMain()
    {
        Loader.Load(Loader.Scene.Main);
    }

    public void PlayerFlee()
    {
    }
    void FishRetreat()
    {
        RevealItem(endInfo.gameObject);
        criticalHitText.gameObject.SetActive(false);

        // lose screen image
        endInfo.reward.sprite = Fish.GetFishObject().fishImage;

        HideItem(Fish.info.gameObject);
        HideItem(Fish.gameObject);
        endInfo.title.text = "The " + Fish.name + " Took off";
        endInfo.description.text = "He got away :o";

        bool gameIsgoing = false;

    }
    void PlayerAction()
    {
		HideItem(descriptionBox);
        if (selectedMove == "Pull")
        {
			bool critical = UnityEngine.Random.Range(0f,1f) < criticalChance;
			if (critical){
				coroutine = CriticalHit(new Vector2(200f,85f));

				StartCoroutine(coroutine);
			}
			Debug.Log($"critical status player: {critical}");
            Fisherman.Pull(critical);
        }
        else if (selectedMove == "Reel")
        {
            Fisherman.Reel();
        }
        else if (selectedMove == "Flee")
        {
            LoadMain();
        }
		else if(selectedMove == "Item")
		{
			
			if (selectedItem.GetItemType() == ItemType.Heal){ // player uses healing item
				PlayerManager.instance.RemoveItem(selectedItem);
				inventory.GetComponent<InventoryPopup>().UpdateInventory();

				HealItem item = (HealItem)selectedItem;
				Fisherman.GainHealth(item.healAmount);
				Fisherman.UpdateText();
			}
			else if(selectedItem.GetItemType() == ItemType.Boost){ // stat boost item
				PlayerManager.instance.RemoveItem(selectedItem);
				inventory.GetComponent<InventoryPopup>().UpdateInventory();

				BoostItem item = (BoostItem)selectedItem;
				Fisherman.SetBoost(item.boostAmount);
				Fisherman.UpdateText();
			}
			else if(selectedItem.GetItemType() == ItemType.Bait){ // used bait
				
			}
			else if(selectedItem.GetItemType() == ItemType.Fish){ // used fish??
				
			}
		}
    }
    bool FishAction(String action)
    {
        Debug.Log(action);
        if (action == "Struggle")
        {
			bool critical = UnityEngine.Random.Range(0f,1f) < criticalChance;
			if (critical) {
				coroutine = CriticalHit(new Vector2(-200f,85f));

				StartCoroutine(coroutine);
			}
            Fish.Struggle(critical);    
        }
        else if (action == "Absorb Nutrients")
        {
            Fish.Absorb();
        }
        else if (action == "Cleanse")
        {
            Fish.Cleanse();
        }
        else
        {
            
            bool result=Fish.Retreat();
            if (result==true)
            {
                FishRetreat();
                return true;
            }
        }
        return false;
    }

	private IEnumerator coroutine;

	/// <summary>
	/// Makes the critical hit text appear and then fade out
	/// </summary>
	/// <param name="pos">(local) position to show it at</param>
	public IEnumerator CriticalHit(Vector2 pos) {
		int duration = 20;
		
		criticalHitText.alpha = 1f;
		
		criticalHitText.rectTransform.localPosition = pos;
		criticalHitText.gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < duration; i++){
			yield return new WaitForSeconds(0.1f);
			criticalHitText.alpha = 1f-(((float)i)/((float)duration));
		}
		criticalHitText.gameObject.SetActive(false);
		criticalHitText.alpha = 1f;
	}

    void Update()
    {
        //coroutines

    }
    public IEnumerator TurnSystem()
    {
        Debug.Log("Start");
            while (gameIsgoing=true)
        {
            RevealItem(buttons.gameObject);
            // player turn
            battleState = State.playerturn;
            Debug.Log("Player Turn");
            var fishAction = Fish.Predict();
            reelDescription.text = "Add 1 debuff" + "\n" + "(" + Fish.debuff + " / " + (Fish.strength - 1) + ")";
            while (pressed == false)
            {
                yield return null;
                
            }
            HideItem(buttons.gameObject);
            pressed = false;
            if (Fisherman.GetHealth() > 0)
            {
                if(selectedMove == "Flee")
                {
                    // play animation
                    yield return new WaitForSeconds(2);

                }
                PlayerAction();
            }
            // if health fish <= 0
            if (Fish.GetHealth() <= 0)
            {
                yield return new WaitForSeconds(1);
                Win();
                break;
            }
            // caught
            yield return new WaitForSeconds(2);
            // fish turn
            battleState = State.fishturn;
            Debug.Log("Fish Turn");
            if (Fish.GetHealth() > 0)
            {
                bool fleeSuccess = FishAction(fishAction);
                if (fleeSuccess == true)
                {
                    yield break;
                }
            }   
                // if health player <= 0
                if (Fisherman.GetHealth() <= 0)
            {
                yield return new WaitForSeconds(1);
                Lose();
                break;
            }
            // retreat fish
            yield return new WaitForSeconds(2);
        }
        Debug.Log("End");
    }
}
