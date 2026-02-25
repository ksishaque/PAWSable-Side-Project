using UnityEngine;

public class GenericBlastWeapon : BaseWeapon{

	[Header("References")]
	//	Variable: Root from which the projectile fires
	[SerializeField] private GameObject barrel;

	[Header("Configuration")]
	/*	Variables:
	minInterval: Minimum attack interval
	baseProjPerShot: Standard number of projectiles per shot, assuming a mass of 30
	minProjPerShot: Minimum number of projectiles before excess mass begins affecting attack interval instead
	angleRange: Maximum angle deviation
	spread: Index scaler for the bullet spread
	projPerShot: Number of projectiles in this shot
	*/
	[SerializeField] private float minInterval;
	[SerializeField] private int baseProjPerShot;
	[SerializeField] private int minProjPerShot;
	[SerializeField] private float angleRange;
	private Spread spread;
	private int projPerShot;


	//	Overrides
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

		//	Set up `spread`
		spread = new Spread(projPerShot, -angleRange, angleRange, 1);

	}
	override protected void fireProjectile(){
		for(int i = 0; i < projPerShot; i += 1) getProjectile(0).spawnEntity(barrel.transform, spread.getValue(i));
	}
	override public int getProjCount() => 1;

}