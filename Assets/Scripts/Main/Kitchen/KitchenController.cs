using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum KitchenItems{
	Plate,
	Fridge,
	Stove,
	Table
}

public class KitchenController : Rooms {
	const string KeyKitchenItemPlate = "Player/Room/Kitchen/Plate";
	const string KeyKitchenItemFridge = "Player/Room/Kitchen/Fridge";
	const string KeyKitchenItemStove = "Player/Room/Kitchen/Stove";
	const string KeyKitchenItemTable = "Player/Room/Kitchen/Table";

	int plate{
		get{return PlayerPrefs.GetInt(KeyKitchenItemPlate,0);}
		set{PlayerPrefs.SetInt(KeyKitchenItemPlate,value);}
	}
	int fridge{
		get{return PlayerPrefs.GetInt(KeyKitchenItemFridge,0);}
		set{PlayerPrefs.SetInt(KeyKitchenItemFridge,value);}
	}
	int stove{
		get{return PlayerPrefs.GetInt(KeyKitchenItemStove,0);}
		set{PlayerPrefs.SetInt(KeyKitchenItemStove,value);}
	}
	int table{
		get{return PlayerPrefs.GetInt(KeyKitchenItemTable,0);}
		set{PlayerPrefs.SetInt(KeyKitchenItemTable,value);}
	}

	protected override int nextIndex(int item)
	{
		int tempIndex = 0;

		switch((KitchenItems)item){
		case KitchenItems.Plate: plate++;
			if(plate == roomSO.content[item].itemSprites.Length) plate = 0;
			tempIndex = plate; break;
		case KitchenItems.Fridge: fridge++;
			if(fridge == roomSO.content[item].itemSprites.Length) fridge = 0;
			tempIndex = fridge; break;
		case KitchenItems.Stove: stove++;
			if(stove == roomSO.content[item].itemSprites.Length) stove = 0;
			tempIndex = stove; break;
		case KitchenItems.Table: table++;
			if(table == roomSO.content[item].itemSprites.Length) table = 0;
			tempIndex = table; break;
		default: break;
		}

		return tempIndex;
	}
}