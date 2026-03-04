using UnityEngine;

//	Class for handling player projectile piercing tags
public class ProjectilePierceHandler : MonoBehaviour{

	/*	Variables:
	projectile: Player projectile reference to get base values from
	full: Remaining number of enemies to pierce
	death: Remaining number of enemies to pierce on kill
	*/
	[SerializeField] private BasePlayerProjectile projectile;
	private int full, death;

	//	Set up
	private void Start(){
		full = projectile.getFullPierce();
		death = projectile.getDeathPierce();
	}

	//	Manage pierce
	public void onHit(Health health){

		//	Check for death pierce
		if(health.isDead() && death != 0) death -= 1;

		//	Check for expiration
		else if(full == 0) ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.PROJECTILE_EXPIRE);

		//	Increment `full`
		else full -= 1;

	}

}