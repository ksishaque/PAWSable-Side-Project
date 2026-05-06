using UnityEngine;

//	Action for timed enemy movement
[System.Serializable] abstract public class BaseMovementBehavior : BaseAction{

	//	Variable: Facer of `actor`
	private AIFacer facer;

	[Header("Timing")]
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
	protected BaseMovementBehavior(){
		duration = 1;
		endless = false;
	}
	protected BaseMovementBehavior(BaseMovementBehavior origin){
		duration = origin.duration;
		endless = origin.endless;
	}

	//	Overrides
	override protected void initialize(Runner instance){

		//	Set members
		facer = instance.actor.getComponent<AIFacer>();

		//	Base call
		base.initialize(instance);

	}
	sealed override protected void update(ref float remainingTime){

		//	Check `endless`
		if(endless){
			time += remainingTime;
			updatePos(remainingTime);
			remainingTime = -1;
		}
		else{

			//	Variables: Time spent on this action
			float spentTime = remainingTime;

			//	Update `time` and `remainingTime`
			time += remainingTime;
			remainingTime = time - duration;

			//	Check for ending
			if(remainingTime >= 0){
				updatePos(spentTime - remainingTime);
				exit();
			}

			//	Update position
			else updatePos(spentTime);

		}

	}
	sealed override public bool setEndless(bool endless = true){
		this.endless = endless;
		return true;
	}

	//	Helper function for updating position
	private void updatePos(float dt){

		//	Variable: Direction vector to send to `facer`
		Vector2 displacement = getDelPos(dt);

		//	Add `displacement`
		instance.actor.move(displacement);

		//	Send facing data
		if(facer != null) facer.faceMovement(displacement);

	}

	//	Functions for stopping the movement
	virtual protected void exit(){}

	//	Determine position and velocity
	virtual protected Vector2 getDelPos(float dt){
		return getVelocity(dt) * dt;
	}
	virtual protected Vector2 getVelocity(float dt){
		return getDelPos(dt) / dt;
	}

}