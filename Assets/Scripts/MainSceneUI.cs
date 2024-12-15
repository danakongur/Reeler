using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI on main menu
/// </summary>
public class MainSceneUI : MonoBehaviour
{	
	public GameObject tutorial;
	public void LoadTutorial() {
		if (PlayerManager.instance.tutorial){
			tutorial.SetActive(true);
			PlayerManager.instance.tutorial = false;
		}
		else {
			LoadBattle();
		}
	}
	public void LoadBattle() {
		Loader.Load(Loader.Scene.Battle);
	}
	public void LoadShop() {
		Loader.Load(Loader.Scene.Shop);
	}
	public void LoadAquarium() {
		Loader.Load(Loader.Scene.Aquarium);
	}

	public void LoadMain() {
		Loader.Load(Loader.Scene.Main);
	}

	public void LoadSample() {
		Loader.Load(Loader.Scene.SampleScene);
	}
}
