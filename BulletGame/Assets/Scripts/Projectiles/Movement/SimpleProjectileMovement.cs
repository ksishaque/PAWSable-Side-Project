using UnityEngine;

//	Class for single behavior projectile movement
[RequireComponent(typeof(PhysicalRotation)), RequireComponent(typeof(Rigidbody2D))]  public class SimpleProjectileMovement : MonoBehaviour{

	[Header("Projectile")]
	/*	Variables:
	speedMod: Speed modifier to apply
	rotation: Rotation matrix to apply
	*/
	[SerializeReference, SubclassSelector] private BaseProjectileSpeedModifier speedMod;
	private RotationMatrix rotation;

	[Header("Behavior")]
	/*	Variables:
	behavior: Movement behavior to follow
	endMode: Type of ending to use
	*/
	[SerializeReference, SubclassSelector] private BaseMovementBehavior behavior;
	[SerializeField] private AIBehaviorList.EndMode endMode = AIBehaviorList.EndMode.ENDLESS;
	private bool running = false;

	//	Validation
	private void OnValidate(){
		if(speedMod == null) speedMod = BaseProjectileSpeedModifier.getDefault(gameObject);
	}

	//	Update movement
	private void Start(){

		//	Check `endMode` and start `behavior`
		if(endMode == AIBehaviorList.EndMode.ENDLESS) behavior.setEndless();
		behavior.initialize(gameObject, null);

		//	Set `running` to ensure first update runs after initialization
		running = true;

	}
	private void Update(){

		//	Check `running`
		if(running == true){

			//	Variable: Duration of the current frame
			float dt = Time.deltaTime;

			//	Add projectile speed effects
			dt *= speedMod.getSpeedModifier();
dt *= 0.25f;

			//	Run `behavior`
			behavior.update(ref dt);

			//	Manage finishing `behavior`
			if(dt >= 0){
				running = false;
				if(endMode == AIBehaviorList.EndMode.DESPAWN) ObjectDestroyer.destroy(gameObject, ObjectDestroyer.Cause.DESPAWN);
			}

		}

	}

}