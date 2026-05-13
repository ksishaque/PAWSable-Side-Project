using UnityEngine;

//	Class for typical enemy cannons
[System.Serializable] public partial class EnemyCannon : BaseEnemyAutoWeapon{

	[Header("References")]
	/*	Variables:
	barrel: Root from which the main projectile fires
	projectile: Projectile to fire
	target: Target to aim at, will null representing straight left
	angleOffset: Offset angle from the target
	*/
	[SerializeField] private GameObject barrel;
	[SerializeField] private GameObject projectile;
	[SerializeReference, SubclassSelector] private BaseTarget target = new BaseTarget.ObjectReference();
	[SerializeField] private float angleOffset;


	//	Firing
	override protected void fireInstance() => fireInstance(0);
	private void fireInstance(float angleModifier){

		//	Spawn `projectile`
		spawnEntity(angleModifier);

	}
	private void spawnEntity(float angleModifier){
		if(target == null) ObjectInitializer.instantiate(projectile, barrel.transform.position, angleModifier + angleOffset);
		else ObjectInitializer.instantiate(projectile, barrel.transform.position, angleModifier + angleOffset);
	}

}