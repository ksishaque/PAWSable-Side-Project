using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour{

	[System.Serializable] public class WeaponSlot{

		[SerializeField] private GameObject root;
		public StandardWeaponLoot weapon;
		public List<ProjectileLoot> projectiles;

		//	Accessors
		public Transform GetRoot() => root.transform;

	}

	/*	Variables:
	weaponSlots: Available weapon slots
	currentWeapon: Currently active weapon slot
	*/
	[SerializeField] private List<WeaponSlot> weaponSlots;
	private int currentWeapon = 0;

	//	Validation
	private void OnValidate(){

		//	Ensure each weapon has the correct amnount of projectiles slots
		foreach(WeaponSlot weaponSlot in weaponSlots) if(weaponSlot != null && weaponSlot.weapon != null){
			if(weaponSlot.projectiles == null) weaponSlot.projectiles = new List<ProjectileLoot>();
			while(weaponSlot.projectiles.Count < weaponSlot.weapon.GetProjCount()) weaponSlot.projectiles.Add(null);
			while(weaponSlot.projectiles.Count > weaponSlot.weapon.GetProjCount()) weaponSlot.projectiles.RemoveAt(weaponSlot.projectiles.Count - 1);
			;
		}
	}

}
