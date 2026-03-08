using UnityEngine;
using NaughtyAttributes;

public class GenericBlastWeapon : BasePlayerWeapon, IHasBlastSpread{

	[Header("References")]
	//	Variable: Root from which the projectiles fire
	[SerializeField] private GameObject barrel;

	[Header("Configuration")]
	/*	Variables:
	minInterval: Minimum attack interval
	baseProjPerShot: Standard number of projectiles per shot, assuming a mass of 30
	minProjPerShot: Minimum number of projectiles before excess mass begins affecting attack interval instead
	angleRange: Maximum angle deviation
	spreadEdgeBuffer: Number of potential projectile spawn rotations to use as a buffer before each edge
	spread: Index scaler for the bullet spread
	projPerShot: Number of projectiles in this shot
	*/
	[SerializeField, MinValue(0)] private float minInterval = 0.6f;
	[SerializeField, MinValue(0)] private int baseProjPerShot = 3;
	[SerializeField, MinValue(1)] private int minProjPerShot = 2;
	[SerializeField, MinValue(0)] private float angleRange = 30;
	[SerializeField, MinValue(0)] private float spreadEdgeBuffer = 1;
	private Spread spread;
	public int projPerShot{
		get;
		private set;
	}


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
		spread = new Spread(projPerShot, -angleRange, angleRange, spreadEdgeBuffer);

	}
	override protected void fireProjectile(){
		for(int i = 0; i < projPerShot; i += 1) getProjectile(0).spawnEntity(barrel.transform, spread.getValue(i));
	}
	override public int getProjCount() => 1;
	public Spread getBlastSpread() => spread;

}