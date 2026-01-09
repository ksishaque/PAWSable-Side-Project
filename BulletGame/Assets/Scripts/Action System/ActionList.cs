using System.Collections.Generic;
using UnityEngine;

//	Class that runs a list of actions
public class ActionList : MonoBehaviour{

	/*	Variables:
	actions: List of actions left to run
	timeScaled: If the action list utilizes scaled delta time
	started: If the first action in `actions` has started
	*/
	[SerializeReference, SubclassSelector] private List<BaseAction> actions = new List<BaseAction>();
	[SerializeField] bool timeScaled = true;
	private bool started = false;

	//	Run `actions`
	private void Update(){

		//	Variable: Duration of the current frame
		float dt;

		//	Retrieve `remainingTime` based on time scaling
		if(timeScaled) dt = Time.deltaTime;
		else dt = Time.unscaledDeltaTime;

		//	Run
		BaseAction.runActions(ref actions, ref started, gameObject, ref dt);
	}

	//	Add to `actions`
	public void addAction(BaseAction action){
		actions.Add(action);
	}
	public void addActions(List<BaseAction> actions){
		this.actions.AddRange(actions);
	}
	public void addClone(BaseAction action){
		actions.Add(action.clone());
	}
	public void addClones(List<BaseAction> actions){
		BaseAction.addClones(ref this.actions, actions);
	}

	//	Clear `actions`
	public void clearActions(){
		actions.Clear();
		started = false;
	}

}