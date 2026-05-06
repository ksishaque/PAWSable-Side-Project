using System.Collections.Generic;
using UnityEngine;

//	Action for running multiple lists simultaneously
[System.Serializable] public class LoopAction : BaseInstantAction{

	/*	Variables:
	actions: List of actions to loop
	loops: Number of times to loop this list, with negative values representing infinity
	*/
	[SerializeReference, SubclassSelector] private List<BaseAction> actions = new List<BaseAction>();
	[SerializeField] private int loops;

	//	Constructor
	public LoopAction(){
		loops = 3;
	}
	public LoopAction(List<BaseAction> actions, int loops){

		//	Copy members
		this.actions.addClones(actions);
		this.loops = loops;

	}
	public LoopAction(LoopAction origin){

		//	Copy members
		actions.addClones(origin.actions);
		loops = origin.loops;

	}

	//	Overrides
	override public BaseAction clone(){
		return new LoopAction(this);
	}
	override protected void update(){

		//	Check `loops`
		if(loops == 0) return;

		//	Add `actions`
		instance.addActions(actions);

		//	Add next loop action
		instance.addActionDirect(new LoopAction(actions, loops - 1));

	}

}