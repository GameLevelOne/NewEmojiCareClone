using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BedroomItems{
	Window,
	Bed,
	Lamp,
	Toy
}

public class BedroomController : Rooms {
	const string KeyBedroomItemWindow = "Player/Room/Bedroom/Window";
	const string KeyBedroomItemBed = "Player/Room/Bedroom/Bed";
	const string KeyBedroomItemLamp = "Player/Room/Bedroom/Lamp";
	const string KeyBedroomItemToy = "Player/Room/Bedroom/Toy";

	int window{
		get{return PlayerPrefs.GetInt(KeyBedroomItemWindow,0);}
		set{PlayerPrefs.SetInt(KeyBedroomItemWindow,value);}
	}
	int bed{
		get{return PlayerPrefs.GetInt(KeyBedroomItemBed,0);}
		set{PlayerPrefs.SetInt(KeyBedroomItemBed,value);}
	}
	int lamp{
		get{return PlayerPrefs.GetInt(KeyBedroomItemLamp,0);}
		set{PlayerPrefs.SetInt(KeyBedroomItemLamp,value);}
	}
	int toy{
		get{return PlayerPrefs.GetInt(KeyBedroomItemToy,0);}
		set{PlayerPrefs.SetInt(KeyBedroomItemToy,value);}
	}

	public Emoji emoji;
	public Image darkFilter;

	public bool lampOn = true;
	public bool windowOpen = true;

	int staminaFactor = 1;

	public void ButtonWindowOnClick()
	{
		if(windowOpen){
			windowOpen = false;
			UpdateDarkFilter(0.25f);
			staminaFactor++;
		}else{
			windowOpen = true;
			UpdateDarkFilter(-0.25f);
			staminaFactor--;
		}
	}

	public void ButtonBedOnClick()
	{
		foreach(GameObject a in buttonItems) a.GetComponent<Button>().interactable = false;
		if(emoji.state != EmojiState.Sleep && emoji.state != EmojiState.Angry) emoji.Sleep(staminaFactor);
	}

	public void ButtonLampOnClick()
	{
		if(lampOn){
			lampOn = false;
			UpdateDarkFilter(0.5f);
			staminaFactor++;
		}else{
			lampOn = true;
			UpdateDarkFilter(-0.5f);
			staminaFactor--;
		}
	}

	public void ButtonToyOnClick()
	{
		
	}

	protected override int nextIndex(int item)
	{
		int tempIndex = 0;

		switch((BedroomItems)item){
		case BedroomItems.Window: window++;
			if(window == roomSO.content[item].itemSprites.Length) window = 0;
			tempIndex = window; break;
		case BedroomItems.Bed: bed++;
			if(bed == roomSO.content[item].itemSprites.Length) bed = 0;
			tempIndex = bed; break;
		case BedroomItems.Lamp: lamp++;
			if(lamp == roomSO.content[item].itemSprites.Length) lamp = 0;
			tempIndex = lamp; break;
		case BedroomItems.Toy: toy++;
			if(toy == roomSO.content[item].itemSprites.Length) toy = 0;
			tempIndex = toy; break;
		default: break;
		}

		return tempIndex;
	}

	void UpdateDarkFilter(float value)
	{
		darkFilter.color = new Color(0,0,0,darkFilter.color.a+value);
	}

	public void EnableButtonItems()
	{
		foreach(GameObject a in buttonItems) a.GetComponent<Button>().interactable = true;
	}
}