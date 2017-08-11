using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelStatsController : MonoBehaviour {
	public Image[] imageFillers;
	public Emoji emoji;

	void Awake()
	{
		UpdateStats();
	}

	void OnEnable()
	{
		emoji.OnEmojiTickStats += UpdateStats;
	}

	void OnDestroy()
	{
		emoji.OnEmojiTickStats -= UpdateStats;
	}

	void UpdateStats()
	{
		imageFillers[(int)EmojiStats.Hunger].fillAmount = 	(float)emoji.hungerMod/		(float)emoji.hunger;
		imageFillers[(int)EmojiStats.Hygene].fillAmount = 	(float)emoji.hygeneMod/		(float)emoji.hygene;
		imageFillers[(int)EmojiStats.Happiness].fillAmount =(float)emoji.happinessMod/	(float)emoji.happiness;
		imageFillers[(int)EmojiStats.Stamina].fillAmount = 	(float)emoji.staminaMod/	(float)emoji.stamina;
		imageFillers[(int)EmojiStats.Health].fillAmount = 	(float)emoji.healthMod/		(float)emoji.health;
	}
}