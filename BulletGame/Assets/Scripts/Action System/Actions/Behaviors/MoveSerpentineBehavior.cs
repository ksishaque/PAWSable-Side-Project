using UnityEngine;

//	Behavior in which the actor moves in a sinusoidal wave
[System.Serializable] public class MoveSerpentineBehavior : BaseMovementBehavior{

	[Header("Movement")]
	/*	Variables: 
	forwardVelocity: Direction and velocity of "forward"
	amplitude: Total perpendicular distance traveled from the center of the wave
	perpVec: Vector denoting `amplitude` as magnitude and the perpendicular direction from `forwardVelocity` as the direction
	waveLength: Distance traveled forward for each period of the wave
	timeScale: Scale to multiply `time` by to get `wavelength`
	prevHeight: Height of the sinusoidal function reached last frame
	*/
	[SerializeReference, SubclassSelector] private InspectorVector2 forwardVelocity;
	[SerializeField] private float amplitude;
	private Vector2 perpVec;
	[SerializeField] private float wavelength;
	private float timeScale;
	private float prevHeight;

	//	Constructors
	public MoveSerpentineBehavior(){
		forwardVelocity = InspectorVector2.getDefault();
		amplitude = 1;
		wavelength = 1;
	}
	public MoveSerpentineBehavior(MoveSerpentineBehavior origin) : base(origin){
		forwardVelocity = origin.forwardVelocity;
		amplitude = origin.amplitude;
		wavelength = origin.wavelength;
	}

	//	Overrides
	override public BaseAction clone(){
		return new MoveSerpentineBehavior(this);
	}
	override protected void start(){

		//	Variable: Speed of `forwardVelocity`
		float speed = forwardVelocity.get().magnitude;

		//	Calculate `timeScale`
		timeScale = Mathf.PI;
		timeScale *= 2;
		timeScale *= speed;
		timeScale /= wavelength;

		//	Calculate `perpVec`
		perpVec = forwardVelocity.get() * RotationMatrix.CW_RIGHT;
		perpVec /= speed;
		perpVec *= amplitude;

		//	Reset `prevHeight`, to be safe
		prevHeight = 0;

	}
	override protected Vector2 getDelPos(float dt){
		//return new Vector2(12 * dt, 0);

		//	Variable: Change in height of the sinusoidal function
		float dh = prevHeight;

		//	Calculate `dh` and the current sinusoidal height
		prevHeight = Mathf.Sin(time * timeScale);
		dh -= prevHeight;

		//	Calculate total vector and return
		return (dt * forwardVelocity.get()) + (perpVec * dh);

	}

}