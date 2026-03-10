using UnityEngine;

//	Action for timed enemy movement
[System.Serializable] abstract public class BaseMovementBehavior : BaseBehavior{

	/*	Variables:
	facer: Facer of `actor`
	physics: Physics component of `actor`
	rotation: Physical rotation affecting movements
	rotMatrix: Rotation matrix for `rotation`
	*/
	private AIFacer facer;
	private Rigidbody2D physics;
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
		facer = actor.GetComponent<AIFacer>();
		physics = actor.GetComponent<Rigidbody2D>();
		rotation = actor.GetComponent<PhysicalRotation>();

		//	Calculate children members
		onStart();

	}
	sealed override public void update(ref float remainingTime){

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
				onEnd();
			}

			//	Update position
			else updatePos(spentTime);

		}

	}
	sealed override public bool setEndless(bool endless = true){
		this.endless = endless;
		return true;
	}
#if FALSE
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
#endif

	//	Helper function for updating position
	private void updatePos(float dt){

		//	Variable: Direction vector to send to `facer`
		Vector2 direction;

		//	Check if physical movement is not viable
		/*
		if(physics == null){
			direction = getDelPos(dt);
			actor.addLocalPosition(direction * rotMatrix);
		}
		else{
			direction = getVelocity(dt);
			//TODO: Probably make this look/feel better
			if(actor.transform.parent == null) physics.linearVelocity = direction * rotMatrix;
			else physics.linearVelocity = actor.transform.parent.TransformVector(direction * rotMatrix);
		}
		/*/
		direction = getDelPos(dt);
		actor.addLocalPosition(direction * rotMatrix);
		//*/

		//	Send facing data
		if(facer != null) facer.faceMovement(direction);

	}

	//	Functions for starting and stopping the movement
	virtual protected void onStart(){}
	virtual protected void onEnd(){}

	//	Determine position and velocity
	virtual protected Vector2 getDelPos(float dt){
		return getVelocity(dt) * dt;
	}
	virtual protected Vector2 getVelocity(float dt){
		return getDelPos(dt) / dt;
	}

}