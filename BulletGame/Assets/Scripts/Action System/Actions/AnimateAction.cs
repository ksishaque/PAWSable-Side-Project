using UnityEngine;

//	Action for moving the actor
[System.Serializable] public class AnimateAction : BaseInstantAction{

	[Header("Moving")]
	/*	Variables:
	animator: Animator to update
	animation: Index of animation to call
	*/
	[SerializeField] private SpriteAnimator animator;
	[SerializeField] private int animation;

	//	Constructor
	public AnimateAction(){
		animator = null;
		animation = -1;
	}
	public AnimateAction(SpriteAnimator animator, int animationIndex){
		this.animator = animator;
		animation = animationIndex;
	}
	public AnimateAction(AnimateAction origin){
		animator = origin.animator;
		animation = origin.animation;
	}

	//	Overrides
	override public BaseAction clone(){
		return new AnimateAction(this);
	}
	override protected void update(){
		if(animator == null) instance.actor.animate(animation);
		else animator.callAnimation(animation);
	}

}
