using UnityEngine;

//	Class that destroys an object
[System.Serializable] public class DestroyAction : BaseBehavior{

	/*	Variables
	target: Object to destroy (`actor` if `null`)
	type: Type of destruction to use
	*/
	[SerializeField] private GameObject target;
	[SerializeField] private ObjectDestroyer.Type type;

	//	Constructors
	public DestroyAction(){
		target = null;
		type = ObjectDestroyer.Type.DEFAULT;
	}
	public DestroyAction(ObjectDestroyer.Type type){
		target = null;
		this.type = type;
	}
	public DestroyAction(GameObject target, ObjectDestroyer.Type type = ObjectDestroyer.Type.DEFAULT){
		this.target = target;
		this.type = type;
	}

	//	Overrides
	override public BaseAction clone(){
		return new DestroyAction(target, type);
	}
	override public void update(ref float remainingTime){
		if(target == null) ObjectDestroyer.destroy(actor, type);
		else ObjectDestroyer.destroy(target, type);
	}

}