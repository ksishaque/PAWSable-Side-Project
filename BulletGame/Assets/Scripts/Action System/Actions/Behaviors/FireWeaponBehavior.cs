using UnityEngine;
using NaughtyAttributes;

//	Behavior for setting a firing mode immediately
[System.Serializable] public class FireWeaponBehavior : BaseAction{

	[Header("Firing")]
	//	Variable: Velocity to set
	[SerializeField, Dropdown("weaponDropdown")] private int weapon = 0;
	[SerializeField, Dropdown("modeDropdown")] private int mode = 0;

	/*	Variables:
	weaponDropdown: Dropdown for setting weapons
	modeDropdown: Dropdown for setting firing modes
	*/
	[SerializeField, HideInInspector] private DropdownList<int> weaponDropdown = new DropdownList<int>{{"INVALID", 0}};
	[SerializeField, HideInInspector] private DropdownList<int> modeDropdown = new DropdownList<int>{{"INVALID", 0}};


	//	Constructors
	public FireWeaponBehavior(){
		weapon = 0;
		mode = 0;
	}
	public FireWeaponBehavior(FireWeaponBehavior origin){
		weapon = origin.weapon;
		mode = origin.mode;
	}

	//	Overrides
	override public BaseAction clone(){
		return new FireWeaponBehavior(this);
	}
	override public void validate(GameObject actor){

		//	Variable: Weapon handler of `actor`
		EnemyWeaponHandler handler = actor.GetComponent<EnemyWeaponHandler>();

		//	Check `handler`
		if(handler == null){

			//	Set error call
			Debug.LogError("Fire weapon behavior cannot find a proper weapon handler");

			//	Set invalid dropdowns
			weaponDropdown = new DropdownList<int>{{"INVALID", 0}};
			modeDropdown = new DropdownList<int>{{"INVALID", 0}};

		}
		else{

			//	Set `weaponDropdown`
			weaponDropdown = handler.getWeaponsDropdown();

			//	Variable: Specific current weapon
			BaseEnemyWeapon curWeapon = handler.validateIndex(ref weapon);

			//	Check and set `modeDropdown`
			if(curWeapon == null) modeDropdown = new DropdownList<int>{{"INVALID", 0}};
			else modeDropdown = curWeapon.getModesDropdown();

		}
	}
	override protected void start(){
		instance.actor.fireWeapon(weapon, mode);
	}

}