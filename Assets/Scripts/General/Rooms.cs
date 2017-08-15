using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum RoomName{
	Kitchen,
	Bedroom,
	Bathroom
}

public class Rooms : MonoBehaviour {
	public RoomSO roomSO;

	public RoomItems[] roomItems;
	public GameObject[] buttonChangeSprites;

	public void SwitchEditMode(bool editMode)
	{
		foreach(RoomItems roomItem in roomItems) roomItem.editMode = editMode;

		if(editMode) foreach(GameObject button in buttonChangeSprites) button.SetActive(true);
		else foreach(GameObject button in buttonChangeSprites) button.SetActive(false);
	}

	public void ButtonChangeSpriteOnClick(int item)
	{
		roomItems[item].GetComponent<Image>().sprite = roomSO.content[item].itemSprites[nextIndex(item)];
	}

	protected virtual int nextIndex(int item)
	{
		return 0;
	}
}