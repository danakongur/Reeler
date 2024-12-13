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
	public TextMeshProUGUI healInfoText;

	public GameObject descriptionBox;
    private bool gameIsgoing = true;
    public FishAttackAnimator fishAnimation;
    /// <summary>
    /// Chance of a critical hit (ex. 5% => 0.05)
    /// </summary>
    public float criticalChance;

	IEnumerator playercritcoroutine;
	IEnumerator fishcritcoroutine;

	TMP_Text pullButtonDescription;

	float healmod = 0.2f; // how much to heal player, 0.2 means 20%

    // Start is called before the first frame update
    void Start()
    {
		playercritcoroutine = CriticalHit(new Vector2(200f,85f));
		fishcritcoroutine = CriticalHit(new Vector2(-200f,85f));
        StartCoroutine(TurnSystem());
        buttons.pull.onClick.AddListener(Pull); //() => { pressed = true; }
        buttons.reel.onClick.AddListener(Reel); //() => { pressed = true; }
        buttons.flee.onClick.AddListener(Flee); //() => { pressed = true; }

		pullButtonDescription = buttons.pull.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
		int mindmg = Mathf.RoundToInt(PlayerManager.instance.strength*PlayerManager.instance.minchance);
		int maxdmg= Mathf.RoundToInt(PlayerManager.instance.strength*PlayerManager.instance.maxchance);
		pullButtonDescription.text = $"Deal between {mindmg} and {maxdmg} damage";

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
			// check if bait can be used
			if (item.GetItemType() == ItemType.Bait){
				BaitItem bait = (BaitItem)item;
				int fishMaxHealth = Mathf.RoundToInt(Fish.GetFishObject().health * PlayerManager.instance.GetDifficultyModifier());
				if (Fish.GetMaxHealth() - bait.healthReduction <= 0 || false){
					// if this bait would kill the fish

					//TODO: show text for a moment
					Debug.Log($"{bait.itemName} would kill the fish OR fish max health is already under 10% of original max");
					return;
				}
			}
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
		GameObject loseButtons = endInfo.transform.Find("LoseButtons").gameObject;
		GameObject continueButtons = endInfo.transform.Find("ContinueButtons").gameObject;
		
		HideItem(descriptionBox);
        RevealItem(endInfo.gameObject);
		criticalHitText.gameObject.SetActive(false);
		loseButtons.SetActive(false);
		continueButtons.SetActive(true);

		// win screen image
		endInfo.reward.sprite = Fish.GetFishObject().fishImage;

        HideItem(Fish.info.gameObject);
        HideItem(Fish.gameObject);
		HideItem(endInfo.caughtFishCounter.gameObject);
        endInfo.title.text = "You Beat up the " + Fish.name + "...";
        endInfo.description.text = $"Gain 2 coins and gain {healmod*100}% health";

		// Adds fish to inventory
		PlayerManager.instance.CatchFish(this.Fish.GetFishObject());
		PlayerManager.instance.coins += 2;
		
		int newhealth = Mathf.Min((int)Mathf.Round(PlayerManager.instance.health + (healmod*PlayerManager.instance.maxHealth)), PlayerManager.instance.maxHealth);
		StartCoroutine(Fisherman.AnimateHealthBar(PlayerManager.instance.health, newhealth, 1f));
		PlayerManager.instance.health = newhealth;
		Fisherman.UpdateText();

		// increase difficulty on win
		PlayerManager.instance.IncreaseDifficulty();
    }
    public void Lose()
    {
		GameObject loseButtons = endInfo.transform.Find("LoseButtons").gameObject;
		GameObject continueButtons = endInfo.transform.Find("ContinueButtons").gameObject;
		HideItem(descriptionBox);
        RevealItem(endInfo.gameObject);
		criticalHitText.gameObject.SetActive(false);
		loseButtons.SetActive(true);
		continueButtons.SetActive(false);

		// lose screen image
		endInfo.reward.sprite = Fish.GetFishObject().fishImage;
        
        HideItem(Fisherman.info.gameObject);
        HideItem(Fisherman.gameObject);
        endInfo.title.text = "Game over!\nThe " + Fish.name + " Beat you up!";
        endInfo.description.text = $"The fish took all your items.\nDon't worry! Your fish collection is still there.";
		if (PlayerManager.instance.caughtFishDuringRun > 0){
			RevealItem(endInfo.caughtFishCounter.gameObject);
			endInfo.caughtFishCounter.text = $"You caught {PlayerManager.instance.caughtFishDuringRun} new fish.\nCheck them out in the collection.";
		}

		PlayerManager.instance.LoseReset();
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
		GameObject loseButtons = endInfo.transform.Find("LoseButtons").gameObject;
		GameObject continueButtons = endInfo.transform.Find("ContinueButtons").gameObject;
		RevealItem(endInfo.gameObject);
        criticalHitText.gameObject.SetActive(false);
		loseButtons.SetActive(false);
		continueButtons.SetActive(true);

        // lose screen image
        endInfo.reward.sprite = Fish.GetFishObject().fishImage;

        HideItem(Fish.info.gameObject);
        HideItem(Fish.gameObject);
		HideItem(endInfo.caughtFishCounter.gameObject);
        endInfo.title.text = "You fled";
        endInfo.description.text = $"You gain {healmod*100}% health, but the game still gets harder...";

		// heal
		int newhealth = Mathf.Min((int)Mathf.Round(PlayerManager.instance.health + (healmod*PlayerManager.instance.maxHealth)), PlayerManager.instance.maxHealth);
		StartCoroutine(Fisherman.AnimateHealthBar(PlayerManager.instance.health, newhealth, 1f));
		PlayerManager.instance.health = newhealth;
		Fisherman.UpdateText();

        bool gameIsgoing = false;

		// increase difficulty on flee
		PlayerManager.instance.IncreaseDifficulty();
    }
    void FishRetreat()
    {
		GameObject loseButtons = endInfo.transform.Find("LoseButtons").gameObject;
		GameObject continueButtons = endInfo.transform.Find("ContinueButtons").gameObject;

        RevealItem(endInfo.gameObject);
        criticalHitText.gameObject.SetActive(false);
		loseButtons.SetActive(false);
		continueButtons.SetActive(true);

        // lose screen image
        endInfo.reward.sprite = Fish.GetFishObject().fishImage;

        HideItem(Fish.info.gameObject);
        //HideItem(Fish.gameObject);
		HideItem(endInfo.caughtFishCounter.gameObject);
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
				StopCoroutine(fishcritcoroutine);
				if (playercritcoroutine != null) StopCoroutine(playercritcoroutine);
				playercritcoroutine = CriticalHit(new Vector2(200f,85f));
				StartCoroutine(playercritcoroutine);
			}
            Fisherman.Pull(critical);
        }
        else if (selectedMove == "Reel")
        {
            Fisherman.Reel();
        }
        else if (selectedMove == "Flee")
        {
            PlayerFlee();
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
				PlayerManager.instance.RemoveItem(selectedItem);
				inventory.GetComponent<InventoryPopup>().UpdateInventory();

				BaitItem item = (BaitItem)selectedItem;
				Fish.LoseMaxHealth(item.healthReduction);
				Fish.UpdateText();
				Fish.UpdateHealthBar();
				StartCoroutine(FlashText(healInfoText, $"-{item.healthReduction} max health", 2f));
			}
			else if(selectedItem.GetItemType() == ItemType.Fish){ // used fish??
				
			}
			
			int mindmg = Mathf.RoundToInt(Fisherman.strength*PlayerManager.instance.minchance);
			int maxdmg = Mathf.RoundToInt(Fisherman.strength*PlayerManager.instance.maxchance);
			pullButtonDescription.text = $"Deal {mindmg} to {maxdmg} damage";
		}
    }
    void FishIcon(String action)
    {
        Fish.nextMove.Attack.gameObject.SetActive(false);
        Fish.nextMove.Heal.gameObject.SetActive(false);
        Fish.nextMove.Cleanse.gameObject.SetActive(false);
        Fish.nextMove.Retreat.gameObject.SetActive(false);

        if (action == "Struggle")
        {
            Fish.nextMove.Attack.gameObject.SetActive(true);

        }
        else if (action == "Absorb Nutrients")
        {
            Fish.nextMove.Heal.gameObject.SetActive(true);

        }
        else if (action == "Cleanse")
        {
            Fish.nextMove.Cleanse.gameObject.SetActive(true);

        }
        else
        {
            Fish.nextMove.Retreat.gameObject.SetActive(true);
        }
    }
    bool FishAction(String action)
    {
        Debug.Log(action);
        if (action == "Struggle")
        {
            StartCoroutine(fishAnimation.StruggleAnimation());
			bool critical = UnityEngine.Random.Range(0f,1f) < criticalChance;
			if (critical) {
				StopCoroutine(playercritcoroutine);
				if (fishcritcoroutine != null) StopCoroutine(fishcritcoroutine);
				fishcritcoroutine = CriticalHit(new Vector2(-200f,85f));
				StartCoroutine(fishcritcoroutine);
			}
            Fish.Struggle(critical);    
        }
        else if (action == "Absorb Nutrients")
        {
            StartCoroutine(fishAnimation.HealAnimation());
            Fish.Absorb();
        }
        else if (action == "Cleanse")
        {
            StartCoroutine(fishAnimation.CleanseAnimation());
            Fish.Cleanse();
        }
        else
        {
            bool result =Fish.Retreat();
            if (result==true)
            {
                StartCoroutine(fishAnimation.RetreatAnimation());
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
		Debug.Log($"start crit coroutine pos:{pos}");
		int duration = 10;
		
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

	/// <summary>
	/// Flash text object with text for duration
	/// </summary>
	/// <param name="textObj">object to use</param>
	/// <param name="text">text for object</param>
	/// <param name="duration">duration of flashed text</param>
	public IEnumerator FlashText(TextMeshProUGUI textObj, string text, float duration) {
		textObj.text = text;
		textObj.gameObject.SetActive(true);
		yield return new WaitForSecondsRealtime(duration);
		textObj.gameObject.SetActive(false);
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
            FishIcon(fishAction);
            reelDescription.text = "Add 1 debuff" + "\n" + "(" + Fish.debuff + " / " + (Fish.GetMaxDebuff()) + ")";
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
					Fisherman.gameObject.SetActive(false);
					PlayerFlee();

					yield break;
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
