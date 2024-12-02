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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnSystem());
        pull.onClick.AddListener(onButtonPressed); //() => { pressed = true; }
        reel.onClick.AddListener(onButtonPressed); //() => { pressed = true; }
    }

    void onButtonPressed() { 
    if(battleState == State.playerturn)
        {
            pressed = true;
            Debug.Log("Player button pressed");
        }
    }

    void Update()
    {
        //coroutines

    }
    IEnumerator TurnSystem()
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
            yield return new WaitForSeconds(3);
            // fish turn
            battleState = State.fishturn;
            Debug.Log("Fish Turn");
            yield return new WaitForSeconds(3);
        }
        Debug.Log("End");
    }
}
