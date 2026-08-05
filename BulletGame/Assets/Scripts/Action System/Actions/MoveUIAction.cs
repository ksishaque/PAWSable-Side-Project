using UnityEngine;

//	Action for moving the actor
[System.Serializable] public class MoveUIAction : BaseTimedAction{

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
	public MoveUIAction(){
		position = new Vector2(0, 0);
		relative = false;
	}
	public MoveUIAction(Vector2 position, bool relative, float duration, BaseScalingFunction completionFunction) : base(duration, completionFunction){
		this.position = position;
		this.relative = relative;
	}
	public MoveUIAction(MoveUIAction origin) : base(origin){
		position = origin.position;
		relative = origin.relative;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveUIAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `position` to be the change in position
		init = instance.actor.getPosition();
		if(relative == false) position -= init;

	}
	override protected void update(){
		instance.actor.setPosition(init + (position * completion));
	}

}
