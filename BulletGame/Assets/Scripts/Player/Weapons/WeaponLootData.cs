using UnityEngine;
using NaughtyAttributes;

//	Scriptable object for standard weapons
[CreateAssetMenu(fileName = "StandardWeaponLoot", menuName = "Loot/Standard Weapon")] public class WeaponLootData : BaseLootData{

	/*	Variables:
	weapon: Weapon indicated by this loot
	projectileCount: Number of projectiles that can be loaded
	*/
	[SerializeField, BoxGroup("References"), Required("`weapon` must have a `BasePlayerWeapn` component")] private GameObject weapon;
	[ShowNativeProperty] private int projectileCount{
		get{
			Prefab.validateComponent<BasePlayerWeapon>(ref weapon);
			return getProjCount();
		}
	}

	//	Validation
	override protected void OnValidate(){

		//	Run base
		base.OnValidate();

		//	Check `weapon`
		Prefab.validateComponent<BasePlayerWeapon>(ref weapon);

	}

	//	Spawning
	public BasePlayerWeapon spawnEntity(PlayerWeaponHandler.WeaponSlot root){

		//	Variable: Return value / spawned weapon component
		BasePlayerWeapon ans = ObjectInitializer.instantiate(weapon, root.getRoot()).GetComponent<BasePlayerWeapon>();

		//	Set up
		ans.setUp(root.projectiles);

		//	Return
		return ans;

	}

	//	Accessors
	public int getProjCount() => weapon.GetComponent<BasePlayerWeapon>().getProjCount();

}