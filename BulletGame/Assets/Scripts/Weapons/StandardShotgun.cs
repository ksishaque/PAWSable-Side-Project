using UnityEngine;

public class StandardShotgun : BaseWeapon{

	[Header("References")]
	//	Variable: Root from which the projectile fires
	[SerializeField] private GameObject barrel;

	[Header("Configuration")]
	/*	Variables:
	minInterval: Minimum attack interval
	baseProjPerShot: Standard number of projectiles per shot
	minProjPerShot: Minimum number of projectiles before excess mass begins affecting attack interval instead
	angleRange: Maximum angle deviation
	projPerShot: Number of projectiles in this shot
	*/
	[SerializeField] private float minInterval;
	[SerializeField] private int baseProjPerShot;
	[SerializeField] private int minProjPerShot;
	[SerializeField] private float angleRange;
	private int projPerShot;


	//	Set up
	override protected void onSetUp(){

		//	Variable: Flaot accurate number of projectiles in this shot
		float pps = baseProjPerShot / getProjectile(0).getScaledMass();

		//	If `pps` is too small, increase `attackInterval`
		if(pps < minProjPerShot){
			attackInterval = minInterval * minProjPerShot / pps;
			projPerShot = minProjPerShot;
		}

		//	Set `projPerShot`
		else{
			attackInterval = minInterval;
			projPerShot = (int) (pps + 0.5f);
		}

	}


	//	Fire
	override public void fireProjectile(){
		for(int i = 0; i < projPerShot; i += 1) getProjectile(0).spawnEntity(barrel.transform, Math.spread(i, projPerShot, -angleRange, angleRange, true));
	}

}