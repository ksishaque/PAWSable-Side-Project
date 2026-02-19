using UnityEngine;

//	Class for constant speed, constant direction projectile movement
public class ConstantProjectileMovment : MonoBehaviour, IStoreRotation{

	/*	Variables:
	velocity: Constant velocity to move at
	speedMod: Speed modifier to apply
	rotation: Rotation matrix to apply
	*/
	[SerializeField] private Vector2 velocity = new Vector2(1, 0);
	[SerializeReference, SubclassSelector] private BaseProjectileSpeedModifier speedMod;
	private RotationMatrix rotation;

	//	Validation
	private void OnValidate(){

		//	Set up default speed modifier
		if(speedMod == null){

			//	Variable: Player projectile component
			PlayerProjectile playerProj = gameObject.GetComponent<PlayerProjectile>();

			//	Determine best modifier type
			if(playerProj == null) speedMod = new BaseProjectileSpeedModifier.Enemy();
			else speedMod = new BaseProjectileSpeedModifier.Player(playerProj);

		}

	}

	//	Set up
	public void storeRotation(){
		rotation = new RotationMatrix(transform);
	}

	//	Update movement
	private void Update(){
		gameObject.addPosition((velocity * rotation) * speedMod.getSpeedModifier() * Time.deltaTime);
	}

}