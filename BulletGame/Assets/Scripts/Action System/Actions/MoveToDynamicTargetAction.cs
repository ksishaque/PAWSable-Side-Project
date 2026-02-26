#define ENABLE_MOVE_TO_DYNAMIC_TARGET_ACTION

using UnityEngine;

//	NOTES: Works, but wouldn't recommend. Will speed up towards end if the target is moving, regardless of completion function. This is because of how movement for the dynamic initial position scales as the actor approaches the target. If you need it, probably rethink your design.
#if ENABLE_MOVE_TO_DYNAMIC_TARGET_ACTION
//	Action for moving the actor to a dynamic target
[System.Serializable] public class MoveToDynamicTargetAction : BaseTimedAction{

	[Header("Moving")]
	/*	Variables:
	target: Target to move to
	nC: `completion` from a previous frame, before it reached 1
	nP: Position recorded at the time `nCompletion` was last set
	*/
	[SerializeReference, SubclassSelector] private BaseTarget target;
	private float nC = 0;
	private Vector2 nP;

	//	Constructor
	public MoveToDynamicTargetAction(){
		target = BaseTarget.getDefault();
	}
	public MoveToDynamicTargetAction(MoveToDynamicTargetAction origin) : base(origin){
		target = origin.target;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveToDynamicTargetAction(this);
	}
	override protected void start(){
		nP = actor.transform.localPosition;
	}
	override protected void update(){

		/*	Variables:
		pC: Previous `completion` value to use
		pP: Previous position to use
		*/
		float pC = pCompletion;
		Vector2 pP;

		//	Determine `pC` and `pP`
		if(pC == 1){
			pC = nC;
			pP = nP;
		}

		//	Update `nC` and `nP`, if necessary, and finish finding `pP`
		else{
			nC = pC;
			nP = pP = actor.transform.localPosition;
		}

		//	Calculate and set the new position
		actor.setLocalPosition((target.getLocation() * completion) + (((pP - (target.getLocation() * pC)) * (1 - completion)) / (1 - pC)));

	}

}
#endif