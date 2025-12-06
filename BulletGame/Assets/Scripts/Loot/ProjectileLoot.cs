using UnityEngine;

//	Scriptable object for projectiles
[CreateAssetMenu(fileName = "ProjectileLoot", menuName = "Loot/Projectile")]
public class ProjectileLoot : BaseLoot{

	/*	Variables:
	projectile: Projectile indicated by this loot
	desc: Description of the weapon
	mass: Mass value of the projectile
	*/
	[SerializeField] private GameObject projectile;
	[SerializeField] private string desc = "";
	[SerializeField] private int mass = 20;

	//	Validation
	private void OnValidate(){
		//if(projectile.GetComponent<BaseProjectile>() == null) projectile = null;
	}

	//	Spawning
	//*
	public void SpawnEntity(Transform origin, float angleModifier, float speedModifier){
	/*/
	public BaseProjectile SpawnEntity(Transform origin, float angleModifier, float speedModifier){
	//*/

		//	Variable: Spawned weapon component
		BaseStandardWeapon ans = GameObject.Instantiate(projectile, origin.position, Quaternion.Euler(0, 0, angleModifier)).GetComponent<BaseStandardWeapon>();

	}

	//	Accessors
	public int GetMass() => mass;

}
