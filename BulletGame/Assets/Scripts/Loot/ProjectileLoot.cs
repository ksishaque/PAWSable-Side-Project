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
	public void spawnEntity(Transform origin, float angleModifier = 0){
		ObjectInitializer.instantiate(projectile, origin.position, angleModifier + origin.rotation.eulerAngles.z);
	/*/
	public BaseProjectile SpawnEntity(Transform origin, float angleModifier = 0){

		//	Variable: Spawned projectile component
		BaseProjectile ans = ObjectInitializer.instantiate(projectile, origin.position, angleModifier + origin.rotation.eulerAngles.z).GetComponent<BaseProjectile>();

		//	Return
		return ans;

	//*/
	}

	//	Accessors
	public int getMass() => mass;
	public float getScaledMass() => (Mathf.Pow(3, mass / 10.0f) / 18) + 0.5f;

}
