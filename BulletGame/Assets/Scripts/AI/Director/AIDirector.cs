using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour{

	//	Variable: Singleton instance
	static public AIDirector instance = null;

	[Header("Plans")]
	//	Variable: List of valid plans to consider
	[SerializeReference, SubclassSelector] private List<AIBasePlan> plans = new List<AIBasePlan>();

	[Header("Configurations")]
	/*	Variables:
	delay: Time waited before the next (or first) plan
	durationScale: Scale by which to affect the duration of each plan
	lockInterval: Interval at which to check when locked
	skipInterval: Interval at which to check when skipping (i.e. no plan has a priority above 0)
	maxRecency: Maximum recency value for plans
	locked: If the AI director is locked from acting
	*/
	[SerializeField] private float delay = 4;
	[SerializeField] private float durationScale = 1;
	[SerializeField] private float lockInterval = 0.5f;
	[SerializeField] private float skipInterval = 5;
	[SerializeField] private int maxRecency = 5;
	private bool locked = false;

	//	Manage `instance`
	private void Start(){
        if(instance != null) GameObject.Destroy(this);
		else instance = this;
    }
	private void OnDestroy(){
		if(instance == this) instance = null;
	}

	//	Run plans
	private void Update(){

		if(delay > 0) delay -= Time.deltaTime;
		else if(locked) delay = lockInterval;
		else{

			/*	Variables:
			i: Index of the most prioritized plan
			maxPriority: Priority value of the most prioritized plan
			*/
			int i = -1;
			float maxPriority = 0;

			//	Determine `i`
			for(int j = 0; j < plans.Count; j += 1){

				//	Variable: Priority of the currently observed plan (at index `j`)
				float curPriority = plans[j].getPriorityValue();

				//Debug.Log("Checked Priority of Plan " + j + ": " + curPriority + " (Max: " + maxPriority + ")");

				//	Update `i` and `maxPriority`, if necessary
				if(maxPriority < curPriority){
					i = j;
					maxPriority = curPriority;
				}

			}
			Debug.Log("Final Plan: " + i + " (" + maxPriority + ")");

			//	Select the plan
			if(i > -1) plans[i].select();
			else delay = skipInterval;

			//	Update recency values and `delay`
			foreach(AIBasePlan plan in plans) plan.updateRecency();

		}

	}

	//	Accessor
	public int getMaxRecency() => maxRecency;

	//	Mutator
	public void setLock(bool locked = true){
		this.locked = locked;
	}
	public void setDuration(float duration){
		delay = duration * durationScale;
	}

}
