using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Recipe{
	public GameObject foodObject;
	public IngredientItems[] ingredients;
	public int cookDuration;
}


public class Cookbook : RoomItems {
	public Animator panelCookbook;
	public Recipe[] recipes;


	public void CookbookOnClick()
	{
		if(!editMode) panelCookbook.SetTrigger("On");
	}
}
