using UnityEngine;

//	Scriptable object for debug projectiles
[CreateAssetMenu(fileName = "DebugTestProjectileLoot", menuName = "Loot/Projectiles/Debug Projectile")] public class DebugTestProjectileLoot : ProjectileLoot{

	[SerializeField] private Color color = new Color(1, 1, 1);
	[SerializeField] private bool kill = false;

	override public GameObject spawnEntity(Transform origin, float angleModifier, PlayerProjectile.Data data){

		//	Modify `data` for `kill`
		if(kill) data = new PlayerProjectile.Data(1000000, data.speed);

		//	Variable: Spawned projectile's graphics component
		SpriteRenderer sprite = base.spawnEntity(origin, angleModifier, data).GetComponent<SpriteRenderer>();

		//	Set up `color`
		sprite.color = color;

		//	Return
		return sprite.gameObject;

	}

}
