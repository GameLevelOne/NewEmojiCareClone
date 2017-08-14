using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Soap : RoomItems {
	public Brush brush;

	public void SoapOnClick()
	{
		if(!editMode){
			brush.AddFoam();
		}
	}
}
