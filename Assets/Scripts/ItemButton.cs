using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{

	public Item item;
	public GameObject descriptionBox;
    
	public void ShowDescription() {
		if(!descriptionBox){
			return;
		}
		descriptionBox.SetActive(true);
		Transform iname = descriptionBox.transform.Find("Name");
		Transform price = descriptionBox.transform.Find("Price");
		Transform type = descriptionBox.transform.Find("Type");
		Transform desc = descriptionBox.transform.Find("Description");
		
		TMPro.TMP_Text nameT = iname.GetComponent<TMPro.TMP_Text>();
		TMPro.TMP_Text priceT = price.GetComponent<TMPro.TMP_Text>();
		TMPro.TMP_Text typeT = type.GetComponent<TMPro.TMP_Text>();
		TMPro.TMP_Text descT = desc.GetComponent<TMPro.TMP_Text>();

		nameT.text = item.itemName;
		priceT.text = $"{item.price} coins";
		typeT.text = $"Type: {item.GetItemType().ToString()}";
		descT.text = item.description;

	}

	public void HideDescription() {
		if(!descriptionBox){
			return;
		}
		descriptionBox.SetActive(false);
	}

}
