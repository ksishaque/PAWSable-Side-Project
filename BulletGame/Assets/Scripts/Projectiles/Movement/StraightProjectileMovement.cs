using UnityEngine;

//	Class for constant speed, constant direction projectile movement
[RequireComponent(typeof(PhysicalRotation)), RequireComponent(typeof(Rigidbody2D))] public class StraightProjectileMovement : MonoBehaviour{

	/*	Variables:
	velocity: Constant velocity to move at
	speedMod: Speed modifier to apply
	rotation: Physical rotation to use
	physics: Physics component to use
	*/
	[SerializeField] private Vector2 velocity = new Vector2(1, 0);
	[SerializeReference, SubclassSelector] private BaseProjectileSpeedModifier speedMod;
	private PhysicalRotation rotation;
	private Rigidbody2D physics;

	//	Validation
	private void OnValidate(){
		if(speedMod == null) speedMod = BaseProjectileSpeedModifier.getDefault(gameObject);
	}

	//	Set up
	private void Start(){
		rotation = GetComponent<PhysicalRotation>();
		physics = GetComponent<Rigidbody2D>();
	}

	//	Update movement
	private void Update(){
		physics.linearVelocity = (velocity * rotation.matrix) * speedMod.getSpeedModifier();
	}

}