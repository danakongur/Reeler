using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemMenu : MonoBehaviour
{
	bool visible = false;

	public void ToggleVisibility(){
		visible = !visible;
		gameObject.SetActive(visible);
	}
}
