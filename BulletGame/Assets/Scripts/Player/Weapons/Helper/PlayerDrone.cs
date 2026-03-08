using UnityEngine;

//	Component that manages player drone movement
[RequireComponent(typeof(ActionList))] public class PlayerDrone : MonoBehaviour, IInitialize{

	/*	Variables:
	caller: Current component calling this drone
	actionList: Action list to add transition actions to
	indicator: Sprite renderer to recolor to indicate drone state
	inactiveColor: Color of `indicator` when inactive
	duration: Duration to move from one caller to the next
	hasBeenCalled: If the drone has been called before
	*/
	private PlayerDroneCaller caller = null;
	private ActionList actionList;
	[SerializeField] private SpriteRenderer indicator;
	[SerializeField] private Color inactiveColor = new Color(0, 0, 0, 0.5f);
	[SerializeField, NaughtyAttributes.MinValue(0)] private float repositionDuration = 0.25f;
	private bool hasBeenCalled = false;

	//	Set up
	public void onInitialize(){
		actionList = GetComponent<ActionList>();
	}

	//	Call the drone to a caller
	public void call(PlayerDroneCaller caller){
Debug.Log("Called");

		//	Transition
		transitionTo(caller.transform, caller.getIndicatorColor());

		//	Set `caller` and `hasBeenCalled`
		this.caller = caller;
		hasBeenCalled = true;

	}

	//	Release the drone from a caller
	public void release(PlayerDroneCaller caller){
Debug.Log("Released");

		//	Check if `caller` still has ownership
		if(caller == this.caller){

			//	Reset `caller`
			this.caller = null;

			//	Transition
			transitionTo(GlobalReferences.instance.player.transform, inactiveColor, true);
			actionList.addActionDirect(new DestroyAction(ObjectDestroyer.Cause.STOW_WEAPON));

		}

	}

	//	Helpers
	static public PlayerDrone findOrCreateDrone(int index){

		//	Variable: Return value / drone at `index`
		PlayerDrone ans;

		//	Check if the key exists
		if(GlobalReferences.instance.playerDrones.TryGetValue(index, out ans)){

			//	Check if the drone itself exists
			if(ans == null){

				//	Initialize and add a new drone
				ans = createDrone();
				GlobalReferences.instance.playerDrones[index] = ans;

			}

			//	Return
			return ans;

		}

		//	Initialize and add a new drone
		ans = createDrone();
		GlobalReferences.instance.playerDrones.Add(index, ans);

		//	Return
		return ans;

	}
	static private PlayerDrone createDrone() =>  ObjectInitializer.instantiate(GlobalReferences.instance.playerDronePrefab, GlobalReferences.instance.player.transform).GetComponent<PlayerDrone>();
	private void transitionTo(Transform parent, Color indicatorColor, bool dissappear = false){

		//	Reparent transform
		transform.SetParent(parent);

		/*	Variables:
		actions: Parallel action node to run each transition action in parallel
		compFunc: Completion function to use for each action
		*/
		ParallelAction actions = new ParallelAction();
		StandardScalingFunction compFunc = new StandardScalingFunction(hasBeenCalled, !dissappear);

		//	Set up transition actions
		actions.addSingleBranchDirect(new MoveAction(new Vector2(0, 0), false, repositionDuration, compFunc));
		actions.addSingleBranchDirect(new RotateAction(0, RotateAction.Direction.FASTEST, false, repositionDuration, compFunc));
		if(dissappear) actions.addSingleBranchDirect(new ScaleAction(new Vector2(), false, repositionDuration, compFunc));
		else actions.addSingleBranchDirect(new ScaleAction(new Vector2(1, 1), false, repositionDuration, compFunc));
		actions.addSingleBranchDirect(new RecolorAction(indicator, indicatorColor, repositionDuration, new StandardScalingFunction()));

		//	Add `actions` to `actionList`
		actionList.clearActions();
		actionList.addActionDirect(actions);

	}

}