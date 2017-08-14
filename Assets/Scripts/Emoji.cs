using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EmojiStats{
	Hunger,
	Hygene,
	Happiness,
	Stamina,
	Health
}

public enum EmojiState{
	Default = 0,
	Surprised,
	Annoyed,
	Angry,
	Happy,
	Blissful,
	Sleep
}

public class Emoji : MonoBehaviour {
	public delegate void EmojiSleep();
	public delegate void EmojiTickStats();
	public event EmojiSleep OnEmojiSleepEvent;
	public event EmojiTickStats OnEmojiTickStats;

	public EmojiSO emojiSO;
	public EmojiState state = EmojiState.Default;



	int tapCount;
	bool sleeping = false;

	#region stats
	const string KeyEmojiHunger = "Player/Emoji/Hunger";
	const string KeyEmojiHygene = "Player/Emoji/Hygene";
	const string KeyEmojiHappiness = "Player/Emoji/Happiness";
	const string KeyEmojiStamina = "Player/Emoji/Stamina";
	const string KeyEmojiHealth = "Player/Emoji/Health";

	public int hunger{ 	get{return emojiSO.hunger;}}
	public int hygene{	get{return emojiSO.hygene;}}
	public int happiness{ get{return emojiSO.happiness;}}
	public int stamina{ get{return emojiSO.stamina;}}
	public int health{ 	get{return emojiSO.health;}}

	public int hungerMod{
		get{return PlayerPrefs.GetInt(KeyEmojiHunger,emojiSO.hunger/2);}
		set{PlayerPrefs.SetInt(KeyEmojiHunger,value);}
	}
	public int hygeneMod{
		get{return PlayerPrefs.GetInt(KeyEmojiHygene,emojiSO.hygene/2);}
		set{PlayerPrefs.SetInt(KeyEmojiHygene,value);}
	}
	public int happinessMod{
		get{return PlayerPrefs.GetInt(KeyEmojiHappiness,emojiSO.happiness/2);}
		set{PlayerPrefs.SetInt(KeyEmojiHappiness,value);}
	}
	public int staminaMod{
		get{return PlayerPrefs.GetInt(KeyEmojiStamina,emojiSO.stamina/2);}
		set{PlayerPrefs.SetInt(KeyEmojiStamina,value);}
	}
	public int healthMod{
		get{return PlayerPrefs.GetInt(KeyEmojiHealth,emojiSO.health/2);}
		set{PlayerPrefs.SetInt(KeyEmojiHealth,value);}
	}

	public Sprite[] expressions{ get{return emojiSO.expressions;} }
	#endregion

	Rigidbody2D thisRigidbody;
	RectTransform thisTransform;
	Animator thisAnim;
	Image thisImage;

	Vector2 flickStartPos;
	bool bSwiping = false;
	bool isLaunched = false;

	void Awake()
	{
		Init();
	}

	void Init()
	{
//		thisAnim = GetComponent<Animator>();
		thisImage = GetComponent<Image>();
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisTransform = GetComponent<RectTransform>();
	}

	void OnEnable()
	{
		if(state == EmojiState.Angry){
			StartCoroutine("OnAngry");
		} else {
			SetState(EmojiState.Default);
		}
	}

	void OnDisable()
	{
		StopAllCoroutines();
	}

	#region mechanics_general
	public void TickStats(EmojiStats category,int ticks = -1)
	{
		switch(category){
		case EmojiStats.Hunger: hungerMod += ticks; break;
		case EmojiStats.Hygene: hygeneMod += ticks; break;
		case EmojiStats.Happiness: happinessMod += ticks; break;
		case EmojiStats.Stamina: staminaMod += ticks; break;
		case EmojiStats.Health: healthMod += ticks; break;
		}
		AdjustStats();
		if(OnEmojiTickStats != null) OnEmojiTickStats();
	}

	void AdjustStats()
	{
		if(hungerMod > hunger) hungerMod = hunger;
		if(hygeneMod > hygene) hygeneMod = hunger;
		if(happinessMod > happiness) happinessMod = happiness;
		if(staminaMod > stamina) staminaMod = stamina;
		if(healthMod > health) healthMod = health;
		
		if(hungerMod < 0) hungerMod = 0;
		if(hygeneMod < 0) hygeneMod = 0;
		if(happinessMod < 0) happinessMod = 0;
		if(staminaMod < 0) staminaMod = 0;
		if(healthMod < 0) healthMod = 0;
	}

	//CHEAT
	public void ResetStats()
	{
		TickStats(EmojiStats.Hunger,emojiSO.hunger);
		TickStats(EmojiStats.Hygene,emojiSO.hunger);
		TickStats(EmojiStats.Happiness,emojiSO.hunger);
		TickStats(EmojiStats.Stamina,emojiSO.hunger);
		TickStats(EmojiStats.Health,emojiSO.hunger);
	}

	void SetState(EmojiState state)
	{
		if(state == EmojiState.Default && sleeping) this.state = EmojiState.Sleep;
		else this.state = state;

		thisImage.sprite = expressions[(int)this.state];
	}
	#endregion

	#region mechanics_collissions
	void OnCollisionEnter2D(Collision2D e){
		if(e.gameObject.tag == "Toy"){
			if(!isLaunched) StartCoroutine(OnColliding());
		}
	}
	#endregion

	#region mechanics_event_triggers
	public void OnPointerClick()
	{
		if(state == EmojiState.Default){
			SetState(EmojiState.Surprised);
			StartCoroutine("OnSurprised", 3f);
		}else if(state == EmojiState.Surprised){
			if(tapCount++ == 5){
				StopCoroutine("OnSurprised");
				StartCoroutine("OnAnnoyed");
			}
		}else if(state == EmojiState.Annoyed) {
			if(tapCount++ == 10){
				StopCoroutine("OnAnnoyed");
				StartCoroutine("OnAngry");
			}
		}else if(state == EmojiState.Sleep){
			Wake();
		}
	}

	public void OnBeginDrag()
	{
		if(state != EmojiState.Sleep){
			bSwiping = true;
			flickStartPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			StartCoroutine(Flicking());
			if(state != EmojiState.Angry && state != EmojiState.Sleep){
				SetState(EmojiState.Blissful);
			}
		}

	}

	public void OnEndDrag()
	{
		if(state != EmojiState.Sleep){
			if(bSwiping){
				Vector2 flickEndPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
				Vector2 tempResult = flickEndPos - flickStartPos;
				StartCoroutine(EmojiLaunched(tempResult));
			}
			if(state == EmojiState.Blissful){
				TickStats(EmojiStats.Happiness,1);
				SetState(EmojiState.Default);
			}
		}
	}
	#endregion

	#region mechanics_coroutines
	IEnumerator Flicking()
	{
		yield return new WaitForSeconds(0.2f);
		bSwiping = false;
	}

	IEnumerator EmojiLaunched(Vector2 result)
	{
		isLaunched = true;
		thisRigidbody.AddForce(new Vector2(result.x*100,result.y*100));
		yield return new WaitForSeconds(4f);

		StartCoroutine(EmojiResetPosition());
	}

	IEnumerator EmojiResetPosition()
	{
		isLaunched = false;
		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.angularVelocity = 0f;
		GetComponent<CircleCollider2D>().enabled = false;

		float t = 0;
		while(t < 1f){
			Vector2 curPos = thisTransform.anchoredPosition;
			Quaternion curRot = thisTransform.localRotation;
			curRot.eulerAngles = new Vector3(0,0,curRot.eulerAngles.z % 360f);

			thisTransform.anchoredPosition = Vector2.Lerp(curPos,Vector2.zero,t);
			curRot.eulerAngles = Vector3.Lerp(curRot.eulerAngles,Vector3.zero,t);
			thisTransform.localRotation = curRot;

			t+= Time.fixedDeltaTime*5;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}

		thisTransform.anchoredPosition = Vector2.zero;
		thisTransform.localRotation = Quaternion.identity;
		GetComponent<CircleCollider2D>().enabled = true;
	}

	IEnumerator OnColliding()
	{
		yield return new WaitForSeconds(4f);
		StartCoroutine(EmojiResetPosition());
	}

	IEnumerator OnSurprised(float duration = 2f)
	{
		SetState(EmojiState.Surprised);
		yield return new WaitForSeconds(duration);
		tapCount = 0;
		SetState(EmojiState.Default);

	}
	IEnumerator OnAnnoyed()
	{
		SetState(EmojiState.Annoyed);
		yield return new WaitForSeconds(2f);
		tapCount = 0;
		SetState(EmojiState.Default);
	}
	IEnumerator OnAngry()
	{
		SetState(EmojiState.Angry);
		tapCount = 0;
		TickStats(EmojiStats.Happiness);
		yield return new WaitForSeconds(5f);
		SetState(EmojiState.Default);
	}
	IEnumerator OnHappy()
	{
		SetState(EmojiState.Happy);
		yield return new WaitForSeconds(2f);
		SetState(EmojiState.Default);
	}
	#endregion

	#region activities
	public void Sleep(int staminaFactor){
		sleeping = true;
		SetState(EmojiState.Sleep);
		GetComponent<RectTransform>().anchoredPosition = new Vector2(150f,529f);
		EmojiStatsManager.Instance.EmojiGainStamina(staminaFactor);
		if(OnEmojiSleepEvent != null) OnEmojiSleepEvent();
	}
	void Wake(){
		EmojiStatsManager.Instance.StopGainStamina();
		sleeping = false;
		SetState(EmojiState.Default);
		GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		if(OnEmojiSleepEvent != null) OnEmojiSleepEvent();
	}
	#endregion

	#region reaction
	public void Happy()
	{
		StopAllCoroutines();
		TickStats(EmojiStats.Happiness,1);
		StartCoroutine("OnHappy");
	}
	public void Annoyed()
	{
		StopAllCoroutines();
		TickStats(EmojiStats.Happiness);
		StartCoroutine("OnAnnoyed");
	}
	#endregion
}