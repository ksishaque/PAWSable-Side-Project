using UnityEngine;

//	Scriptable object for standard weapons
[CreateAssetMenu(fileName = "StandardWeaponLoot", menuName = "Loot/Standard Weapon")] public class StandardWeaponLoot : BaseLoot{

	/*	Variables:
	weapon: Weapon indicated by this loot
	desc: Description of the weapon
	projCount: Number of projectiles that can be loaded
	*/
	[SerializeField] private GameObject weapon;
	[SerializeField] private string desc = "";
	[SerializeField] private int projCount = 1;

	//	Validation
	private void OnValidate(){
		if(weapon.GetComponent<BaseStandardWeapon>() == null) weapon = null;
	}

	//	Spawning
	public BaseStandardWeapon SpawnEntity(PlayerWeaponHandler.WeaponSlot root){

		//	Variable: Spawned weapon component
		BaseStandardWeapon ans = GameObject.Instantiate(weapon, root.GetRoot()).GetComponent<BaseStandardWeapon>();

		//	TODO: Animations

		//	Return
		return ans;

	}

	//	Accessors
	public int GetProjCount() => projCount;

}