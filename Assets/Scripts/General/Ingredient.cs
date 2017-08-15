using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum IngredientItems{
	Cabbage,
	Carrot,
	Cheese,
	Chicken,
	Egg,
	Fish,
	Flour,
	Meat,
	Mushroom,
	Tomato,
}

public class Ingredient : MonoBehaviour {
	public IngredientItems type;
	public bool editMode = false;

	BoxCollider2D thisCollider;
	RectTransform thisTransform;
	Vector2 startPos;

	Stove stove;

	void Awake()
	{
		thisCollider = GetComponent<BoxCollider2D>();
		thisTransform = GetComponent<RectTransform>();
		startPos = thisTransform.anchoredPosition;
	}

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "Stove"){
			stove = e.GetComponent<Stove>();
		}
	}

	void OnTriggerExit2D(Collider2D e)
	{
		if(e.tag == "Stove"){
			stove = null;
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
			if(stove != null){
				stove.AddIngredient(this);
				thisTransform.anchoredPosition = new Vector2(thisTransform.anchoredPosition.x,2000);
			}else{
				StartCoroutine(BackToStartPosition());
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