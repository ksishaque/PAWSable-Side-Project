using System.Collections.Generic;
using UnityEngine;

//	Class that stores references to multiple enemy weapons
public class EnemyWeaponHandler : MonoBehaviour{

	//	Variable: References to all of the enemy's weapons
	[SerializeField] private List<BaseEnemyWeapon> weapons = new List<BaseEnemyWeapon>();


	//	Accessor
	public BaseEnemyWeapon getWeapon(int index){
		return weapons[index];
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