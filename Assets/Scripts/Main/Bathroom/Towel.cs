using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Towel : MonoBehaviour {
	public delegate void Drying();
	public event Drying OnDrying;

	Vector2 startPos;

	void Awake()
	{
		startPos = GetComponent<RectTransform>().anchoredPosition;
	}

	public void OnDrag()
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag()
	{
		GetComponent<RectTransform>().anchoredPosition = startPos;
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "Emoji"){
			if(OnDrying != null) OnDrying();
		}
	}
}
