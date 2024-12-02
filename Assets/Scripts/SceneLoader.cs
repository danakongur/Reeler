using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene loader
/// </summary>
public static class Loader
{

/// <summary>
/// Main, Battle, Shop, Aquarium
/// </summary>
	public enum Scene {
		Main,
		Battle,
		Shop,
		Aquarium,
	}
    public static void Load(Scene scene){
		SceneManager.LoadScene(scene.ToString());
	}
}
