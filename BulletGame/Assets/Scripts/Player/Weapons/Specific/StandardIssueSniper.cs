using UnityEngine;
using NaughtyAttributes;

//	Class for Standard Issue Sniper
public class StandardIssueSniper : BasePlayerWeapon{

	[Header("References")]
	//	Variable: Root from which the projectile fires
	[SerializeField] private GameObject barrel;

	[Header("Configuration")]
	/*	Variables:
	interval: Attack interval
	intensity: Intensity of bullet to fire
	baseProjSpeed: Standard speed of projectile, assuming a mass of 30
	maxProjSpeed: Maximum speed of projectile, assuming 0 mass
	projData: projectile data to use
	*/
	[SerializeField, MinValue(0)] private float interval;
	[SerializeField, MinValue(1)] private float intensity;
	[SerializeField, MinValue(1)] private float baseProjSpeed;
	[SerializeField, MinValue(1)] private float minProjSpeed;
	private PlayerProjectile.Data projData;


	//	Overrides
	override protected void onSetUp(){

		//	Send `interval`
		attackInterval = interval;

		//	Set up `projData`
		projData = new PlayerProjectile.Data(intensity, Mathf.Lerp(minProjSpeed, baseProjSpeed, getProjectile(0).getInertia()));

	}
	override protected void fireProjectile(){
		getProjectile(0).spawnEntity(barrel.transform, projData);
	}
	override public int getProjCount() => 1;

}