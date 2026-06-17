using UnityEngine;

//	Class for typical enemy cannons
[System.Serializable] public partial class EnemyCannon : BaseEnemyAutoWeapon{

	//	Preview class
	private class Preview : EnemyCannon{

		//	Variable: Actor to reference for the preview class's position
		BasePreviewImage image;


		//	Constructor
		public Preview(EnemyCannon baseCannon, BasePreviewImage image) : base(baseCannon){
			this.image = image;
		}


		//	Preview spawning
		override protected void spawnEntity(float angleModifier){

			//	Variable: Angle to fire at
			float angle = angleModifier + angleOffset;

			//	Determine `angle`
			if(target != null) angle += Math.angleTo(target.getLocation(), image.getPosition());

			//	Create the projectile
			new BehaviorListPreviewImage(projectile.GetComponent<ComplexProjectileMovement>(), image.getPosition(), angle);

		}

	}

	[Header("References")]
	/*	Variables:
	barrel: Root from which the main projectile fires
	projectile: Projectile to fire
	target: Target to aim at, will null representing straight left
	angleOffset: Offset angle from the target
	*/
	[SerializeField] private GameObject barrel;
	[SerializeField] private GameObject projectile;
	[SerializeReference, SubclassSelector] private BaseTarget target;
	[SerializeField] private float angleOffset;


	//	Constructor
	public EnemyCannon(){
		barrel = null;
		projectile = null;
		target = new BaseTarget.ObjectReference();
		angleOffset = 0;
	}
	protected EnemyCannon(EnemyCannon source) : base(source){
		barrel = source.barrel;
		projectile = source.projectile;
		target = source.target;
		angleOffset = source.angleOffset;
	}


	//	Firing
	override protected void fireInstance() => fireInstance(0);
	private void fireInstance(float angleModifier) => spawnEntity(angleModifier);
	virtual protected void spawnEntity(float angleModifier){

		//	Variable: Angle to fire at
		float angle = angleModifier + angleOffset;

		//	Determine `angle`
		if(target != null) angle += Math.angleTo(target.getLocation(), barrel.transform.position);

		//	Create the projectile
		ObjectInitializer.instantiate(projectile, barrel.transform.position, angle);

	}


	//	Preview
	override public BaseEnemyWeapon preview(BasePreviewImage image){
		return new Preview(this, image);
	}

}