using UnityEngine;

//	Action specifically made for enemy pathing, with preview options
[System.Serializable] abstract public class BaseBehavior : BaseAction{

	//	Attempt to set the behavior as unending (i.e. it is the last behavior on a non-destructive list)
	virtual public bool setEndless(bool endless = true){
		return false;
	}

}

//	Action specifically made for enemy pathing, with preview options
[System.Serializable] abstract public class BaseTimedBehavior : BaseBehavior{

	/*	Variables
	duration: Duration of the action
	time: Time since start of the action
	endless: If the behavior should be used indefinitely
	*/
	[SerializeField] private float duration;
	protected float time{
		get;
		private set;
	} = 0;
	protected bool endless;

	//	Constructors
	protected BaseTimedBehavior(){
		duration = 1;
		endless = false;
	}
	protected BaseTimedBehavior(BaseTimedBehavior origin){
		duration = origin.duration;
		endless = origin.endless;
	}

	//	Overrides
	sealed override public void update(ref float remainingTime){

		//	Check `endless`
		if(endless){
			time += remainingTime;
			updateEndless(remainingTime);
			remainingTime = -1;
		}
		else{

			//	Variables: Time spent on this action
			float spentTime = remainingTime;

			//	Update `time` and `remainingTime`
			time += remainingTime;
			remainingTime = time - duration;
			if(remainingTime > 0){
				time = duration;
				spentTime -= remainingTime;
			}

			//	Update
			update(spentTime);

			//	Check if the action needs to end
			if(remainingTime >= 0) exit();

		}

	}
	sealed override public bool setEndless(bool endless){
		this.endless = endless;
		return true;
	}

	//	Simplified version of `update()`, after `time` has been handled
	abstract protected void update(float dt);

	//	Update for unending version
	virtual protected void updateEndless(float dt){
		update(dt);
	}

	//	Final step of action
	virtual protected void exit(){}

}