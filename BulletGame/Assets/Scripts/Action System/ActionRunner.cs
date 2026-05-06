using System.Collections.Generic;
using UnityEngine;

public partial class BaseAction{
	
	//	Class holding information about the running state of an action list
	public class Runner{

		/*	Variables:
		actor: Game object executing the action
		list: List from which the action is being executed
		started: If the current action has been initialized
		*/
		public IActor actor{
			get;
			private set;
		}
		private List<BaseAction> list;
		private bool started = false;


		//	Constructor
		public Runner(IActor actor, List<BaseAction> list){
			this.actor = actor;
			this.list = list;
		}


		//	Run `list`
		public void update(ref float remainingTime){

			//	Check `list`
			if(list.Count < 1) started = false;
			else{

				//	Check if the list has started
				if(started == false){

					//	Start the list
					list[0].initialize(this);
					started = true;

				}

				//	Update and continue to next
				while(remainingTime >= 0){

					//	Update
					list[0].update(ref remainingTime);

					//	If there is time left, remove the current node
					if(remainingTime >= 0){
						list.RemoveAt(0);

						//	If possible, start the next action
						if(list.Count > 0) list[0].initialize(this);

						//	Forcefully pause the action list
						else{
							started = false;
							break;
						}

					}

				}

			}

		}


		//	Mutators
		public void addAction(BaseAction action){
			list.Add(action.clone());
		}
		public void addActionNext(BaseAction action){
			list.Insert(1, action.clone());
		}
		public void addActionDirect(BaseAction action){
			list.Add(action);
		}
		public void addActionNextDirect(BaseAction action){
			list.Insert(1, action);
		}
		public void addActions<Action>(List<Action> actions) where Action : BaseAction{
			list.addClones(actions);
		}
		public void clearActions(){
			list.Clear();
			started = false;
		}

	}
	
}


//	Helpers
static public partial class Helpers{

	static public BaseAction.Runner start(this List<BaseAction> list, GameObject actor){

		//	Variable: Custom physics component of `actor`
		CustomPhysics physics = actor.GetComponent<CustomPhysics>();

		//	Return
		if(physics == null) return new BaseAction.Runner(new BaseAction.IActor.Nonphysical(actor), list);
		return new BaseAction.Runner(new BaseAction.IActor.Physical(physics), list);

	}
	static public void addClones<Action>(this List<BaseAction> actions, List<Action> additives) where Action : BaseAction{
		foreach(Action additive in additives) actions.Add(additive.clone());
	}

}