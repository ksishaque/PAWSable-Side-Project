using UnityEngine;

//	Action specifically made for enemy pathing, with preview options
[System.Serializable] abstract public class BaseBehavior : BaseAction{

	//	Attempt to set the behavior as unending (i.e. it is the last behavior on a non-destructive list)
	virtual public bool setEndless(bool endless = true){
		return false;
	}

	//	Validate
	virtual public bool forceEnd() => false;

	//	Draw preview
	abstract public void drawPreview(ref Vector2 position, ref float timeUntilImage, ref float timeUntilDurationImage, float imageRadius, bool endless = false);
	protected void drawImage(Vector2 position, float imageRadius){
		Gizmos.DrawSphere(position, imageRadius);
	}
	protected void drawDurationImage(Vector2 position, float imageRadius){
		Gizmos.DrawWireCube(position, new Vector3(imageRadius * 2, imageRadius * 2, 0));
	}

}

//	Action for timed enemy movement
[System.Serializable] abstract public class BaseMovementBehavior : BaseBehavior{

	/*	Variables:
	physics: Physics component of `actor`
	origin: Original position of `actor`
	origRot: Original rotation of `actor`
	origSca: Original scale of `actor`
	*/
	private Rigidbody2D physics;
	protected Vector2 origin{
		get;
		private set;
	}
	private float origRot;
	private Vector2 origSca;

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

	[Header("Facing")]
	/*	Variables:
	flip: If the object should flip across the y axis when moving backwards
	rotate: If the object should rotate to face forward
	*/
	[SerializeField] private bool flip;
	[SerializeField] private bool rotate;
	[SerializeField] private bool reset;

	//	Constructors
	protected BaseMovementBehavior(){
		duration = 1;
		endless = false;
		flip = true;
		rotate = false;
		reset = false;
	}
	protected BaseMovementBehavior(BaseMovementBehavior origin){
		duration = origin.duration;
		endless = origin.endless;
		flip = origin.flip;
		rotate = origin.rotate;
		reset = origin.reset;
	}

	//	Overrides
	sealed override protected void start(){
		physics = actor.GetComponent<Rigidbody2D>();
		origin = (Vector2) actor.transform.localPosition;
		origRot = actor.transform.localRotation.eulerAngles.z;
		origSca = (Vector2) actor.transform.localScale;
	}
	sealed override public void update(ref float remainingTime){

		//	Check `endless`
		if(endless){
			time += remainingTime;
			updatePos(getPosition(remainingTime));
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

				//	Update position
				if(runtimePrediction()) updatePos(predictPosition(origin, duration));
				else updatePos(getPosition(spentTime - remainingTime));

				//	Reset rotation and scale if necessary
				if(reset){
					if(flip) actor.transform.localScale = new Vector3(origSca.x, origSca.y, actor.transform.localScale.z);
					if(rotate) Physics.setLocalRotation(actor, physics, origRot);
				}

			}

			//	Update position
			else updatePos(getPosition(spentTime));

		}

	}
	sealed override public bool setEndless(bool endless){
		this.endless = endless;
		return true;
	}
	sealed override public void drawPreview(ref Vector2 position, ref float timeUntilImage, ref float timeUntilDurationImage, float imageRadius, bool endless){

		/*	Variables:
		totalTime: Total amount of time simulated since the start of the preview
		totalDuration: Total amount of time to simulate by the end
		newPos: Next position along the simulated path
		start: Position at which the simulated path starts
		*/
		float totalTime = AISpawnPatternEditor.getTimeStep(), totalDuration = duration;
		Vector2 newPos, start = position;

		//	Update `totalDuration` based on `endless`
		if(endless) totalDuration = AISpawnPatternEditor.getEndlessDuration();

		//	Draw the path
		while(totalTime < totalDuration){

			//	Find `newPos` and draw path
			newPos = predictPosition(start, totalTime);
			Gizmos.DrawLine(position, newPos);

			//	Increment `totalTime` and update `position`
			totalTime += AISpawnPatternEditor.getTimeStep();
			position = newPos;

		}

		//	Draw the final step and finalize `position`
		newPos = predictPosition(start, totalDuration);
		Gizmos.DrawLine(position, newPos);
		position = newPos;

		//	Draw the images, if necessary
		if(timeUntilImage < totalDuration && timeUntilImage >= 0) drawImage(predictPosition(start, timeUntilImage), imageRadius);
		if(timeUntilDurationImage < totalDuration && timeUntilDurationImage >= 0) drawDurationImage(predictPosition(start, timeUntilDurationImage), imageRadius);

		//	Finalize times
		timeUntilImage -= duration;
		timeUntilDurationImage -= duration;

	}

	//	Helper function for updating position
	private void updatePos(Vector2 position){

		//	Flip if necessary
		if(flip){
			if(position.x > actor.transform.localPosition.x){
				if(rotate) actor.transform.localScale = new Vector3(origSca.x, -origSca.y, actor.transform.localScale.z);
				else actor.transform.localScale = new Vector3(-origSca.x, origSca.y, actor.transform.localScale.z);
			}
			else if(position.x < actor.transform.localPosition.x) actor.transform.localScale = new Vector3(origSca.x, origSca.y, actor.transform.localScale.z);
		}

		//	Rotate if necessary
		if(rotate){

			//	Variable: Total displacement traveled
			Vector2 disp = position - (Vector2) actor.transform.localPosition;

			//	Check for standstill and rotate
			if(disp.x != 0 || disp.y != 0) Physics.setLocalRotation(actor, physics, (Mathf.Atan2(disp.y, disp.x) * Mathf.Rad2Deg) + 180);

		}

		//	Update position
		Physics.setLocalPosition(actor, physics, position);

	}

	//	Determine change in position (or velocity)
	virtual protected Vector2 getPosition(float dt){
		return getDelPos(dt) + (Vector2) actor.transform.position;
	}
	virtual protected Vector2 getDelPos(float dt){
		return getVelocity(dt) * dt;
	}
	virtual protected Vector2 getVelocity(float dt){
		return new Vector2(0, 0);
	}

	//	Predict position
	abstract protected Vector2 predictPosition(Vector2 start, float duration);
	virtual protected bool runtimePrediction() => true;

}