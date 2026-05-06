using System.Collections.Generic;
using UnityEngine;

//	Action that waits for an action list to reach a certain point
public class WaitForCallbackAction : BaseAction{

	//	Class that calls back to the wait action
	private class CallbackAction : BaseInstantAction{

		//	Variable: Remote action to call back
		WaitForCallbackAction callback;

		//	Constructor
		public CallbackAction(WaitForCallbackAction callback){
			this.callback = callback;
		}

		//	Overrides
		override public BaseAction clone(){
			return new ErrorAction("Callback actions should not be cloned!");
		}
		override protected void update(){
			callback.waiting = false;
		}

	}

	//	Variable: If the action is still waiting
	private bool waiting = true;

	//	Constructors
	public WaitForCallbackAction(ref List<BaseAction> actions){
		actions.Add(new CallbackAction(this));
	}
	public WaitForCallbackAction(ref ActionList actions){
		actions.getInstance().addActionDirect(new CallbackAction(this));
	}
	public WaitForCallbackAction(out BaseAction callback){
		callback = new CallbackAction(this);
	}

	//	Overrides
	override public BaseAction clone(){
		return new ErrorAction("Callback actions should not be cloned!");
	}
	override protected void update(ref float remainingTime){
		if(waiting) remainingTime = -1;
	}

}
