using UnityEngine;

//	Action for rotating the actor
[System.Serializable] public class MultiRotationAction : BaseTimedAction{

	//	Direction in which the actor should rotate
	public enum Direction{CLOCKWISE = -180, COUNTERCLOCKWISE = 180};

	[Header("Rotating")]
	/*	Variables:
	rotation: Total number of full rotations to run
	rotation: Final rotation to end on
	init: Initial local rotation of the actor
	direction: Direction in which the actor should rotate
	relative: If `finalRotation` is relative to the current finalRotation
	*/
	[SerializeField] private int rotationCount;
	[SerializeField] private float finalRotation;
	private float init;
	[SerializeField] private Direction direction;
	[SerializeField] private bool relative = false;

	//	Constructors
	public MultiRotationAction(){
		rotationCount = 0;
		direction = Direction.CLOCKWISE;
	}
	public MultiRotationAction(MultiRotationAction origin) : base(origin){
		rotationCount = origin.rotationCount;
		direction = origin.direction;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MultiRotationAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `rotation` to be the change in rotation
		init = actor.transform.localRotation.eulerAngles.z;
		if(relative == false) finalRotation -= init;

		//	Clamp `finalRotation` to the correct range
		while(finalRotation < ((float) direction) - 180) finalRotation += 360;
		while(finalRotation > ((float) direction) + 180) finalRotation -= 360;
		if(finalRotation == 360 || finalRotation == -360) finalRotation = 0;

		//	Add `rotationCount` to `finalRotation`
		finalRotation += ((float) direction) * rotationCount * 2;

	}
	override protected void update(){
		actor.setLocalRotation(init + (finalRotation * completion));
	}

}
