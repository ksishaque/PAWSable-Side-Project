using UnityEngine;

//	Action for waiting a period of time and doing nothing
[System.Serializable] public class WaitAction : BaseBehavior{

	/*	Variables:
	duration: Duration of the wait action
	time: Time since start of the action
	*/
	[SerializeField] private float duration;
	private float time = 0;

	//	Constructor
	public WaitAction(){
		duration = 1;
	}
	public WaitAction(float duration){
		this.duration = duration;
	}

	//	Overrides
	override public BaseAction clone(){
		return new WaitAction(duration);
	}
	override public void update(ref float remainingTime){
		time += remainingTime;
		remainingTime = time - duration;
	}

}