using UnityEngine;

//	Class used for rotating 2D vectors quickly
public class RotationMatrix{

	//	Variable: Identity rotation matrix
	static readonly public RotationMatrix IDENTITY = new RotationMatrix();

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

	//	Rotate
	public Vector2 rotate(Vector2 vector){
		return new Vector2((cos * vector.x) - (sin * vector.y), (sin * vector.x) + (cos * vector.y));
	}
	static public Vector2 operator*(Vector2 vector, RotationMatrix matrix){
		return matrix.rotate(vector);
	}

}