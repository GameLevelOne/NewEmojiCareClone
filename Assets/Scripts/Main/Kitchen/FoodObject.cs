using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoodObject : MonoBehaviour {
	public RectTransform parent, foodBuffer;
	public string foodName;
	public int hungerFactor, hygeneFactor, happinessFactor, staminaFactor, healthfactor;

	public bool editMode = false;

	RectTransform thisTransform;
	Vector2 startPos;

	Emoji emoji = null;

	void Awake()
	{
		thisTransform = GetComponent<RectTransform>();
		startPos = thisTransform.anchoredPosition;
	}


	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "EmojiTrigger"){
			emoji = e.GetComponent<RectTransform>().parent.GetComponent<Emoji>();
		}
	}

	void OnTriggerExit2D(Collider2D e)
	{
		if(e.tag == "EmojiTrigger"){
			emoji = null;
		}
	}

	public void OnBeginDrag()
	{
		thisTransform.SetParent(foodBuffer);
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
			if(emoji == null){ 
				thisTransform.SetParent(parent);
				StartCoroutine(BackToStartPosition());
			}else{
				emoji.Eat(hungerFactor,hygeneFactor,happinessFactor,staminaFactor,healthfactor);
				Destroy(this.gameObject);
			}
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