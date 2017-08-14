using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterSprout : MonoBehaviour {
	public delegate void ShowerEvent();
	public event ShowerEvent OnShower;

	void OnTriggerEnter2D(Collider2D e)
	{
		if(e.tag == "Emoji"){
			if(OnShower != null) OnShower();
		}
	}
}
