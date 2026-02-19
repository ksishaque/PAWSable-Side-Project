using System.Collections.Generic;
using UnityEngine;

public class StandardIssueMachineGun : BaseWeapon{

	[Header("References")]
	/*	Variables:
	barrels: List of roots from which the projectiles can fire
	nextBarrel: Index of the next barrel to fire
	*/
	[SerializeField] private List<GameObject> barrels;
	private int nextBarrel = 0;

	[Header("Configuration")]
	/*	Variables:
	intensity: Intensity of bullet to fire
	baseInterval: Standard attack interval, assuming a mass of 30
	*/
	[SerializeField] private float intensity;
	[SerializeField] private float baseInterval;


	//	Overrides
	override protected void onSetUp(){
		attackInterval = getProjectile(0).getScaledMass() * baseInterval;
	}
	override protected void fireProjectile(){

		//	Fire the projectile
		getProjectile(0).spawnEntity(barrels[nextBarrel].transform, new PlayerProjectile.Data(intensity));

		//	Cycle through `barrels`
		nextBarrel += 1;
		if(nextBarrel >= barrels.Count) nextBarrel -= barrels.Count;

	}

}