using System.Collections.Generic;
using UnityEngine;

//	Class that runs a list of actions
public class ActionList : MonoBehaviour{

	[Header("Actions")]
	/*	Variables:
	actions: List of actions left to run
	timeScaled: If the action list utilizes scaled delta time
	started: If the first action in `actions` has started
	*/
	[SerializeReference, SubclassSelector] protected List<BaseAction> actions = new List<BaseAction>();
	[SerializeField] private bool timeScaled = true;
	private bool started = false;

	//	Validate
	private void OnValidate(){
		foreach(BaseAction action in actions) if(action != null) action.validate(gameObject);
	}

	//	Run `actions`
	private void Update(){

		//	Variable: Duration of the current frame
		float dt;

		//	Retrieve `remainingTime` based on time scaling
		if(timeScaled) dt = Time.deltaTime;
		else dt = Time.unscaledDeltaTime;

		//	Run
		BaseAction.runActions(actions, ref started, gameObject, ref dt);
	}

	//	Add to `actions`
	public void addActionDirect(BaseAction action){
		actions.Add(action);
	}
	public void addAction(BaseAction action){
		actions.Add(action.clone());
	}
	public void addActions<Action>(List<Action> actions) where Action : BaseAction{
		BaseAction.addClones(this.actions, actions);
	}

	//	Clear `actions`
	public void clearActions(){
		actions.Clear();
		started = false;
	}

}