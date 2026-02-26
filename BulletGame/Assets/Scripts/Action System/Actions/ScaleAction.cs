using UnityEngine;

//	Action for scaling the actor
[System.Serializable] public class ScaleAction : BaseTimedAction{

	[Header("Scaling")]
	/*	Variables:
	scale: Final local scale of the actor
	init: Initial local scale of the actor
	relative: If `scale` is relative to the current scale
	*/
	[SerializeField] private Vector2 scale;
	private Vector2 init;
	[SerializeField] private bool relative;

	//	Constructor
	public ScaleAction(){
		scale = new Vector2(1, 1);
		relative = false;
	}
	public ScaleAction(Vector2 scale, bool relative, float duration, BaseScalingFunction completionFunction) : base(duration, completionFunction){
		this.scale = scale;
		this.relative = relative;
	}
	public ScaleAction(ScaleAction origin) : base(origin){
		scale = origin.scale;
		relative = origin.relative;
	}

	//	Overrides
	override public BaseAction clone(){
		return new ScaleAction(this);
	}
	override protected void start(){

		//	Set up `init` and reconfigure `scale` to be the change in scale
		init = actor.transform.localScale;
		if(relative == false) scale -= init;

	}
	override protected void update(){
		actor.setLocalScale(init + (scale * completion));
	}

}
