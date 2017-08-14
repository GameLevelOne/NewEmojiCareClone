using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shower : RoomItems {
	public GameObject waterSprout;
	Brush brush;

	RectTransform thisTransform;
	Vector2 startPos;

	void Awake()
	{
		thisTransform = GetComponent<RectTransform>();
		startPos = thisTransform.anchoredPosition;
	}

	public void OnPointerClick()
	{
		if(!editMode){
			if(waterSprout.activeSelf) waterSprout.SetActive(false);
			else waterSprout.SetActive(true);
		}

	}

	public void OnDrag()
	{
		if(!editMode){

		}
	}

	public void OnEndDrag()
	{
		if(!editMode){

		}
	}
}
