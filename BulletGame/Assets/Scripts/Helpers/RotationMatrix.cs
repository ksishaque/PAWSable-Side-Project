using UnityEngine;

//	Class used for rotating 2D vectors quickly
public class RotationMatrix{

	/*	Variables:
	IDENTITY: Identity rotation matrix
	CW_RIGHT: 90 degrees clockwise matrix
	CCW_RIGHT: 90 degrees counterclockwise matrix
	*/
	static readonly public RotationMatrix IDENTITY = new RotationMatrix(1, 0);
	static readonly public RotationMatrix CW_RIGHT = new RotationMatrix(0, -1);
	static readonly public RotationMatrix CCW_RIGHT = new RotationMatrix(0, 1);

	/*	Variables:
	cos: Cosine value of rotation
	sin: Sine value of rotation
	*/
	private float cos, sin;


	//	Constructor
	public RotationMatrix(float angle = 0, bool degrees = true){

		//	Convert `angle` to radians
		if(degrees) angle *= Mathf.Deg2Rad;

		//	Set members
		cos = Mathf.Cos(angle);
		sin = Mathf.Sin(angle);

	}
	public RotationMatrix(Quaternion rotation){

		//	Set members
		cos = Mathf.Cos(rotation.eulerAngles.z * Mathf.Deg2Rad);
		sin = Mathf.Sin(rotation.eulerAngles.z * Mathf.Deg2Rad);

	}
	public RotationMatrix(Transform transform){

		//	Set members
		cos = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
		sin = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);

	}
	private RotationMatrix(float cos, float sin){
		this.cos = cos;
		this.sin = sin;
	}

	//	Rotate
	public Vector2 rotate(Vector2 vector){
		return new Vector2((cos * vector.x) - (sin * vector.y), (sin * vector.x) + (cos * vector.y));
	}
	public Vector2 inverseRotate(Vector2 vector){
		return new Vector2((cos * vector.x) + (sin * vector.y), (sin * vector.x) - (cos * vector.y));
	}
	static public Vector2 operator*(Vector2 vector, RotationMatrix matrix){
		return matrix.rotate(vector);
	}
	static public Vector2 operator/(Vector2 vector, RotationMatrix matrix){
		return matrix.inverseRotate(vector);
	}

}