using UnityEngine;

public class StandardMachineGun : BaseWeapon{

	[Header("References")]
	/*	Variables:
	barrel1: Root from which one of the projectiles fires
	barrel2: Root from which the other projectile fires
	*/
	[SerializeField] private GameObject barrel1;
	[SerializeField] private GameObject barrel2;

	[Header("Configuration")]
	/*	Variables:
	baseInterval: Standard attack interval, assuming a mass of 20
	*/
	[SerializeField] private float baseInterval;


	//	Set up
	override protected void onSetUp(){
		attackInterval = getProjectile(0).getScaledMass() * baseInterval;
	}


	//	Fire
	override protected void fireProjectile(){
		getProjectile(0).spawnEntity(barrel1.transform);
		getProjectile(0).spawnEntity(barrel2.transform);
	}

}