using UnityEngine;

//	Action for moving the actor to a dynamic target
[System.Serializable] public class MoveToTargetAction : BaseTimedAction{

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
	public MoveToTargetAction(){
		target = BaseTarget.getDefault();
	}
	public MoveToTargetAction(MoveToTargetAction origin) : base(origin){
		target = origin.target;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveToTargetAction(this);
	}
	override protected void start(){

		//	Set up `init` and `delPos`
		init = actor.transform.localPosition;
		delPos = target.getLocation();
		delPos -= init;

	}
	override protected void update(){
		actor.setPosition(init + (delPos * completion));
	}

}
