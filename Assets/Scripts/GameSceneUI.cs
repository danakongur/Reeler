using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI on main menu
/// </summary>
public class GameSceneUI : MonoBehaviour
{	
	public void LoadBattle() {
		Loader.Load(Loader.Scene.Battle);
	}
	public void LoadShop() {
		Loader.Load(Loader.Scene.Shop);
	}
	public void LoadAquarium() {
		Loader.Load(Loader.Scene.Aquarium);
	}
}
