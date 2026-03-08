using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GenericMultiBarrelAssaultWeapon : BasePlayerWeapon{

	[Header("References")]
	/*	Variables:
	barrels: List of roots from which the projectiles can fire
	timers: List of cooldown timers
	*/
	[SerializeField] private List<GameObject> barrels = new List<GameObject>();
	private float[] barrelTimers;

	[Header("Configuration")]
	/*	Variables:
	baseInterval: Standard attack interval, assuming a mass of 30
	barrelTimerScale: Number of weapon intervals it takes for a barrel to cool down
	activeBarrelCount: Number of barrels ready to fire
	*/
	[SerializeField, MinValue(0)] private float baseInterval = 0.2f;
	[SerializeField, MinValue(0)] private int barrelTimerScale = 0;
	private int activeBarrelCount;


	//	Overrides
	override public int getProjCount() => 1;
	override protected void onSetUp(){

		//	Set up `activeBulletCount` and `timers`
		activeBarrelCount = barrels.Count;
		barrelTimers = new float[barrels.Count];
		for(int i = 0; i < activeBarrelCount; i += 1) barrelTimers[i] = 0;

		//	Set up intervals
		attackInterval = getProjectile(0).getScaledMass() * baseInterval;
		baseInterval = barrelTimerScale;
		baseInterval += 0.5f;
		baseInterval *= attackInterval;

	}
	override protected void onUpdate(){

		//	Update each timer
		for(int i = 0; i < barrels.Count; i += 1) if(barrelTimers[i] > 0){

			//	Increment
			barrelTimers[i] -= Time.deltaTime;

			//	Update `activeBarrelCount` when ready
			if(barrelTimers[i] <= 0) activeBarrelCount += 1;

		}

	}
	override protected void fireProjectile(){

		//	Check `activeBarrelCount`
		if(activeBarrelCount > 0){

			//	Variable: Index of barrel to fire
			int current = Random.Range(0, activeBarrelCount);

			//	Finish calculating `current`
			for(int i = 0; i <= current; i += 1) if(barrelTimers[i] > 0) current += 1;

			//	Fire the projectile
			getProjectile(0).spawnEntity(barrels[current].transform);
			barrelTimers[current] = baseInterval;
			activeBarrelCount -= 1;

		}

	}

}