using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleGame : MonoBehaviour
{
    public enum State {playerturn, fishturn}
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
            while (pressed == false)
            {
                yield return null;
                
            }
            pressed = false;
            if (selectedMove == "Pull")
            {
                Fisherman.Pull();                
            } else if (selectedMove == "Reel")
            {
                Fisherman.Reel();
            }
            yield return new WaitForSeconds(2);
            // fish turn
            battleState = State.fishturn;
            Debug.Log("Fish Turn");
            var fishAction = Fish.Predict();
            Debug.Log(fishAction);
            yield return new WaitForSeconds(2);
        }
        Debug.Log("End");
    }
}
