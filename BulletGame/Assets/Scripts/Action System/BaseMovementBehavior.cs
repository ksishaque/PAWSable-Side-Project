using UnityEngine;

//	Action for timed enemy movement
[System.Serializable] abstract public class BaseMovementBehavior : BaseBehavior{

	/*	Variables:
	origin: Original position of `actor`
	facer: Facer of `actor`
	rotation: Physical rotation affecting movements
	rotMatrix: Rotation matrix for `rotation`
	*/
	protected Vector2 origin{
		get;
		private set;
	}
	private AIFacer facer;
	protected PhysicalRotation rotation;
	protected RotationMatrix rotMatrix{
		get{
			if(rotation == null) return RotationMatrix.IDENTITY;
			return rotation.matrix;
		}
	}

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
	sealed override protected void start(){

		//	Set members
		origin = (Vector2) actor.transform.localPosition;
		facer = actor.GetComponent<AIFacer>();
		rotation = actor.GetComponent<PhysicalRotation>();

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

			}

			//	Update position
			else updatePos(getPosition(spentTime));

		}

	}
	sealed override public bool setEndless(bool endless = true){
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
		if(facer != null) facer.faceMovement(position);
		actor.setPosition(position);
	}

	//	Determine change in position (or velocity)
	virtual protected Vector2 getPosition(float dt){
		return (getDelPos(dt) * rotMatrix) + (Vector2) actor.transform.position;
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