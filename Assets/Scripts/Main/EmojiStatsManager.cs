using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmojiStatsManager : MonoBehaviour {
	private static EmojiStatsManager instance = null;
	public static EmojiStatsManager Instance{get{return instance;}}

	public Emoji emoji;
	public PanelStatsController statsController;
	public RoomController roomController;

	[Header("Bedroom Items")]
	public Window window;
	public Lamp lamp;

	void Awake()
	{
		if(instance != null && instance != this) 
			Destroy(this.gameObject);
		else 
			instance = this;
		
	}

	void Start()
	{
		StartCoroutine("TickStats");
	}

	public void EmojiGainStamina(int staminaFactor)
	{
		StartCoroutine("GainStamina",staminaFactor);
	}

	public void StopGainStamina()
	{
		StopCoroutine("GainStamina");
	}

	IEnumerator TickStats()
	{
		while(true){
			yield return new WaitForSeconds(5f);
			emoji.TickStats(EmojiStats.Hunger,hungerFactor(roomController.currentRoom));
			emoji.TickStats(EmojiStats.Hygene,hygeneFactor(roomController.currentRoom));
			emoji.TickStats(EmojiStats.Happiness,happinessFactor(roomController.currentRoom)+bedroomFactor(lamp.lampOn,window.windowOpen));
			if(emoji.state != EmojiState.Sleep) emoji.TickStats(EmojiStats.Stamina,staminaFactor(roomController.currentRoom));
			if(emoji.hungerMod <= 0 || emoji.hygeneMod <= 0 || emoji.happinessMod <= 0 || emoji.staminaMod <= 0)
				emoji.TickStats(EmojiStats.Health);
		}
	}

	int hungerFactor(RoomName roomName)
	{
		if(roomName == RoomName.Bedroom) return -2;
		else return -1;
	}
	int hygeneFactor(RoomName roomName)
	{
		return -1;
	}
	int happinessFactor(RoomName roomName)
	{
		return -1;
	}
	int staminaFactor(RoomName roomName)
	{
		if(roomName == RoomName.Kitchen) return -2;
		else return -1;
	}

	int bedroomFactor(bool lampOn, bool windowOpen)
	{
		if(lampOn || windowOpen){
			return -1;
		}else return 0;
	}

	IEnumerator GainStamina(int value = 1)
	{
		while(true){
			yield return new WaitForSeconds(1f);
			emoji.TickStats(EmojiStats.Stamina,value);
		}
	}
}