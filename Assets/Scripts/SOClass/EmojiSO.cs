using UnityEngine;

[CreateAssetMenu(fileName = "Emoji_",menuName = "Cards/Emoji",order = 1)]
public class EmojiSO : ScriptableObject {
	public int hunger = 100;
	public int hygene = 100;
	public int happiness = 100;
	public int stamina = 100;
	public int health = 100;

	public Sprite[] expressions;
}