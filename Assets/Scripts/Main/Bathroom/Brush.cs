using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Brush : RoomItems {
	public GameObject foam;

	RectTransform thisTransform;
	Vector2 startPos;

	bool scrubing = false;

	void Awake()
	{
		thisTransform = GetComponent<RectTransform>();
		startPos = thisTransform.anchoredPosition;
	}

	public void AddFoam()
	{
		foam.SetActive(true);
	}

	public void OnDrag()
	{
		transform.position = Input.mousePosition;
	}
	public void OnEndDrag()
	{
		if(scrubing) foam.SetActive(false);
		scrubing = false;

		StartCoroutine(BackToStartPosition());
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "EmojiTrigger"){
			scrubing = true;
			emoji.Brushed(foam.activeSelf);
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