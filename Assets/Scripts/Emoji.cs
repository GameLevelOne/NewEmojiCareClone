﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Stats_Category{
	Hunger = 0,
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
	public event EmojiSleep OnEmojiSleepEvent;

	public EmojiSO emojiSO;
	public EmojiState state = EmojiState.Default;

	Animator thisAnim;
	Image thisImage;

	int tapCount;

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

	void Awake()
	{
		Init();
	}

	void Init()
	{
//		thisAnim = GetComponent<Animator>();
		thisImage = GetComponent<Image>();
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
	public void TickStats(Stats_Category category,int ticks = -1)
	{
		switch(category){
		case Stats_Category.Hunger: hungerMod += ticks; break;
		case Stats_Category.Hygene: hygeneMod += ticks; break;
		case Stats_Category.Happiness: happinessMod += ticks; break;
		case Stats_Category.Stamina: staminaMod += ticks; break;
		case Stats_Category.Health: healthMod += ticks; break;
		}
	}

	void SetState(EmojiState state)
	{
		this.state = state;
		thisImage.sprite = expressions[(int)state];
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
		}
	}

	public void OnBeginDrag()
	{
		if(state != EmojiState.Angry && state != EmojiState.Sleep){
			SetState(EmojiState.Blissful);
		}
	}

	public void OnEndDrag()
	{
		if(state == EmojiState.Blissful){
			SetState(EmojiState.Default);
		}
	}
	#endregion

	#region mechanics_coroutines
	IEnumerator OnSurprised(float duration = 3f)
	{
		SetState(EmojiState.Surprised);
		yield return new WaitForSeconds(duration);
		tapCount = 0;
		SetState(EmojiState.Default);

	}
	IEnumerator OnAnnoyed()
	{
		SetState(EmojiState.Annoyed);
		yield return new WaitForSeconds(3f);
		tapCount = 0;
		SetState(EmojiState.Default);
	}
	IEnumerator OnAngry()
	{
		SetState(EmojiState.Angry);
		tapCount = 0;
		yield return new WaitForSeconds(5f);
		SetState(EmojiState.Default);
	}
	IEnumerator OnHappy()
	{
		SetState(EmojiState.Happy);
		yield return new WaitForSeconds(3f);
		SetState(EmojiState.Default);
	}
	#endregion

	#region activities
	public void Sleep(){
		SetState(EmojiState.Sleep);
		if(OnEmojiSleepEvent != null) OnEmojiSleepEvent();
	}
	public void Wake(){
		SetState(EmojiState.Default);
		if(OnEmojiSleepEvent != null) OnEmojiSleepEvent();
	}
	#endregion
}