using UnityEngine;
using NaughtyAttributes;

//	Base class for all possible enemy weapons (because not all weapons are cannons)
[System.Serializable] public abstract class BaseEnemyWeapon{

	//	Variable: Name of the weapon
	[SerializeField] private string name;
	protected BaseAction.IActor actor{
		get;
		private set;
	}


	//	Set up
	public void bindActor(BaseAction.IActor actor){
		this.actor = actor;
	}


	//	Fire and maintain the weapon
	abstract public void fire(int mode = 0);

	virtual public void update(float dt){}


	//	Accessors
	public string getName() => name;

	virtual public DropdownList<int> getModesDropdown() => new DropdownList<int>(){{"Fire", 0}};

}

//	Base class for all possible enemy weapons that autofire
[System.Serializable] public abstract class BaseEnemyAutoWeapon : BaseEnemyWeapon{

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
	override public void update(float dt){

		//	Increment `attackTimer`
		if(attackTimer > 0) attackTimer -= dt;

		//	Fire if necessary
		else if(firing){
			fireInstance();
			attackTimer = attackInterval;
		}

	}
	override public void fire(int mode = 0){

		//	Check `mode`
		switch(mode){

		//	Automatic mode
		case 0:
			firing = true;
			break;
		case 1:
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
		{"Fire Instant", -1},
		{"Start Firing", 0},
		{"Stop Firing", 1}
	};

}