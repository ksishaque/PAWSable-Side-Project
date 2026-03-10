using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

//	Class that stores references to multiple enemy weapons
public class EnemyWeaponHandler : MonoBehaviour{

	//	Variable: References to all of the enemy's weapons
	[SerializeField] private List<BaseEnemyWeapon> weapons = new List<BaseEnemyWeapon>();


	//	Accessors
	public BaseEnemyWeapon getWeapon(int index){
		return weapons[index];
	}
	public BaseEnemyWeapon getWeaponSafe(ref int index){

		//	Check `weapons`
		if(weapons.Count < 1) return null;

		//	Check for a valid index
		if(index < 0 || index >= weapons.Count){
			index = 0;
			return getWeaponSafe(ref index);
		}

		//	Return
		return weapons[index];

	}
	public DropdownList<int> getWeaponsDropdown(){

		//	Variable: Return value / dropdown menu of weapon names
		DropdownList<int> ans = new DropdownList<int>();

		//	Copy each weapon's name
		for(int i = 0; i < weapons.Count; i += 1) ans.Add(weapons[i].name, i);

		//	Return
		return ans;

	}


	//	Automatic weapon finder
	[NaughtyAttributes.Button("Automatically find all Weapons")] private void findWeapons(){

		//	Clear `weapons`
		weapons.Clear();

		//	Variable: `weapons` as an array
		BaseEnemyWeapon[] weaArr = GetComponentsInChildren<BaseEnemyWeapon>();

		//	Copy `weaArr` to `weapons`
		for(int i = 0; i < weaArr.Length; i += 1) weapons.Add(weaArr[i]);

	}

}