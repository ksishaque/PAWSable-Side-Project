using UnityEngine;
using NaughtyAttributes;

//	Scriptable object for debug projectiles
[CreateAssetMenu(fileName = "DebugTestProjectileLoot", menuName = "Loot/Projectiles/Specific/Debug Projectile")] public class DebugTestProjectileLoot : ProjectileLootData{

	[SerializeField, BoxGroup("Data")] private Color color = new Color(1, 1, 1);
	[SerializeField, BoxGroup("Data")] private bool kill = false;

	override public void spawnEntity(Transform origin, float angleModifier, PlayerProjectile.Data data){

		//	Modify `data` for `kill`
		if(kill) data = new PlayerProjectile.Data(1000000, data.speed);

		//	Variable: Spawned projectile's graphics component
		SpriteRenderer sprite = spawnEntityInner(projectile, origin, angleModifier, data).GetComponent<SpriteRenderer>();

		//	Set up `color`
		sprite.color = color;

	}

}
