using UnityEngine;

//	Action specifically made for enemy pathing, with preview options
[System.Serializable] abstract public class BaseBehavior : BaseAction{

	//	Attempt to set the behavior as unending (i.e. it is the last behavior on a non-destructive list)
	virtual public bool setEndless(bool endless = true){
		return false;
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
				updatePos(getFinalPosition(duration, spentTime - remainingTime));

				//	Reset rotation and scale if necessary
				if(reset){
					actor.transform.localScale = new Vector3(origSca.x, origSca.y, actor.transform.localScale.z);
					Physics.setLocalRotation(actor, physics, origRot);
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
	abstract protected Vector2 getFinalPosition(float duration, float dt);

}