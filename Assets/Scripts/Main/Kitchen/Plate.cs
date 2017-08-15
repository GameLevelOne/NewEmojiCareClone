using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Plate : RoomItems {
	public GameObject food;
	public RectTransform content;
	public RectTransform foodBuffer;

	public void Add(GameObject foodObject)
	{
		food = Instantiate(foodObject,content);
		food.GetComponent<FoodObject>().parent = GetComponent<RectTransform>();
		food.GetComponent<FoodObject>().foodBuffer = foodBuffer;
	}
}
