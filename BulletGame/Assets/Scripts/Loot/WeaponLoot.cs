using UnityEngine;

//	Scriptable object for standard weapons
[CreateAssetMenu(fileName = "StandardWeaponLoot", menuName = "Loot/Standard Weapon")] public class WeaponLoot : BaseLoot{

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
		if(weapon.GetComponent<BaseWeapon>() == null) weapon = null;
	}

	//	Spawning
	public BaseWeapon spawnEntity(PlayerWeaponHandler.WeaponSlot root){

		//	Variable: Return value / spawned weapon component
		BaseWeapon ans = GameObject.Instantiate(weapon, root.getRoot()).GetComponent<BaseWeapon>();

		//	Set up
		ans.setUp(root.projectiles);

		//	Return
		return ans;

	}

	//	Accessors
	public int getProjCount() => projCount;

}