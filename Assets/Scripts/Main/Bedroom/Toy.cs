using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Toy : RoomItems {
	Rigidbody2D thisRigidbody;
	CircleCollider2D thisCollider;
	RectTransform thisTransform;
	Vector2 startPos;

	bool isFlicking = false;

	void Awake()
	{
		thisTransform = GetComponent<RectTransform>();
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisCollider = GetComponent<CircleCollider2D>();
		startPos = GetComponent<RectTransform>().anchoredPosition;
	}

	void OnCollisionEnter2D(Collision2D e)
	{
		if(e.gameObject.tag == "Emoji"){
			StartCoroutine(OnColliding());
		}
	}

	public void OnBeginDrag()
	{
		if(!editMode && !emojiSleep){
			StartCoroutine(flicking());
			thisCollider.enabled = false;
		}
	}
	public void OnDrag()
	{
		if(!editMode && !emojiSleep){
			transform.position = Input.mousePosition;
		}
	}
	public void OnEndDrag()
	{
		
		if(!editMode && !emojiSleep){
			if(isFlicking){
				Vector2 tempResult = GetComponent<RectTransform>().anchoredPosition - startPos;
				StartCoroutine(ToyLaunched(tempResult));
			}else{
				StartCoroutine(ToyReset());
			}
		}
	}

	IEnumerator flicking()
	{
		isFlicking = true;
		yield return new WaitForSeconds(0.2f);
		isFlicking = false;
	}
		
	IEnumerator ToyLaunched(Vector2 result)
	{
		thisCollider.enabled = true;
		thisRigidbody.AddForce(new Vector2(result.x*100,result.y*100));
		yield return new WaitForSeconds(4f);
		StartCoroutine(ToyReset());
	}

	IEnumerator ToyReset()
	{
		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.angularVelocity = 0f;
		thisCollider.enabled = false;

		float t = 0;
		while(t < 1){
			Vector2 curPos = thisTransform.anchoredPosition;
			Quaternion curRot = thisTransform.localRotation;
			curRot.eulerAngles = new Vector3(0,0,curRot.eulerAngles.z % 360f);

			thisTransform.anchoredPosition = Vector2.Lerp(curPos,startPos,t);
			curRot.eulerAngles = Vector3.Lerp(curRot.eulerAngles,Vector3.zero,t);
			thisTransform.localRotation = curRot;

			t+= Time.fixedDeltaTime*5;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}
		thisTransform.anchoredPosition = startPos;
		thisTransform.localRotation = Quaternion.identity;
		thisCollider.enabled = true;
	}

	IEnumerator OnColliding()
	{
		yield return new WaitForSeconds(4f);
		StartCoroutine(ToyReset());
	}

}
