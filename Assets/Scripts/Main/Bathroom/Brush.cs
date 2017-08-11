using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour {
	public delegate void Scrubbing(bool hasFoam);
	public event Scrubbing OnScrubbing;

	public GameObject foam;
	Vector2 startPos;

	bool scrubing = false;

	void Awake()
	{
		startPos = GetComponent<RectTransform>().anchoredPosition;
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
		GetComponent<RectTransform>().anchoredPosition = startPos;

		if(scrubing) foam.SetActive(false);
		scrubing = false;
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "Emoji"){
			scrubing = true;
			if(OnScrubbing != null) OnScrubbing(foam.activeSelf);
		}
	}
}
