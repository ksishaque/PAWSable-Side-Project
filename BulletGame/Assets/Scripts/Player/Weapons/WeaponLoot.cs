using UnityEngine;

//	Scriptable object for standard weapons
[CreateAssetMenu(fileName = "StandardWeaponLoot", menuName = "Loot/Standard Weapon")] public class WeaponLoot : BaseLoot{

	/*	Variables:
	weapon: Weapon indicated by this loot
	desc: Description of the weapon
	projCount: Number of projectiles that can be loaded
	*/
	[SerializeField, NaughtyAttributes.ValidateInput("validateWeapon", "`weapon` must have a player weapon component")] private GameObject weapon;
	[SerializeField] private string desc = "";

	//	Validation
	private void OnValidate(){
		if(weapon != null && weapon.GetComponent<BaseWeapon>() == null) weapon = null;
	}

	//	Spawning
	public BaseWeapon spawnEntity(PlayerWeaponHandler.WeaponSlot root){

		//	Variable: Return value / spawned weapon component
		BaseWeapon ans = ObjectInitializer.instantiate(weapon, root.getRoot()).GetComponent<BaseWeapon>();

		//	Set up
		ans.setUp(root.projectiles);

		//	Return
		return ans;

	}

	//	Accessors
	[NaughtyAttributes.ShowNativeProperty] private int projectileCount => getProjCount();
	public int getProjCount() => weapon.GetComponent<BaseWeapon>().getProjCount();

}