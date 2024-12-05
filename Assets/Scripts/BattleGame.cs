using System;
using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnSystem());
        buttons.pull.onClick.AddListener(Pull); //() => { pressed = true; }
        buttons.reel.onClick.AddListener(Reel); //() => { pressed = true; }
    }

    public void Pull()
    {
        if (battleState == State.playerturn)
        {
                pressed = true;
                selectedMove = "Pull";
                Debug.Log("Reel button pressed");
                
        }
    }
    void Reel()
    {
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
    void Flee()
    {
        //if (pressed == false)
        //pressed = true; // not neccesary just play animation and wait 3-5 sec and leave
        //selectedMove = "Reel";
        Debug.Log("Flee button pressed");

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
            Fisherman.Pull();
        }
        else if (selectedMove == "Reel")
        {
            Fisherman.Reel();
        }

    }
    void FishAction(String action)
    {
        Debug.Log(action);
        if (action == "Struggle")
        {
            Fish.Struggle();    
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
            while (pressed == false)
            {
                yield return null;
                
            }
            HideItem(buttons.gameObject);
            pressed = false;
            if (Fisherman.GetHealth() > 0)
            {
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
            }
            // retreat fish

            yield return new WaitForSeconds(2);
        }
        Debug.Log("End");
    }
}
