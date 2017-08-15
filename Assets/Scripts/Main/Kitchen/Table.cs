using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : RoomItems {
	public List<GameObject> ingredients = new List<GameObject>();
	public RectTransform content;
	public GameObject[] ingredientObjets;

	public void Add(int index)
	{
		ingredients.Add(Instantiate(ingredientObjets[index],content));
	}

	public void ResetTable()
	{
		foreach(GameObject g in ingredients) Destroy(g.gameObject);
		ingredients.Clear();
	}
}