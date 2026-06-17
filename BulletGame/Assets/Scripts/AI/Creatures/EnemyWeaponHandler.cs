using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

//	Class that stores references to multiple enemy weapons
public class EnemyWeaponHandler : MonoBehaviour{

	//	Variable: References to all of the enemy's weapons
	[SerializeReference, SubclassSelector] private List<BaseEnemyWeapon> weapons = new List<BaseEnemyWeapon>();


	//	Set up and update `weapons`
	private void Update(){
		foreach(BaseEnemyWeapon weapon in weapons) weapon.update(Time.deltaTime);
	}


	//	Accessors
	public BaseEnemyWeapon getWeapon(int index){
		return weapons[index];
	}
	public BaseEnemyWeapon validateIndex(ref int index){

		//	Check `weapons`
		if(weapons.Count < 1) return null;

		//	Check for a valid index
		if(index < 0 || index >= weapons.Count){
			index = 0;
			return validateIndex(ref index);
		}

		//	Return
		return weapons[index];

	}
	public DropdownList<int> getWeaponsDropdown(){

		//	Variable: Return value / dropdown menu of weapon names
		DropdownList<int> ans = new DropdownList<int>();

		//	Copy each weapon's name
		for(int i = 0; i < weapons.Count; i += 1) ans.Add(weapons[i].getName(), i);

		//	Return
		return ans;

	}


	//	Set up previews
	public void fillPreviews(ref List<BaseEnemyWeapon> previewList, BasePreviewImage image){
		previewList.Clear();
		foreach(BaseEnemyWeapon weapon in weapons) previewList.Add(weapon.preview(image));
	}

}