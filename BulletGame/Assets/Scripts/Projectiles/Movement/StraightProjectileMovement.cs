using UnityEngine;

//	Class for constant speed, constant direction projectile movement
[RequireComponent(typeof(PhysicalRotation))] public class StraightProjectileMovement : MonoBehaviour{

	/*	Variables:
	velocity: Constant velocity to move at
	speedMod: Speed modifier to apply
	rotation: Physical rotation to use
	*/
	[SerializeField] private Vector2 velocity = new Vector2(1, 0);
	[SerializeReference, SubclassSelector] private BaseProjectileSpeedModifier speedMod;
	private PhysicalRotation rotation;

	//	Validation
	private void OnValidate(){
		if(speedMod == null) speedMod = BaseProjectileSpeedModifier.getDefault(gameObject);
	}

	//	Set up
	private void Start(){
		rotation = gameObject.GetComponent<PhysicalRotation>();
	}

	//	Update movement
	private void Update(){
		gameObject.addPosition((velocity * rotation.matrix) * speedMod.getSpeedModifier() * Time.deltaTime);
	}

}