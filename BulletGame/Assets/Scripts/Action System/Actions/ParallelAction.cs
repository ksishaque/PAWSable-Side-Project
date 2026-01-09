using System.Collections.Generic;
using UnityEngine;

//	Action for running multiple lists simultaneously
[System.Serializable] public class ParallelAction : BaseAction{

	//	Class for each action list branch
	[System.Serializable] private class Branch{

		/*	Variables:
		actions: List of actions left to run
		original: Original list of actions, saved for cloning
		started: If the first action in `actions` has started
		*/
		[SerializeReference, SubclassSelector] private List<BaseAction> actions = new List<BaseAction>();
		private List<BaseAction> original = null;
		[SerializeField] private bool forceEnd;
		private bool started = false;

		//	Constructors
		public Branch(){
			forceEnd = false;
		}
		public Branch(Branch origin){

			//	Copy `actions`
			if(origin.original == null) addClones(ref actions, origin.actions);
			else addClones(ref actions, origin.original);

			//	Copy `forceEnd`
			forceEnd = origin.forceEnd;

		}

		//	Save a copy of `actions` to `original`
		public void save(){
			original = new List<BaseAction>();
			addClones(ref original, actions);
		}

		//	Run the branch
		public void run(GameObject actor, float dt, ref float remainingForced, ref float remaining){

			//	Run `actions`
			runActions(ref actions, ref started, actor, ref dt);

			//	Check if `actions` has ended
			if(dt < 0) remaining = -1;

			//	Modify `remainingForced` and `remaining` depending on `forceEnd`
			else{
				if(forceEnd){
					if(remainingForced < dt) remainingForced = dt;
				}
				else if(remaining > dt) remaining = dt;
			}

		}

	}

	//	Variable: Branches to run in parallel
	[SerializeField] private List<Branch> branches = new List<Branch>();

	//	Constructor
	public ParallelAction(){}
	public ParallelAction(ParallelAction origin){
		foreach(Branch branch in origin.branches) branches.Add(new Branch(branch));
	}

	//	Overrides
	override public BaseAction clone(){
		return new ParallelAction(this);
	}
	override protected void start(){
		foreach(Branch branch in branches) branch.save();
	}
	override public void update(ref float dt){

		/*	Variables:
		remainingForced: Amount of time remaining after the fastest forced branch
		remaining: Amount of time remaining after the slowest non-forced branch
		*/
		float remainingForced = -1;
		float remaining = dt;

		//	Run each branch
		foreach(Branch branch in branches) branch.run(actor, dt, ref remainingForced, ref remaining);

		//	Set `dt` to the fastest remaining time
		if(remainingForced > remaining) dt = remainingForced;
		else dt = remaining;

	}

}