using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window : RoomItems {
	public Image darkFilter;
	public Bed bed;
	public bool windowOpen = true;

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

	public void WindowOnClick()
	{
		if(!editMode && !emojiSleep){
			if(windowOpen){
				windowOpen = false;
				bed.staminaFactor++;
				darkFilter.color = new Color(0,0,0,darkFilter.color.a + 0.25f);
			}else{
				windowOpen = true;
				bed.staminaFactor--;
				darkFilter.color = new Color(0,0,0,darkFilter.color.a - 0.25f);
			}
		}
	}
}