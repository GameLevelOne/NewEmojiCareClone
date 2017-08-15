﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shower : RoomItems {
	public GameObject waterSprout;

	RectTransform thisTransform;
	Vector2 startPos;

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
		emoji.Showered();
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
			StartCoroutine(BackToStartPosition());
		}
	}

	IEnumerator BackToStartPosition()
	{
		float t = 0f;
		while(t < 1f){
			thisTransform.anchoredPosition = Vector2.Lerp(thisTransform.anchoredPosition,startPos,t);
			t += Time.fixedDeltaTime * 5f;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}
		thisTransform.anchoredPosition = startPos;
	}
}