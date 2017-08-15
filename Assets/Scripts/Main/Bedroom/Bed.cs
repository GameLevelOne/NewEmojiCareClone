using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bed : RoomItems {
	public int staminaFactor = 1;

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

	public void BedOnClick()
	{
		if(!editMode && !emojiSleep)
		{
			if(emoji.state != EmojiState.Sleep && emoji.state != EmojiState.Angry) emoji.Sleep(staminaFactor);
		}
	}
}