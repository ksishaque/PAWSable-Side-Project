using System.Collections.Generic;
using UnityEngine;

//	Action for triggering a set of actions for a remote actor
[System.Serializable] public class RemoteAction : BaseInstantAction{

	/*	Variables:
	target: Action list to add `actions` to 
	actions: List of actions to add
	waitForCompletion: If this action should wait until the remote set of actions is completed
	interruptTarget: If the actions added should interrupt `target`
	*/
	[SerializeField] private ActionList target;
	[SerializeReference, SubclassSelector] private List<BaseAction> actions;
	[SerializeField] private bool waitForCompletion;
	[SerializeField] private bool interruptTarget;

	//	Constructor
	public RemoteAction(){
		target = null;
		actions = new List<BaseAction>();
		waitForCompletion = false;
		interruptTarget = false;
	}
	public RemoteAction(RemoteAction origin){
		target = origin.target;
		actions = origin.actions;
		waitForCompletion = origin.waitForCompletion;
		interruptTarget = origin.interruptTarget;
	}

	//	Overrides
	override public BaseAction clone(){
		return new RemoteAction(this);
	}
	override protected void update(){

		//	Add base actions
		if(interruptTarget) target.clearActions();
		target.addClones(actions);

		//	Add remote callback, if necessary
		if(waitForCompletion) list.Insert(1, new WaitForCallbackAction(ref target));

	}

}