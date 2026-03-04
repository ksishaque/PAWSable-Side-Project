using UnityEngine;
using NaughtyAttributes;

//	Scriptable object for debug projectiles
[CreateAssetMenu(fileName = "DebugTestProjectileLoot", menuName = "Loot/Projectiles/Specific/Debug Projectile")] public class DebugTestProjectileLoot : ProjectileLootData{

	[SerializeField, BoxGroup("Data")] private Color color = new Color(1, 1, 1);

	override public void spawnEntity(Transform origin, float angleModifier, PlayerProjectile.Data data){

		//	Variable: Spawned projectile's graphics component
		SpriteRenderer sprite = spawnEntityInner(projectile, origin, angleModifier, data).GetComponent<SpriteRenderer>();

		//	Set up `color`
		sprite.color = color;

	}

}
