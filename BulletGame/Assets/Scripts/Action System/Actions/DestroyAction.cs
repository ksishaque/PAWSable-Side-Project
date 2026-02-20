using UnityEngine;

//	Class that destroys an object
[System.Serializable] public class DestroyAction : BaseBehavior{

	/*	Variables
	target: Object to destroy (`actor` if `null`)
	type: Type of destruction to use
	*/
	[SerializeField] private GameObject target;
	[SerializeField] private ObjectDestroyer.Cause cause;

	//	Constructors
	public DestroyAction(){
		target = null;
		cause = ObjectDestroyer.Cause.DEFAULT;
	}
	public DestroyAction(ObjectDestroyer.Cause cause){
		target = null;
		this.cause = cause;
	}
	public DestroyAction(GameObject target, ObjectDestroyer.Cause cause = ObjectDestroyer.Cause.DEFAULT){
		this.target = target;
		this.cause = cause;
	}

	//	Overrides
	override public BaseAction clone(){
		return new DestroyAction(target, cause);
	}
	override public void update(ref float remainingTime){
		if(target == null) ObjectDestroyer.destroy(actor, cause);
		else ObjectDestroyer.destroy(target, cause);
	}
	override public void drawPreview(ref Vector2 position, ref float timeUntilImage, ref float timeUntilDurationImage, float imageRadius, bool endless){}

}