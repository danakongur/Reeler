using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{	
	Button battle;
	Button aquarium;
	Button shop;
	void Awake() {

	}
	public void LoadBattle() {
		Loader.Load(Loader.Scene.Battle);
	}
}
