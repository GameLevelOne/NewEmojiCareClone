using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Towel : RoomItems {
	public delegate void Drying();
	public event Drying OnDrying;

	RectTransform thisTransform;
	Vector2 startPos;

	void Awake()
	{
		thisTransform = GetComponent<RectTransform>();
		startPos = thisTransform.anchoredPosition;
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
			GetComponent<RectTransform>().anchoredPosition = startPos;
		}
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "Emoji"){
			if(OnDrying != null) OnDrying();
		}
	}
}