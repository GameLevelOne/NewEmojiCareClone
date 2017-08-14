using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BathroomItems{
	Brush,
	Shower,
	Soap,
	Towel,
	Toy
}

public class BathroomController : Rooms {
	const string KeyBathroomItemBrush = "Player/Room/Bathroom/Brush";
	const string KeyBathroommItemShower = "Player/Room/Bathroom/Shower";
	const string KeyBathroomItemSoap = "Player/Room/Bathroom/Soap";
	const string KeyBathroomItemTowel = "Player/Room/Bathroom/Towel";
	const string KeyBedroomItemToy = "Player/Room/Bedroom/Toy";

	int brush{
		get{return PlayerPrefs.GetInt(KeyBathroomItemBrush,0);}
		set{PlayerPrefs.SetInt(KeyBathroomItemBrush,value);}
	}
	int shower{
		get{return PlayerPrefs.GetInt(KeyBathroommItemShower,0);}
		set{PlayerPrefs.SetInt(KeyBathroommItemShower,value);}
	}
	int soap{
		get{return PlayerPrefs.GetInt(KeyBathroomItemSoap,0);}
		set{PlayerPrefs.SetInt(KeyBathroomItemSoap,value);}
	}
	int towel{
		get{return PlayerPrefs.GetInt(KeyBathroomItemTowel,0);}
		set{PlayerPrefs.SetInt(KeyBathroomItemTowel,value);}
	}
	int toy{
		get{return PlayerPrefs.GetInt(KeyBedroomItemToy,0);}
		set{PlayerPrefs.SetInt(KeyBedroomItemToy,value);}
	}

	public Emoji emoji;
	public Brush brushObject;
	public Towel towelObject;
	public GameObject waterSprout;
	bool foamOnEmoji = false;
	bool emojiWet = false;

	void Awake()
	{
		brushObject.OnScrubbing += EmojiReactOnScrubing;
		towelObject.OnDrying += EmojiReactOnDrying;
	}

	void OnDestroy()
	{
		brushObject.OnScrubbing -= EmojiReactOnScrubing;
		towelObject.OnDrying -= EmojiReactOnDrying;
	}

	void EmojiReactOnScrubing(bool hasFoam)
	{
		if(!emojiWet){
			if(!foamOnEmoji){
				if(hasFoam == false){
					emoji.Annoyed();
				}else{
					foamOnEmoji = true;
					emoji.Happy();
				}
			}
		}
	}

	void EmojiReactOnDrying()
	{
		if(emojiWet){
			emojiWet = false;
			emoji.TickStats(EmojiStats.Happiness,1);
		}
		emoji.Happy();
	}

	public void ButtonSoapOnClick()
	{
		brushObject.AddFoam();
	}

	public void ButtonShowerOnClick()
	{
		if(waterSprout.activeSelf) waterSprout.SetActive(false);
		else{ 
			if(foamOnEmoji){
				emoji.TickStats(EmojiStats.Hygene,3);
				emoji.Happy();
				foamOnEmoji = false;
			}else {
				emoji.TickStats(EmojiStats.Hygene,1);
				emoji.Annoyed();
			}
			waterSprout.SetActive(true);
			emojiWet = true;
		}
	}

	protected override int nextIndex(int item)
	{
		int tempIndex = 0;

		switch((BathroomItems)item){
		case BathroomItems.Brush: brush++;
			if(brush == roomSO.content[item].itemSprites.Length) brush = 0;
			tempIndex = brush; break;
		case BathroomItems.Shower: shower++;
			if(shower == roomSO.content[item].itemSprites.Length) shower = 0;
			tempIndex = shower; break;
		case BathroomItems.Soap: soap++;
			if(soap == roomSO.content[item].itemSprites.Length) soap = 0;
			tempIndex = soap; break;
		case BathroomItems.Towel: towel++;
			if(towel == roomSO.content[item].itemSprites.Length) towel = 0;
			tempIndex = towel; break;
		case BathroomItems.Toy: toy++;
			if(toy == roomSO.content[item].itemSprites.Length) toy = 0;
			tempIndex = toy; break;
		default: break;
		}

		return tempIndex;
	}
}