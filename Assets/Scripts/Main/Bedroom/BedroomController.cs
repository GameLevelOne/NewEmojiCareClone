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

	void OnEnable()
	{
		emoji.OnEmojiSleepEvent += SetBedroomItems;
	}
	void OnDisable()
	{
		emoji.OnEmojiSleepEvent -= SetBedroomItems;
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

	public void SetBedroomItems()
	{
		if(emoji.state == EmojiState.Sleep){
			foreach(RoomItems roomItem in roomItems) roomItem.emojiSleep = true;
		}else{
			foreach(RoomItems roomItem in roomItems) roomItem.emojiSleep = false;
		}
	}
}