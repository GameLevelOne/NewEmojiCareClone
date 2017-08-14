using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bed : RoomItems {
	public int staminaFactor = 1;

	public void BedOnClick()
	{
		if(!editMode && !emojiSleep)
		{
			if(emoji.state != EmojiState.Sleep && emoji.state != EmojiState.Angry) emoji.Sleep(staminaFactor);
		}
	}
}