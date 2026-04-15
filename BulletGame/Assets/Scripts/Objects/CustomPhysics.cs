using UnityEngine;

//	Class that stores rotation for physics purposes
[RequireComponent(typeof(Rigidbody2D))] public class CustomPhysics : MonoBehaviour, IInitialize{

	//	Variable: Physics component to wrap
	Rigidbody2D physics = null;

	/*	Variables:
	position: Intended physical position
	posDirty: If the true position needs to be adjusted to meet `position`
	*/
	public Vector2 position{
		get;
		private set;
	}
	private bool posDirty = false;

	/*	Variables:
	rotation: Public accessor/mutator variable for `rot`
	rotationMatrix: Public accessor for `rotMx`
	rot: Physical rotation
	rotMx: Rotation matrix derived from rotation
	rotDirty: If `rotMx` is out of date
	*/
	public float rotation{
		get => rot;
		set{
			rot = value;
			rotDirty = true;
		}
	}
	public RotationMatrix rotationMatrix{
		get{

			//	Calculate `rotMx`
			if(rotDirty){
				rotMx = new RotationMatrix(rot);
				rotDirty = false;
			}

			//	Return
			return rotMx;

		}
	}
	[NaughtyAttributes.ShowNonSerializedField] private float rot;
	private RotationMatrix rotMx;
	private bool rotDirty = true;

	//	Set up
	public void Start(){
		physics = GetComponent<Rigidbody2D>();
	}
	public void onInitialize(){
		Start();
		position = transform.localPosition;
		rotation = transform.localRotation.eulerAngles.z;
	}

	//	Update `physics`
	private void Update(){

		//	Update position
		if(posDirty){

			//	Set to world position, if needed
			if(transform.parent == null) physics.MovePosition(position);
			else physics.MovePosition(transform.parent.TransformPoint(position));

			//	Clear `posDirty`
			posDirty = false;

			//Debug.Log("Physics Movement");

		}

	}

	//	Accessors
	static public implicit operator float(CustomPhysics rotation){
		if(rotation == null) return 0;
		return rotation.rot;
	}

	//	Mutators
	public void teleport(Vector2 position){

		//	Set `position`
		this.position = position;
		posDirty = false;

		//	Correct `physics`
		physics.position = position;

	}
	public void moveToPosition(Vector2 position){
		this.position = position;
		posDirty = true;
	}
	public void moveToWorldPosition(Vector2 position){

		//	Set to local position, if needed
		if(transform.parent == null) this.position = position;
		else this.position = transform.parent.InverseTransformPoint(position);

		//	Set `posDirty`
		posDirty = true;

	}
	public void move(Vector2 displacement){
		position += displacement * rotationMatrix;
		posDirty = true;
	}
	
}
