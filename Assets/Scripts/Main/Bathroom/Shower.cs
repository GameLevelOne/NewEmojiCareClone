using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shower : RoomItems {
	public GameObject waterSprout;
	public Brush brush;

	RectTransform thisTransform;
	Vector2 startPos;

	public bool emojiWet = false;

	void Awake()
	{
		thisTransform = GetComponent<RectTransform>();
		startPos = thisTransform.anchoredPosition;
	}

	void OnEnable()
	{
		waterSprout.GetComponent<WaterSprout>().OnShower += OnShower;
	}

	void OnDisable()
	{
		waterSprout.GetComponent<WaterSprout>().OnShower -= OnShower;
	}

	void OnShower()
	{
		emojiWet = true;
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
			transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag()
	{
		if(!editMode){
			thisTransform.anchoredPosition = startPos;
		}
	}
}
