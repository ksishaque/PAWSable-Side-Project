using System.Collections.Generic;
using UnityEngine;

//	Interface for behaviors that need to run a function before the object begins to destroy itself
public interface IDestroy{
	abstract public void onDestroy();
}

//	List of actions to run upon destruction
//	NOTE: Objects must be destroyed using the custom `Destroy()` method in order for this to run
[RequireComponent(typeof(ActionList))] public class ObjectDestroyer : MonoBehaviour{

	//	Types of destruction
	public enum Cause{DEFAULT = 0, DEATH = 1 << 0, STOW_WEAPON = 1 << 1, DESPAWN = 1 << 2, PROJECTILE_EXPIRE = 1 << 3}

	//	Class that assigns a set of actions to run upon a certain type of destruction
	[System.Serializable] private class OnDestroyLayer{

		/*	Variables:
		type: Type of death to respond to
		actions: Set of actions to respond with
		*/
		[NaughtyAttributes.EnumFlags] public Cause type = Cause.DEFAULT;
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

	/*	Variables:
	onDestroyActions: List of actions to run upon destruction
	destroyed: If this object has already been destroyed, to avoid infinite looping
	*/
	[SerializeField] private List<OnDestroyLayer> onDestroyActions = new List<OnDestroyLayer>();
	private bool destroyed = false;

	//	Destruction
	static public void destroy(GameObject obj, Cause type = Cause.DEFAULT){

		//	Variable: Object destroyer to run
		ObjectDestroyer destroyer = obj.GetComponent<ObjectDestroyer>();

		//	If `destroyer` is invalid, destroy the object normally
		if(destroyer == null) GameObject.Destroy(obj);

		//	Run `destroyer`
		else destroyer.destroy(type);

	}

	//	Helpers
	private void destroy(Cause type){

		//	Check and set `destroyed`
		if(destroyed) return;
		destroyed = true;

		//	Variable: Action list found while destroying self
		ActionList actionList = destroyInner(type);

		//	Destroy children
		foreach(ObjectDestroyer child in GetComponentsInChildren<ObjectDestroyer>()) if(child != this){

			//	Variable: Action list found while destroying child
			ActionList childActionList = child.destroyInner(type);

			//	Add callback
			actionList.addActionDirect(new WaitForCallbackAction(ref childActionList));

		}

		//	Properly destroy the game object
		actionList.addActionDirect(new DestroyAction());

	}
	private ActionList destroyInner(Cause type){

		//	Check if there are any destroy layers
		if(onDestroyActions.Count < 1) return GetComponent<ActionList>();

		//	Variable: Index of the correct destroy layer
		int i = 0;

		//	Find correct destroy layer
		if(type == Cause.DEFAULT) while(Math.bitContains(onDestroyActions[i].type, type)){

			//	Increment
			i += 1;

			//	Check for end of `onDestroyActions`
			if(onDestroyActions.Count <= i)  destroyInner(Cause.DEFAULT);

		}
		else while(onDestroyActions[i].type != Cause.DEFAULT){

			//	Increment
			i += 1;

			//	Check for end of `onDestroyActions`
			if(onDestroyActions.Count <= i)  return GetComponent<ActionList>();

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