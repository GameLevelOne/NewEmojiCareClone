using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum RoomName{
	Kitchen,
	Bedroom,
	Bathroom
}

public class Rooms : MonoBehaviour {
	public delegate void EditModeSwitched();
	public event EditModeSwitched OnEditModeSwitch;

	public RoomSO roomSO;

	public GameObject[] buttonItems;
	public GameObject[] buttonChangeSprites;

	protected bool editMode = false;

	public void SwitchEditMode(bool editMode)
	{
		if(OnEditModeSwitch != null){
			OnEditModeSwitch();
		}

		if(!editMode){
			foreach(GameObject button in buttonItems) if(button.GetComponent<Button>() != null) button.GetComponent<Button>().interactable = true;
			foreach(GameObject button in buttonChangeSprites) button.SetActive(false);
		}else{
			foreach(GameObject button in buttonItems) if(button.GetComponent<Button>() != null) button.GetComponent<Button>().interactable = false;
			foreach(GameObject button in buttonChangeSprites) button.SetActive(true);
		}
	}

	public void ButtonChangeSpriteOnClick(int item)
	{
		buttonItems[item].GetComponent<Image>().sprite = roomSO.content[item].itemSprites[nextIndex(item)];
	}

	protected virtual int nextIndex(int item)
	{
		return 0;
	}
}
