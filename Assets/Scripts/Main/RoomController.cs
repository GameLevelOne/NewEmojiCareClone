using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour {
	public Emoji emoji;

	public GameObject[] rooms;
	public GameObject buttonEditRoom, buttonLeft, buttonRight;
	public Text textRoomName, textButtonEditMode;

	public RoomName currentRoom = RoomName.Bedroom;

	bool editMode = false;
	bool emojiSleep = false;

	void Awake()
	{
		emoji.OnEmojiSleepEvent += OnEmojiSleep;
	}

	void Start () 
	{
		Init();
	}

	void Init()
	{
		ChangeRoom(currentRoom);
	}

	void OnEmojiSleep()
	{
		emojiSleep = true;
		emoji.OnEmojiSleepEvent -= OnEmojiSleep;
		emoji.OnEmojiSleepEvent += OnEmojiWake;
		buttonLeft.SetActive(false);
		buttonRight.SetActive(false);
		textButtonEditMode.text = "Wake Up";
	}

	void OnEmojiWake()
	{
		emojiSleep = false;
		emoji.OnEmojiSleepEvent -= OnEmojiWake;
		emoji.OnEmojiSleepEvent += OnEmojiSleep;
		buttonLeft.SetActive(true);
		buttonRight.SetActive(true);
		textButtonEditMode.text = "Edit Room";
		rooms[(int)RoomName.Bedroom].GetComponent<BedroomController>().EnableButtonItems();
	}

	public void ButtonEditRoomOnClick()
	{
		if(!emojiSleep){
			editMode = !editMode;

			if(editMode){
				emoji.gameObject.SetActive(false);
				buttonLeft.SetActive(false);
				buttonRight.SetActive(false);
				textButtonEditMode.text = "Done";
			}else{
				emoji.gameObject.SetActive(true);
				buttonLeft.SetActive(true);
				buttonRight.SetActive(true);
				textButtonEditMode.text = "Edit Room";
			}

			switch(currentRoom){
			case RoomName.Kitchen: rooms[(int)currentRoom].GetComponent<KitchenController>().SwitchEditMode(editMode); break;
			case RoomName.Bathroom: rooms[(int)currentRoom].GetComponent<BathroomController>().SwitchEditMode(editMode); break;
			case RoomName.Bedroom: rooms[(int)currentRoom].GetComponent<BedroomController>().SwitchEditMode(editMode); break;
			default: break;
			}
		}else{
			emoji.Wake();
		}

	}

	public void ButtonLeftOnClick()
	{
		ChangeRoom(prevRoom());
		ValidateButtonLeftRight();
	}

	public void ButtonRightOnClick()
	{
		ChangeRoom(nextRoom());
		ValidateButtonLeftRight();
	}

	void ChangeRoom(RoomName room)
	{
		currentRoom = room;
		for(int i = 0;i<rooms.Length;i++) rooms[i].SetActive(false);
		rooms[(int)room].SetActive(true);
		textRoomName.text = room.ToString();
	}

	RoomName prevRoom()
	{
		int curr = (int)currentRoom;
		curr--;
		currentRoom = (RoomName)curr;

		return currentRoom;
	}

	RoomName nextRoom()
	{
		int curr = (int)currentRoom;
		curr++;
		currentRoom = (RoomName)curr;

		return currentRoom;
	}

	void ValidateButtonLeftRight()
	{
		if(currentRoom == RoomName.Kitchen){
			buttonLeft.SetActive(false);
			buttonRight.SetActive(true);
		}else if(currentRoom == RoomName.Bathroom){
			buttonRight.SetActive(false);
			buttonLeft.SetActive(true);
		}else{
			buttonRight.SetActive(true);
			buttonLeft.SetActive(true);
		}
	}

}
