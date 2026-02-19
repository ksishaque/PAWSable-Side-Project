using UnityEngine;

//	Accessor system for speed modifiers for different types of projectiles
[System.Serializable] abstract public class BaseDamageModifier{

	//	Enemy accessor
	[System.Serializable] public class Enemy : BaseDamageModifier{

		//	Override
		override public float getDamageModifier() => 1;

	}

	//	Player accessor
	[System.Serializable] public class PlayerProjectile : BaseDamageModifier{

		//	Variable: Player projectile component
		[SerializeField] private global::PlayerProjectile data;

		//	Constructors
		public PlayerProjectile(){}
		public PlayerProjectile(global::PlayerProjectile data){
			this.data = data;
		}

		//	Override
		override public float getDamageModifier() => data.getDamageModifier();

	}


	//	Accessor
	abstract public float getDamageModifier();

}