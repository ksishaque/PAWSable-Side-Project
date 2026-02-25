using System.Collections.Generic;
using UnityEngine;

//	Basic multi-purpose action node
[System.Serializable] abstract public class BaseAction{

	/*	Variables:
	actor: Game object executing the action
	list: List from which the action is being executed
	*/
	protected GameObject actor{
		get;
		private set;
	}
	protected List<BaseAction> list{
		get;
		private set;
	}

	//	Cloning
	abstract public BaseAction clone();

	//	Initialize members, then start
	public void initialize(GameObject actor, List<BaseAction> list){

		//	Set members
		this.actor = actor;
		this.list = list;

		//	Run `start()`
		start();
	}

	//	Initial and update steps of action
	virtual protected void start(){}
	virtual public void update(ref float remainingTime){}

	//	Helper functions for managing a list of actions
	static public void runActions(ref List<BaseAction> actions, ref bool started, GameObject actor, ref float remainingTime){

		//	Check `actions`
		if(actions.Count < 1) started = false;
		else{

			//	Check if the list has started
			if(started == false){

				//	Start the list
				actions[0].initialize(actor, actions);
				started = true;

			}

			//	Update and continue to next
			while(remainingTime >= 0){

				//	Update
				actions[0].update(ref remainingTime);

				//	If there is time left, remove the current node
				if(remainingTime >= 0){
					actions.RemoveAt(0);

					//	If possible, start the next action
					if(actions.Count > 0) actions[0].initialize(actor, actions);

					//	Forcefully pause the action list
					else{
						started = false;
						break;
					}

				}

			}

		}

	}
	static public void addClones<Action>(ref List<BaseAction> actions, List<Action> additives) where Action : BaseAction{
		foreach(Action additive in additives) actions.Add(additive.clone());
	}

}

//	Basic action node that runs in an instant
[System.Serializable] abstract public class BaseInstantAction : BaseAction{

	//	Overrides
	sealed override protected void start(){}
	sealed override public void update(ref float remainingTime){
		update();
	}

	//	Simplified version of `update()`, after `remainingTime` has been handled
	abstract protected void update();

}

//	Basic action node that runs in an instant
[System.Serializable] abstract public class BaseTimedAction : BaseAction{

	[Header("Timer")]
	/*	Variables:
	Duration: Inspector-editable initial duration
	duration: Duration of the action
	time: Time since start of the action
	function: Function to scale `completion` by
	completion: Completion rate of the action
	dCompletion: Amount of completion rate covered in the last frame
	pCompletion: Completion rate from the last frame
	*/
	[SerializeField] private float Duration;
	protected float duration => Duration;
	private float time = 0;
	[SerializeReference, SubclassSelector] private BaseScalingFunction completionFunction = new StandardScalingFunction();
	protected float completion => completionFunction.operate(time / Duration);
	protected float dCompletion{
		get;
		private set;
	} = 0;
	protected float pCompletion{
		get;
		private set;
	} = 0;

	//	Constructors
	protected BaseTimedAction(){
		Duration = 1;
	}
	protected BaseTimedAction(BaseTimedAction origin){
		Duration = origin.Duration;
	}

	//	Overrides
	sealed override public void update(ref float remainingTime){

		//	Store `pCompletion`
		pCompletion = completion;

		//	Update `time` and `remainingTime`
		time += remainingTime;
		remainingTime = time - Duration;
		if(remainingTime > 0) time = Duration;

		//	Determine `dCompletion`
		dCompletion = completion - pCompletion;

		//	Update
		update();

		//	Check if the action needs to end
		if(remainingTime >= 0) exit();

	}

	//	Simplified version of `update()`, after `remainingTime` has been handled
	abstract protected void update();

	//	Final step of action
	virtual protected void exit(){}

}

//	Action node to signify an error
[System.Serializable] public class ErrorAction : BaseInstantAction{

	//	Variable: Error message to log
	[SerializeField] private string message;

	//	Constructors
	public ErrorAction(){
		message = "";
	}
	public ErrorAction(string message){
		this.message = message;
	}

	//	Overrides
	override public BaseAction clone(){
		return new ErrorAction(message);
	}
	override protected void update(){
		Debug.LogError(message);
	}

}