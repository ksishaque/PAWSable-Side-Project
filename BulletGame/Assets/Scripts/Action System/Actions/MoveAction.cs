using UnityEngine;

//	Action for moving the actor
[System.Serializable] public class MoveAction : BaseTimedAction{

	[Header("Moving")]
	/*	Variables:
	position: Final local position of the actor
	init: Initial local position of the actor
	*/
	[SerializeField] private Vector2 position;
	private Vector2 init;

	//	Constructor
	public MoveAction(){
		position = new Vector2(0, 0);
	}
	public MoveAction(MoveAction origin) : base(origin){
		position = origin.position;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `position` to be the change in position
		init = actor.transform.localPosition;
		position -= init;

	}
	override protected void update(){
		actor.setPosition(init + (position * completion));
	}

}
