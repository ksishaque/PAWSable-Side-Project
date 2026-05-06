using UnityEngine;

//	Action for moving the actor to a static target
//	NOTE: If used on a dynamic target, the actor will simply move to the position that the target was at at the start of the action.
[System.Serializable] public class MoveToStaticTargetAction : BaseTimedAction{

	[Header("Moving")]
	/*	Variables:
	target: Target to move to
	delPos: Total change in position
	init: Initial local position of the actor
	*/
	[SerializeReference, SubclassSelector] private BaseTarget target;
	private Vector2 delPos;
	private Vector2 init;

	//	Constructor
	public MoveToStaticTargetAction(){
		target = BaseTarget.getDefault();
	}
	public MoveToStaticTargetAction(MoveToStaticTargetAction origin) : base(origin){
		target = origin.target;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveToStaticTargetAction(this);
	}
	override protected void start(){

		//	Set up `init` and `delPos`
		init = instance.actor.getPosition();
		delPos = target;
		delPos -= init;

	}
	override protected void update(){
		instance.actor.setPosition(init + (delPos * completion));
	}

}
