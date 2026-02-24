using UnityEngine;
using NaughtyAttributes;

//	Standard scriptable object for projectiles
[CreateAssetMenu(fileName = "ProjectileLoot", menuName = "Loot/Projectiles/Standard Projectile")] public class ProjectileLoot : BaseLoot{

	/*	Variables:
	projectile: Projectile indicated by this loot
	desc: Description of the weapon
	mass: Mass value of the projectile
	*/
	[SerializeField, BoxGroup("References")] protected GameObject projectile;
	[SerializeField, BoxGroup("Visual"), ResizableTextArea] private string desc = "";
	[SerializeField, BoxGroup("Data")] private int mass = 30;

	//	Validation
	virtual protected void OnValidate(){
		if(projectile.GetComponent<PlayerProjectile>() == null) projectile = null;
	}

	//	Spawning
	public void spawnEntity(Transform origin) => spawnEntity(origin, 0, PlayerProjectile.STANDARD_DATA);
	public void spawnEntity(Transform origin, float angleModifier) => spawnEntity(origin, angleModifier, PlayerProjectile.STANDARD_DATA);
	public void spawnEntity(Transform origin, PlayerProjectile.Data data) => spawnEntity(origin, 0, data);
	virtual public void spawnEntity(Transform origin, float angleModifier, PlayerProjectile.Data data){
		spawnEntityInner(projectile, origin, angleModifier, data);
	}
	protected GameObject spawnEntityInner(GameObject prefab, Transform origin, float angleModifier, PlayerProjectile.Data data){

		/*	Variable: Spawned projectile object
		*/
		GameObject ans = ObjectInitializer.instantiate(prefab, origin.position, angleModifier + origin.rotation.eulerAngles.z);

		//	Set up `data`
		ans.GetComponent<PlayerProjectile>().setData(data);

		//	Return
		return ans;

	}


	//	Accessors
	public int getMass() => mass;
	//	General-purpose value denoting the effects of mass
	public float getScaledMass() => (Mathf.Pow(4, mass / 30.0f) / 8) + (mass / 120.0f) + 0.25f;
	//	Value denoting the converse effects of mass on speed, scaled such that a mass of 30 returns 1 and a mass of 60 returns 0
	public float getInertia() => (11 - (4 * getScaledMass())) / 7;

}