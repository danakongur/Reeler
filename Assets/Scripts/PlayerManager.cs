using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerManager has information that persists between scenes
/// </summary>
public class PlayerManager : MonoBehaviour
{

	public static PlayerManager instance;
    
	void Awake() {
		instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
}
