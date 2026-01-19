using System.Collections.Generic;
using UnityEngine;

//	List of actions to run upon destruction
//	NOTE: Objects must be destroyed using the custom `Destroy()` method in order for this to run
[RequireComponent(typeof(ActionList))] public class ObjectDestroyer : MonoBehaviour{

	//	Types of destruction
	public enum Cause{DEFAULT, DEATH, HIDE, DESPAWN}

	//	Class that assigns a set of actions to run upon a certain type of destruction
	[System.Serializable] private class OnDestroyLayer{

		/*	Variables:
		type: Type of death to respond to
		actions: Set of actions to respond with
		*/
		public Cause type = Cause.DEATH;
		[SerializeReference, SubclassSelector] public List<BaseAction> actions;

	}

	//	Action that properly destroys a game object
	private class DestroyAction : BaseInstantAction{

		//	Overrides
		override public BaseAction clone(){
			return new ErrorAction("Runtime constructed actions should not be cloned!");
		}
		override protected void update(){
			GameObject.Destroy(actor);
		}

	}

	//	Variable: List of actions to run upon destruction
	[SerializeField] private List<OnDestroyLayer> onDestroyActions = new List<OnDestroyLayer>();

	//	Destruction
	static public void destroy(GameObject obj, Cause type = Cause.DEFAULT){

		//	Variable: Object destroyer to run
		ObjectDestroyer destroyer = obj.GetComponent<ObjectDestroyer>();

		//	If `destroyer` is invalid, destroy the object normally
		if(destroyer == null){
			GameObject.Destroy(obj);
			return;
		}

		//	Run `destroyer`
		destroyer.destroy(type);

	}

	//	Helpers
	private void destroy(Cause type){

		//	Variable: Action list found while destroying self
		ActionList actionList = destroyInner(type);

		//	Destroy children
		foreach(ObjectDestroyer child in GetComponentsInChildren<ObjectDestroyer>()) if(child != this){

			//	Variable: Action list found while destroying child
			ActionList childActionList = child.destroyInner(type);

			//	Add callback
			actionList.addOriginalAction(new WaitForCallbackAction(ref childActionList));

		}

		//	Properly destroy the game object
		actionList.addOriginalAction(new DestroyAction());

	}
	private ActionList destroyInner(Cause type){

		//	Check if there are any destroy layers
		if(onDestroyActions.Count < 1) return GetComponent<ActionList>();

		//	Variable: Index of the correct destroy layer
		int i = 0;

		//	Find correct destroy layer
		while(onDestroyActions[i].type != type){

			//	Increment
			i += 1;

			//	Check for end of `onDestroyActions`
			if(onDestroyActions.Count <= i){

				//	Check if type can be switched to `DEFAULT`
				if(type == Cause.DEFAULT) return GetComponent<ActionList>();
				return destroyInner(Cause.DEFAULT);

			}

		}

		//	Variable: Return value / action list found
		ActionList ans = GetComponent<ActionList>();

		//	Add `actions` to `ans`
		ans.clearActions();
		ans.addActions(onDestroyActions[i].actions);

		//	Return
		return ans;
	}

}