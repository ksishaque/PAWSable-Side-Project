using UnityEngine;

//	Class for modular plans that the AI director can follow
[System.Serializable] abstract public partial class AIBasePlan{

	/*	Variables:
	recency: Recency score of the pattern, which decreases by 1 every time the pattern is not selected, and reset when it is
	*/
	private int recency = 0;

	//	Check and manage priority
	abstract protected PrioritySet getPriority();
	virtual public float getPriorityValue(){
		return getPriority().getPriorityValue(this);
	}
	public void updateRecency(){
		if(recency > 0) recency -= 1;
	}

	//	Activate the pattern
	//	NOTE: For spawning, this should set up the spawner. For bosses, this should directly set up the actions.
	abstract protected void use();
	public void select(){
		use();
		recency = MAX_RECENCY + 1;
	}

}