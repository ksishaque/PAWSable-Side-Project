using System.Collections.Generic;
using UnityEngine;

//	Action for running multiple lists simultaneously
[System.Serializable] public class ParallelAction : BaseAction{

	//	Class for each action list branch
	[System.Serializable] private class Branch{

		/*	Variables:
		actions: List of actions left to run
		original: Original list of actions, saved for cloning
		runner: Runner of the action list
		*/
		[SerializeReference, SubclassSelector] private List<BaseAction> actions = new List<BaseAction>();
		private List<BaseAction> original = null;
		[SerializeField] private bool forceEnd;
		private Runner runner;

		//	Constructors
		public Branch(){
			forceEnd = false;
		}
		public Branch(BaseAction action, bool forceEnd){
			actions.Add(action);
			this.forceEnd = forceEnd;
		}
		public Branch(List<BaseAction> actions, bool forceEnd){
			this.actions.addClones(actions);
			this.forceEnd = forceEnd;
		}
		public Branch(Branch origin){

			//	Copy `actions`
			if(origin.original == null) actions.addClones(origin.actions);
			else actions.addClones(origin.original);

			//	Copy `forceEnd`
			forceEnd = origin.forceEnd;

		}

		//	Prepare the branch
		public void start(IActor actor){

			//	Save a copy of `actions` to `original`
			original = new List<BaseAction>();
			original.addClones(actions);

			//	Set up `runner`
			runner = new Runner(actor, actions);
		}

		//	Run the branch
		public void run(float dt, ref float remainingForced, ref float remaining){

			//	Run `actions`
			runner.update(ref dt);

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

	//	Mutator
	public void addBranch(List<BaseAction> actions, bool forceEnd = false){
		branches.Add(new Branch(actions, forceEnd));
	}
	public void addSingleBranchDirect(BaseAction action, bool forceEnd = false){
		branches.Add(new Branch(action, forceEnd));
	}

	//	Overrides
	override public BaseAction clone(){
		return new ParallelAction(this);
	}
	override protected void start(){
		foreach(Branch branch in branches) branch.start(instance.actor);
	}
	override protected void update(ref float dt){

		/*	Variables:
		remainingForced: Amount of time remaining after the fastest forced branch
		remaining: Amount of time remaining after the slowest non-forced branch
		*/
		float remainingForced = -1;
		float remaining = dt;

		//	Run each branch
		foreach(Branch branch in branches) branch.run(dt, ref remainingForced, ref remaining);

		//	Set `dt` to the fastest remaining time
		if(remainingForced > remaining) dt = remainingForced;
		else dt = remaining;

	}

}