using UnityEngine;
using NaughtyAttributes;

//	Action for moving the actor
[System.Serializable] public class AnimateAction : BaseInstantAction{

	[Header("Animation")]
	/*	Variables:
	animation: Index of animation to call
	*/
	[SerializeField, Dropdown("dropdown")] private int animation;

	/*	Variables:
	dropdown: Dropdown for setting animations by index
	*/
	[SerializeField, HideInInspector] private DropdownList<int> dropdown = new DropdownList<int>{{"INVALID", -2}};

	//	Constructor
	public AnimateAction(){
		animation = -1;
	}
	public AnimateAction(int animationIndex){
		animation = animationIndex;
	}
	public AnimateAction(AnimateAction origin){
		animation = origin.animation;
	}

	//	Overrides
	override public BaseAction clone(){
		return new AnimateAction(this);
	}
	override public void validate(GameObject actor){

		//	Variable: Animator or actor of `actor`
		SpriteAnimator animator = actor.GetComponent<SpriteAnimator>();

		//	Check `handler`
		if(animator == null){

			//	Set error call
			Debug.LogError("Animation behavior cannot find a proper animator");

			//	Set invalid dropdown
			dropdown = new DropdownList<int>{{"INVALID", -2}};

		}

		//	Set `dropdown`
		else dropdown = animator.getAnimationIndexDropdown();

	}
	override protected void update(){
		instance.actor.animate(animation);
	}

}
