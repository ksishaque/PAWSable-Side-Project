using UnityEngine;

//	Action for recoloring a sprite renderer
[System.Serializable] public class RecolorAction : BaseTimedAction{

	[Header("Coloring")]
	/*	Variables:
	renderer: Sprite renderer component of the actor
	scale: Final color of `renderer`
	init: Initial color of `renderer`
	*/
	[SerializeField] private SpriteRenderer renderer;
	[SerializeField] private Color color;
	private Color init;

	//	Constructor
	public RecolorAction(){
		renderer = null;
		color = new Color(1, 1, 1, 1);
	}
	public RecolorAction(SpriteRenderer renderer, Color color, float duration, BaseScalingFunction completionFunction) : base(duration, completionFunction){
		this.renderer = renderer;
		this.color = color;
	}
	public RecolorAction(RecolorAction origin) : base(origin){
		renderer = origin.renderer;
		color = origin.color;
	}

	//	Overrides
	override public BaseAction clone(){
		return new RecolorAction(this);
	}
	override protected void start(){

		//	Set up `renderer` if needed
		if(renderer == null) renderer = actor.GetComponent<SpriteRenderer>();

		//	Set up `init` and reconfigure `rotation` to be the added rotation
		init = renderer.color;
		color -= init;

	}
	override protected void update(){
		renderer.color = (init + (color * completion));
	}

}
