using UnityEngine;

[System.Serializable]
public class RoomContent{
	public string itemName;
	public Sprite[] itemSprites;
}

[CreateAssetMenu(fileName = "Room_",menuName = "Cards/Room",order = 1)]
public class RoomSO : ScriptableObject {
	public RoomName roomName;
	public RoomContent[] content;
}
