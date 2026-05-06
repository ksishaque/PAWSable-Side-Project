using System.Collections.Generic;
using UnityEngine;

//	Class that runs a list of actions
public class ActionList : MonoBehaviour{

	[Header("Actions")]
	/*	Variables:
	actions: List of actions left to run
	timeScaled: If the action list utilizes scaled delta time
	runner: Runner for the action list
	*/
	[SerializeReference, SubclassSelector] protected List<BaseAction> actions = new List<BaseAction>();
	[SerializeField] private bool timeScaled = true;
	private BaseAction.Runner runner;

	//	Validate
	private void OnValidate(){
		foreach(BaseAction action in actions) if(action != null) action.validate(gameObject);
	}

	//	Run `actions`
	private void Start(){
		runner = actions.start(gameObject);
	}
	private void Update(){

		//	Variable: Duration of the current frame
		float dt;

		//	Retrieve `remainingTime` based on time scaling
		if(timeScaled) dt = Time.deltaTime;
		else dt = Time.unscaledDeltaTime;

		//	Run
		runner.update(ref dt);

	}


	//	Accessor
	public BaseAction.Runner getInstance() => runner;

}