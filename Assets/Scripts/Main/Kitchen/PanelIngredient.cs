using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelIngredient : MonoBehaviour {
	public Button[] buttonIngredients;

	public Table table;

	public void ButtonIngredientOnClick(int index)
	{
		buttonIngredients[index].interactable = false;
		table.Add(index);
	}

	public void ResetButtons()
	{
		foreach(Button b in buttonIngredients) b.interactable = true;
	}
}