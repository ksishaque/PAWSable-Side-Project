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

	[Header("Weapons")]
	/*	Variables:
	weaponSlots: Available weapon slots
	currentWeapon: Currently active weapon slot
	*/
	[SerializeField] private List<WeaponSlot> weaponSlots;
	private List<BaseWeapon> activeWeapons = new List<BaseWeapon>();
	private int curWeapon = -1;

	[Header("Inputs")]
	/*	Variables:
	shoot: Input action for shooting
	scroll: Interpreter for scrolling
	scrollTimeout: Time before `scroll` resets remaining scroll input
	*/
	private InputAction shoot;
	private ScrollWheelInterpreter scroll;
	[SerializeField] private float scrollTimeout;


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
		if(input == null) input = GetComponent<PlayerInput>();

	}


	//	Set up / clean up
	private void Start(){

		//	Find `shoot`
		shoot = input.actions.FindAction("Shoot");

		//	Set up `scroll`
		scroll = new ScrollWheelInterpreter(input);

		//	Set up callbacks
		input.actions.FindAction("ShiftRight").started += cb => swapUp();
		input.actions.FindAction("ShiftLeft").started += cb => swapDown();
		scroll.up += swapDown;
		scroll.down += swapUp;

	}

	private void OnDestroy(){

		//	Clean up callbacks
		input.actions.FindAction("ShiftRight").started -= cb => swapUp();
		input.actions.FindAction("ShiftLeft").started -= cb => swapDown();

	}


	//	Update
	private void Update(){

		//	Update firing
		if(shoot.ReadValue<float>() > 0.5f) foreach(BaseWeapon weapon in activeWeapons) weapon.shoot();

		//	Update `scroll`
		scroll.update();

	}


	//	Input callbacks
	private void swapUp(){

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

	private void swapDown(){

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
		Debug.Log("Activate gun: " + weaponIndex);
	}

	private void activate(){
		activate(curWeapon);
	}

}
