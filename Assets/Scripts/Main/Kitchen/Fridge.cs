using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : RoomItems {
	public PanelIngredient panelIngredient;

	public void FridgeOnClick()
	{
		if(!editMode){
			panelIngredient.GetComponent<Animator>().SetTrigger("On");
		}
	}
}