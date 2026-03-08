using UnityEngine;
using NaughtyAttributes;

//	Base class for all possible enemy weapons (because not all weapons are cannons)
public abstract class BaseEnemyWeapon : MonoBehaviour{

	//	Fire the weapon
	abstract public void fire(int mode = 0);

	//	Access inspector dropdown options for firing modes
	virtual public DropdownList<int> getModesDropdown() => new DropdownList<int>(){{"Fire", 0 }};

}

//	Base class for all possible enemy weapons that autofire
public abstract class BaseEnemyAutoWeapon : BaseEnemyWeapon{

	[Header("Auto Firing")]
	/*	Variables:
	attackInterval: Interval between each attack in automatic mode
	attackTimer: Time until the next attack
	firing: If the weapon is in automatic mode
	*/
	[SerializeField, MinValue(0)] private float attackInterval = 1;
	private float attackTimer = 0;
	//*
	[SerializeField] private bool firing = false;
	/*/
	[ShowNonSerializedField] private bool firing = false;
	//*/


	//	Firing
	virtual protected void Update(){

		//	Increment `attackTimer`
		if(attackTimer > 0) attackTimer -= Time.deltaTime;

		//	Fire if necessary
		if(firing){
			fireInstance();
			attackTimer = attackInterval;
		}

	}
	override public void fire(int mode = 0){

		//	Check `mode`
		switch(mode){

		//	Automatic mode
		case 1:
			firing = true;
			break;
		case 2:
			firing = false;
			break;

		//	Instant fire
		default:
			fireInstance();
			attackTimer = attackInterval;
			break;

		}

	}
	abstract protected void fireInstance();


	//	Accessor
	override public DropdownList<int> getModesDropdown() => new DropdownList<int>(){
		{"Fire Instant", 0},
		{"Start Firing", 1},
		{"Stop Firing", 2}
	};

}