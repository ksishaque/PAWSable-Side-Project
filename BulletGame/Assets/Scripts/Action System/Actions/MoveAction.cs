using UnityEngine;

//	Action for moving the actor
[System.Serializable] public class MoveAction : BaseTimedAction{

	[Header("Moving")]
	/*	Variables:
	position: Final local position of the actor
	init: Initial local position of the actor
	relative: If `position` is relative to the current position
	*/
	[SerializeField] private Vector2 position;
	private Vector2 init;
	[SerializeField] private bool relative;

	//	Constructor
	public MoveAction(){
		position = new Vector2(0, 0);
		relative = false;
	}
	public MoveAction(Vector2 position, bool relative, float duration, BaseScalingFunction completionFunction) : base(duration, completionFunction){
		this.position = position;
		this.relative = relative;
	}
	public MoveAction(MoveAction origin) : base(origin){
		position = origin.position;
		relative = origin.relative;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `position` to be the change in position
		init = actor.transform.localPosition;
		if(relative == false) position -= init;

	}
	override protected void update(){
		actor.setLocalPosition(init + (position * completion));
	}

}
