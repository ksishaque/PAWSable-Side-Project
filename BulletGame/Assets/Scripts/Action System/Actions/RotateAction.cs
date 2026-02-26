using UnityEngine;

//	Action for rotating the actor
[System.Serializable] public class RotateAction : BaseTimedAction{

	//	Direction in which the actor should rotate
	public enum Direction{STRAIGHT = 360, CLOCKWISE = -180, COUNTERCLOCKWISE = 180, FASTEST = 0};

	[Header("Rotating")]
	/*	Variables:
	rotation: Final local rotation of the actor
	init: Initial local rotation of the actor
	direction: Direction in which the actor should rotate
	relative: If `rotation` is relative to the current rotation
	*/
	[SerializeField] private float rotation;
	private float init;
	[SerializeField] private Direction direction;
	[SerializeField] private bool relative;

	//	Constructors
	public RotateAction(){
		rotation = 0;
		direction = Direction.FASTEST;
		relative = false;
	}
	public RotateAction(float rotation, Direction direction, bool relative, float duration, BaseScalingFunction completionFunction) : base(duration, completionFunction){
		this.rotation = rotation;
		this.direction = direction;
		this.relative = relative;
	}
	public RotateAction(RotateAction origin) : base(origin){
		rotation = origin.rotation;
		direction = origin.direction;
		relative = origin.relative;
	}

	//	Overrides
	override public BaseAction clone(){
		return new RotateAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `rotation` to be the change in rotation
		init = actor.transform.localRotation.eulerAngles.z;
		if(relative == false) rotation -= init;

		//	Clamp `rotation` to the correct range
		if(direction != Direction.STRAIGHT){

			//	Clamp for too low or too high
			while(rotation < ((float) direction) - 180) rotation += 360;
			while(rotation > ((float) direction) + 180) rotation -= 360;

			//	Check for 360s
			if(rotation == 360 || rotation == -360) rotation = 0;

		}

	}
	override protected void update(){
		actor.setLocalRotation(init + (rotation * completion));
	}

}
