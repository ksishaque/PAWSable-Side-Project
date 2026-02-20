using UnityEngine;

//	Standard scriptable object for projectiles
[CreateAssetMenu(fileName = "ProjectileLoot", menuName = "Loot/Projectiles/Standard Projectile")] public class ProjectileLoot : BaseLoot{

	/*	Variables:
	projectile: Projectile indicated by this loot
	desc: Description of the weapon
	mass: Mass value of the projectile
	*/
	[SerializeField] protected GameObject projectile;
	[SerializeField] private string desc = "";
	[SerializeField] private int mass = 20;

	//	Validation
	virtual protected void OnValidate(){
		if(projectile.GetComponent<PlayerProjectile>() == null) projectile = null;
	}

	//	Spawning
	public GameObject spawnEntity(Transform origin) => spawnEntity(origin, 0, PlayerProjectile.STANDARD_DATA);
	public GameObject spawnEntity(Transform origin, float angleModifier) => spawnEntity(origin, angleModifier, PlayerProjectile.STANDARD_DATA);
	public GameObject spawnEntity(Transform origin, PlayerProjectile.Data data) => spawnEntity(origin, 0, data);
	virtual public GameObject spawnEntity(Transform origin, float angleModifier, PlayerProjectile.Data data){

		//	Variable: Spawned projectile component
		PlayerProjectile projData = ObjectInitializer.instantiate(projectile, origin.position, angleModifier + origin.rotation.eulerAngles.z).GetComponent<PlayerProjectile>();

		//	Set up `data`
		projData.setData(data);

		//	Return
		return projData.gameObject;

	}


	//	Accessors
	public int getMass() => mass;
	//	General-purpose value denoting the effects of mass
	public float getScaledMass() => (Mathf.Pow(4, mass / 30.0f) / 8) + (mass / 120.0f) + 0.25f;
	//	Value denoting the converse effects of mass on speed, scaled such that a mass of 30 returns 1 and a mass of 60 returns 0
	public float getInertia() => (11 - (4 * getScaledMass())) / 7;

}