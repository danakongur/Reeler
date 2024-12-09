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
    public Fisherman_Animator Fisherman;
    public Fish_Animator Fish;
    public EndResultInfo endInfo;
    public TextMeshProUGUI reelDescription;
    public GameObject inventory;

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
                Debug.Log("Pull button pressed");
                
        }
    }
    void Reel()
    {
        HideItem(inventory);
            if (pressed == false)
            {
                pressed = true;
                selectedMove = "Reel";
                Debug.Log("Reel button pressed");

        }
    }
    void Item()
    {
        //if (pressed == false)
            //pressed = true; // when you have selected an item
            //selectedMove = "Reel";
            Debug.Log("Item button pressed");

    }


	/// <summary>
	/// Called when an item in the item menu is clicked
	/// </summary>
	/// <param name="item">Item that was clicked</param>
	void ItemClicked(Item item){
		Debug.Log($"battle game item {item.itemName} clicked");
	}

    void Flee()
    {
        HideItem(inventory);
        //if (pressed == false)
        //pressed = true; // not neccesary just play animation and wait 3-5 sec and leave
        //selectedMove = "Reel";
        if (pressed == false)
        {
            pressed = true;
            selectedMove = "Flee";
            Debug.Log("Reel button pressed");

        }

    }




    public void Win()
    {
        RevealItem(endInfo.gameObject);

		// win screen image
		endInfo.reward.sprite = Fish.GetFishObject().fishImage;

        HideItem(Fish.info.gameObject);
        HideItem(Fish.gameObject);
        endInfo.title.text = "You Beat up the " + Fish.name + "...";
        endInfo.description.text = "Gain 2 coins";
		PlayerManager.instance.CatchFish(this.Fish.GetFishObject());
		PlayerManager.instance.coins += 2;
    }
    public void Lose()
    {
        RevealItem(endInfo.gameObject);

		// lose screen image
		endInfo.reward.sprite = Fish.GetFishObject().fishImage;
        
        Debug.Log(Fisherman.GetHealth());
        HideItem(Fisherman.info.gameObject);
        HideItem(Fisherman.gameObject);
        endInfo.title.text = "The " + Fish.name + " Beat you up!";
        endInfo.description.text = "Medicare is expensive, Lose 2 coins";
		PlayerManager.instance.coins -= 2;
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
        {
            RevealItem(endInfo.gameObject);
            HideItem(Fisherman.info.gameObject);
            HideItem(Fisherman.gameObject);
            endInfo.title.text = "The " + Fish.name + " Managed to escape!";
        }

    }
    void PlayerAction()
    {
        if (selectedMove == "Pull")
        {
			bool critical = UnityEngine.Random.Range(0f,1f) < criticalChance;
			if (critical){
				// do some critical behavior here
			}
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
    }
    void FishAction(String action)
    {
        Debug.Log(action);
        if (action == "Struggle")
        {
			bool critical = UnityEngine.Random.Range(0f,1f) < criticalChance;
            Fish.Struggle(critical);    
        }
        else if (action == "Absorb Nutrients")
        {
            Fish.Absorb();
        }
        else {
            Fish.Retreat();
        }
    }


    void Update()
    {
        //coroutines

    }
    public IEnumerator TurnSystem()
    {
        Debug.Log("Start");
        while (true)
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
                FishAction(fishAction);
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
