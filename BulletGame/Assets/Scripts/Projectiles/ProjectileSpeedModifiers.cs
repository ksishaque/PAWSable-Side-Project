using UnityEngine;

//	Accessor system for speed modifiers for different types of projectiles
[System.Serializable] abstract public class BaseProjectileSpeedModifier{

	//	Enemy accessor
	[System.Serializable] public class Enemy : BaseProjectileSpeedModifier{

		//	Override
		override public float getSpeedModifier() => 1;

	}

	//	Player accessor
	[System.Serializable] public class Player : BaseProjectileSpeedModifier{

		//	Variable: Player projectile component
		[SerializeField] private BasePlayerProjectile data;

		//	Constructors
		public Player(){}
		public Player(BasePlayerProjectile data){
			this.data = data;
		}

		//	Override
		override public float getSpeedModifier() => data.getSpeedModifier();

	}


	//	Accessor
	abstract public float getSpeedModifier();

	//	Helper for finding default modifier
	static public BaseProjectileSpeedModifier getDefault(GameObject obj){

			//	Variable: Player projectile component
			BasePlayerProjectile playerProj = obj.GetComponent<BasePlayerProjectile>();

			//	Determine best modifier type
			if(playerProj == null) return new Enemy();
			return new Player(playerProj);

	}

}