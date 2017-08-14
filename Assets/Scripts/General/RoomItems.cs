using UnityEngine;

public class RoomItems : MonoBehaviour {
	public Emoji emoji;
	[HideInInspector] public bool editMode = false, emojiSleep = false;

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
}