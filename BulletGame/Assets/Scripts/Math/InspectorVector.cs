using UnityEngine;

//	Base class for inspector friendly Vector2
[System.Serializable] abstract public class InspectorVector2{

	//	Standard Vector2 version
	[System.Serializable] public class Vector2D : InspectorVector2{

		//	Variable: Vector to access
		[SerializeField] private Vector2 vector = new Vector2();

		//	Override
		override public Vector2 get(){
			return vector;
		}

	}

	//	Directional angle and magnitude version
	[System.Serializable] public class DirectionMagnitude : InspectorVector2{

		//	Variable: Vector to access
		[SerializeField] private float direction = 0;
		[SerializeField] private float magnitude = 1;

		//	Override
		override public Vector2 get(){
			return new Vector2(Mathf.Cos(Mathf.Deg2Rad * direction) * magnitude, Mathf.Sin(Mathf.Deg2Rad * direction) * magnitude);
		}

	}

	//	Directional vector and magnitude version
	[System.Serializable] public class VectorMagnitude : InspectorVector2{

		//	Variable: Vector to access
		[SerializeField] private Vector2 direction = new Vector2();
		[SerializeField] private float magnitude = 1;

		//	Override
		override public Vector2 get(){
			return direction.normalized * magnitude;
		}

	}

	//	Access the actual vector
	abstract public Vector2 get();

	//	Get a default
	static public Vector2D getDefault() => new Vector2D();

}