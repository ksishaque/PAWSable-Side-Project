using UnityEngine;
using NaughtyAttributes;

//	Base class for droppable loot
abstract public class BaseLootData : ScriptableObject{

	//	Enum for rarity levels
	public enum Rarity{STARTER, COMMON, UNCOMMON, RARE, LEGENDARY, INVALID}

	/*	Variables:
	displayName: Name to display in game
	description: Description text to display in inventory
	rarity: Rarity of the weapon
	*/
	[SerializeField, BoxGroup("Visual Info")] private string displayName;
	[SerializeField, BoxGroup("Visual Info"), ResizableTextArea] private string description = "";
	[SerializeField, BoxGroup("Visual Info")] private Rarity rarity;

	virtual protected void OnValidate(){
		if(displayName.Length < 1) rename();
	}
	[Button] private void rename(){
		displayName = name;
	}

	public Rarity getRarity() => rarity;

}