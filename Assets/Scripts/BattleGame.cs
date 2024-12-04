using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleGame : MonoBehaviour
{
    public enum State {playerturn, fishturn, win, caught, lose, Playerflee, FishRetreat}
    public Button pull;
    public Button reel;
    public bool pressed;
    public State battleState;
    public string selectedMove;
    public Fisherman_Animator Fisherman;
    public Fish_Animator Fish;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnSystem());
        pull.onClick.AddListener(Pull); //() => { pressed = true; }
        reel.onClick.AddListener(Reel); //() => { pressed = true; }
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
    public void Win()
    {
        Destroy(Fish.info.gameObject);
        Destroy(Fish.gameObject); 
    }
    void Lose()
    {
        Destroy(Fisherman.info.gameObject);
        Destroy(Fisherman.gameObject);
    }

    void Catch()
    {
    }

    public void PlayerFlee()
    {
    }
    void FishRetreat()
    {
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
            // player turn
            battleState = State.playerturn;
            Debug.Log("Player Turn");
            var fishAction = Fish.Predict();
            while (pressed == false)
            {
                yield return null;
                
            }
            pressed = false;
            PlayerAction();
            // if health fish <= 0
            // caught
            Win();
            yield return new WaitForSeconds(2);
            // fish turn
            battleState = State.fishturn;
            Debug.Log("Fish Turn");
            FishAction(fishAction);
            // if health player <= 0
            // retreat fish

            yield return new WaitForSeconds(2);
        }
        Debug.Log("End");
    }
}
