using UnityEngine;

//	Action for rotating the actor
[System.Serializable] public class RotateAction : BaseTimedAction{

	//	Direction in which the actor should rotate
	private enum Direction{STRAIGHT = 360, CLOCKWISE = -180, COUNTERCLOCKWISE = 180, FASTEST = 0};

	[Header("Rotating")]
	/*	Variables:
	rotation: Final local rotation of the actor
	direction: Direction in which the actor should rotate
	init: Initial local rotation of the actor
	physics: Physics component of the actor
	*/
	[SerializeField] private float rotation;
	[SerializeField] private Direction direction;
	private float init;
	private Rigidbody2D physics;

	//	Constructors
	public RotateAction(){
		rotation = 0;
		direction = Direction.FASTEST;
	}
	public RotateAction(RotateAction origin) : base(origin){
		rotation = origin.rotation;
		direction = origin.direction;
	}

	//	Overrides
	override public BaseAction clone(){
		return new RotateAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `rotation` to be the added rotation
		init = actor.transform.localRotation.eulerAngles.z;
		rotation -= init;

		//	Clamp `rotation` to the correct range
		if(direction != Direction.STRAIGHT){

			//	Clamp for too low or too high
			while(rotation < ((float) direction) - 180) rotation += 360;
			while(rotation > ((float) direction) + 180) rotation -= 360;

			//	Check for 360s
			if(rotation == 360 || rotation == -360) rotation = 0;

		}

		//	Attempt to find `physics`
		physics = actor.GetComponent<Rigidbody2D>();

	}
	override protected void update(){
		Physics.setLocalRotation(actor, physics, init + (rotation * completion));
	}

}
