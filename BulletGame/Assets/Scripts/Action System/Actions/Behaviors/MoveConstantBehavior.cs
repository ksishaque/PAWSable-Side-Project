using UnityEngine;

//	Behavior in which the actor moves at a constant velocity
[System.Serializable] public class MoveConstantBehavior : BaseMovementBehavior{

	[Header("Movement")]
	//	Variable: Velocity to set
	[SerializeReference, SubclassSelector] private InspectorVector2 velocity;

	//	Constructors
	public MoveConstantBehavior(){
		velocity = InspectorVector2.getDefault();
	}
	public MoveConstantBehavior(MoveConstantBehavior origin) : base(origin){
		velocity = origin.velocity;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveConstantBehavior(this);
	}
	override protected Vector2 getVelocity(float dt) => velocity.get();
	override protected Vector2 getFinalPosition(float duration, float dt){
		return origin + (duration * velocity.get());
	}

}