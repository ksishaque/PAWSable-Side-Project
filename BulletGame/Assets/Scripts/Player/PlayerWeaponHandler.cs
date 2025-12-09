using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour{

	[System.Serializable] public class WeaponSlot{

		[SerializeField] private GameObject root;
		public WeaponLoot weapon;
		public List<ProjectileLoot> projectiles;

		//	Accessors
		public Transform getRoot() => root.transform;

	}

	[Header("References")]
	//	Variable: Player input component to listen to
	[SerializeField] private PlayerInput input;
	private InputAction shoot;

	[Header("Weapons")]
	/*	Variables:
	weaponSlots: Available weapon slots
	currentWeapon: Currently active weapon slot
	*/
	[SerializeField] private List<WeaponSlot> weaponSlots;
	private List<BaseWeapon> activeWeapons = new List<BaseWeapon>();
	private int curWeapon = -1;


	//	Validation
	private void OnValidate(){

		//	Ensure each weapon has the correct amnount of projectiles slots
		foreach(WeaponSlot weaponSlot in weaponSlots) if(weaponSlot != null) {
			if(weaponSlot.weapon == null) weaponSlot.projectiles = null;
			else{
				if(weaponSlot.projectiles == null) weaponSlot.projectiles = new List<ProjectileLoot>();
				while(weaponSlot.projectiles.Count < weaponSlot.weapon.getProjCount()) weaponSlot.projectiles.Add(null);
				while(weaponSlot.projectiles.Count > weaponSlot.weapon.getProjCount()) weaponSlot.projectiles.RemoveAt(weaponSlot.projectiles.Count - 1);
			}
		}

		//	Check if `input` can be automatically set
		if(this.input == null) input = GetComponent<PlayerInput>();

	}


	//	Set up / clean up
	private void Start(){

		//	Find `shoot`
		shoot = input.actions.FindAction("Shoot");

		//	Set up callbacks
		input.actions.FindAction("SwitchRight").started += swapUp;
		input.actions.FindAction("SwitchLeft").started += swapDown;

	}

	private void OnDestroy(){

		//	Clean up callbacks
		input.actions.FindAction("SwitchRight").started -= swapUp;
		input.actions.FindAction("SwitchLeft").started -= swapDown;

	}


	//	Update for firing
	private void Update(){
		if(shoot.ReadValue<float>() > 0.5f) foreach(BaseWeapon weapon in activeWeapons) weapon.shoot();
	}


	//	Input callbacks
	private void swapUp(InputAction.CallbackContext cb){

		//	Put away current weapons
		deactivateAll();

		//	Variable: Previous weapon index, to prevent looping
		int preWeapon = curWeapon;

		//	Loop to find next weapon
		curWeapon += 1;
		if(curWeapon >= weaponSlots.Count) curWeapon = 0;
		while(curWeapon != preWeapon && weaponSlots[curWeapon] == null){
			curWeapon += 1;
			if(curWeapon >= weaponSlots.Count) curWeapon = 0;
		}

		//	Activate newly found weapon
		activate();

	}

	private void swapDown(InputAction.CallbackContext cb){

		//	Put away current weapons
		deactivateAll();

		//	Variable: Previous weapon index, to prevent looping
		int preWeapon = curWeapon;

		//	Loop to find next weapon
		curWeapon -= 1;
		if(curWeapon < 0) curWeapon = weaponSlots.Count - 1;
		while(curWeapon != preWeapon && weaponSlots[curWeapon] == null){
			curWeapon -= 1;
			if(curWeapon < 0) curWeapon = weaponSlots.Count - 1;
		}

		//	Activate newly found weapon
		activate();

	}


	//	Helpers
	private void deactivateAll(){
		foreach(BaseWeapon weapon in activeWeapons) Destroy(weapon.gameObject);
		activeWeapons.Clear();
	}

	public void activate(int weaponIndex){
		if(weaponSlots[weaponIndex].weapon != null) activeWeapons.Add(weaponSlots[weaponIndex].weapon.spawnEntity(weaponSlots[weaponIndex]));
	}

	private void activate(){
		activate(curWeapon);
	}

}
