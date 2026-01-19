using UnityEngine;

public partial class Physics{

	//	Determine the radius of an object based on its collider
	static public float getRadius(CircleCollider2D collider){

		//	Variable: Larger of the collider's scales
		float scale = collider.transform.lossyScale.x;

		//	Calculate and return
		if(scale < collider.transform.lossyScale.y) scale = collider.transform.lossyScale.y;
		return collider.radius * scale;

	}

}
