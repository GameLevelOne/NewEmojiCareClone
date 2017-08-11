using System.Collections;
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
			StartCoroutine("OnSurprised");
		}if(state == EmojiState.Surprised){
			
			if(tapCount++ == 3){
				
			}
			print(tapCount);
		}
	}

	public void OnBeginDrag()
	{
		
	}

	public void OnEndDrag()
	{
		
	}
	#endregion

	#region mechanics_coroutines
	IEnumerator OnSurprised()
	{
		yield return null;
	}
	IEnumerator OnAnnoyed()
	{
		yield return null;
	}
	IEnumerator OnAngry()
	{
		yield return null;
	}
	IEnumerator OnHappy()
	{
		yield return null;
	}
	#endregion

	#region activities
	public void Sleep(){}
	public void Wake(){}
	#endregion


}