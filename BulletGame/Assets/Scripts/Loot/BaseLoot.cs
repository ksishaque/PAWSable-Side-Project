using UnityEngine;

//	Base class for droppable loot
abstract public class BaseLoot : ScriptableObject{

	public enum Rarity{COMMON, UNCOMMON, RARE, LEGENDARY}

	[SerializeField] private Rarity rarity;

	public Rarity getRarity() => rarity;

}