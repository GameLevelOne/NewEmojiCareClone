using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Window : RoomItems {
	public Image darkFilter;
	public Bed bed;
	public bool windowOpen = true;

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