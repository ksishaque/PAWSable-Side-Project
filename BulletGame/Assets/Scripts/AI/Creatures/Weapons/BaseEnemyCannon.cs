using UnityEngine;

//	Class for typical enemy cannons
public partial class EnemyCannon : BaseEnemyAutoWeapon{

	[Header("References")]
	/*	Variables:
	barrel: Root from which the main projectile fires
	projectile: Projectile to fire
	*/
	[SerializeField] private GameObject barrel;
	[SerializeField] private GameObject projectile;


	//	Firing
	override protected void fireInstance() => fireInstance(0);
	private void fireInstance(float angleModifier){

		//	Spawn `projectile`
		spawnEntity(angleModifier);

	}
	private void spawnEntity(float angleModifier){

		//	Variable: Spawned projectile object
		GameObject ans = ObjectInitializer.instantiate(projectile, barrel.transform.position, angleModifier + barrel.transform.rotation.eulerAngles.z);

	}

}