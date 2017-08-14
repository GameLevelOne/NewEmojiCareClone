using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lamp : RoomItems {
	public Image darkFilter;
	public Bed bed;
	public bool lampOn = true;

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