using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Brush : RoomItems {
	public delegate void Scrubbing(bool hasFoam);
	public event Scrubbing OnScrubbing;

	public GameObject foam;

	public bool emojiOnFoam = false;

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
		thisTransform.anchoredPosition = startPos;

		if(scrubing) foam.SetActive(false);
		scrubing = false;
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "Emoji"){
			scrubing = true;
			emojiOnFoam = true;
			if(OnScrubbing != null) OnScrubbing(foam.activeSelf);
		}
	}
}
