using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemMenu : MonoBehaviour
{
	bool visible = false;

	public void ToggleVisibility(){
		gameObject.SetActive(!gameObject.activeSelf);
	}
}
