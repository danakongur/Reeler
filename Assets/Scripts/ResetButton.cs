using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    void Start()
    {
        Button itemBut = gameObject.GetComponent<Button>();
		if (itemBut){ // ensure button component exists
			// Adds function call as listener to button
			itemBut.onClick.AddListener(PlayerManager.instance.ResetPlayerManager);
		}
    }

	void Update() {
		if (Input.GetKey(KeyCode.KeypadMultiply) || Input.GetKey(KeyCode.KeypadPeriod) || Input.GetKey(KeyCode.Plus)){
			PlayerManager.instance.ResetPlayerManager();
		}
	}


}
