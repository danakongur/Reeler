using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    void Start() {
		Button itemBut = gameObject.GetComponent<Button>();
		if (itemBut){ // ensure button component exists
			// Adds function call as listener to button
			itemBut.onClick.AddListener(delegate {
				Loader.Load(Loader.Scene.Main);
			});
		}
	}


}
