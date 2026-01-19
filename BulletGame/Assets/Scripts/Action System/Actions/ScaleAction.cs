using UnityEngine;

//	Action for scaling the actor
[System.Serializable] public class ScaleAction : BaseTimedAction{

	[Header("Scaling")]
	/*	Variables:
	scale: Final local scale of the actor
	init: Initial local scale of the actor
	*/
	[SerializeField] private Vector2 scale;
	private Vector2 init;

	//	Constructor
	public ScaleAction(){
		scale = new Vector2(1, 1);
	}
	public ScaleAction(ScaleAction origin) : base(origin){
		scale = origin.scale;
	}

	//	Overrides
	override public BaseAction clone(){
		return new ScaleAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `rotation` to be the added rotation
		init = actor.transform.localScale;
		scale -= init;

	}
	override protected void update(){
		actor.transform.localScale = new Vector3(init.x + (scale.x * completion), init.y + (scale.y * completion), 1);
	}

}
