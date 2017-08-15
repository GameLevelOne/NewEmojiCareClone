using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lamp : RoomItems {
	public Image darkFilter;
	public Bed bed;
	public bool lampOn = true;

	void OnEnable()
	{
		emoji.OnEmojiSleepEvent += OnEmojiSleepOrAwake;
	}

	void OnDisable()
	{
		emoji.OnEmojiSleepEvent -= OnEmojiSleepOrAwake;
	}

	void OnEmojiSleepOrAwake(){
		if(emoji.state == EmojiState.Sleep) emojiSleep = true;
		else emojiSleep = false;
	}

	public void LampOnClick()
	{
		if(!editMode && !emojiSleep){
			if(lampOn){
				lampOn = false;
				bed.staminaFactor++;
				darkFilter.color = new Color(0,0,0,darkFilter.color.a + 0.5f);
			}else{
				lampOn = true;
				bed.staminaFactor--;
				darkFilter.color = new Color(0,0,0,darkFilter.color.a - 0.5f);
			}
		}
	}
}