using UnityEngine;
using NaughtyAttributes;

//	Scriptable object for debug projectiles
[CreateAssetMenu(fileName = "DualProjectileLoot", menuName = "Loot/Projectiles/Dual Projectiles")] public class DualProjectileLoot : ProjectileLoot{

	//	Variable: Opposing projectile to spawn
	[SerializeField, BoxGroup("References")] protected GameObject projectile2;

	override public void spawnEntity(Transform origin, float angleModifier, PlayerProjectile.Data data){
		spawnEntityInner(projectile, origin, angleModifier, data).GetComponent<SpriteRenderer>();
		spawnEntityInner(projectile2, origin, angleModifier, data).GetComponent<SpriteRenderer>();

	}

}
