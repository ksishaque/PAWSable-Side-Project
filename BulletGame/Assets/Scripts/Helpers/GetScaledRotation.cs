using UnityEngine;

static public partial class Physics{
	static public float getScaledRotation(this Transform trans){

		//	Variable: Unit direction vector
		Vector2 dir = (Vector2) trans.TransformVector(1, 0, 0);

		//	Calculate and return
		return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

	}

}