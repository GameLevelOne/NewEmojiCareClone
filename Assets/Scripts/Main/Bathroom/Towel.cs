using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Towel : RoomItems {
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
			StartCoroutine(BackToStartPosition());
		}
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "EmojiTrigger"){
			emoji.Wiped();
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