using UnityEngine;

//	Base class for inspector friendly Vector2
[System.Serializable] abstract public class InspectorVector2{

	//	Standard Vector2 version
	[System.Serializable] public class Vector2D : InspectorVector2{

		//	Variable: Vector to access
		[SerializeField] private Vector2 vector;

		//	Constructors
		public Vector2D(){
			vector = new Vector2();
		}
		public Vector2D(Vector2 vector){
			this.vector = vector;
		}
		public Vector2D(float x, float y){
			vector = new Vector2(x, y);
		}

		//	Override
		override public Vector2 get(){
			return vector;
		}

	}

	//	Directional angle and magnitude version
	[System.Serializable] public class DirectionMagnitude : InspectorVector2{

		//	Variable: Vector to access
		[SerializeField] private float direction;
		[SerializeField] private float magnitude;

		//	Constructors
		public DirectionMagnitude(){
			direction = 0;
			magnitude = 1;
		}
		public DirectionMagnitude(float direction, float magnitude){
			this.direction = direction;
			this.magnitude = magnitude;
		}

		//	Override
		override public Vector2 get(){
			return new Vector2(Mathf.Cos(Mathf.Deg2Rad * direction) * magnitude, Mathf.Sin(Mathf.Deg2Rad * direction) * magnitude);
		}

	}

	//	Directional vector and magnitude version
	[System.Serializable] public class VectorMagnitude : InspectorVector2{

		//	Variable: Vector to access
		[SerializeField] private Vector2 direction;
		[SerializeField] private float magnitude;

		//	Constructors
		public VectorMagnitude(){
			direction = new Vector2();
			magnitude = 1;
		}
		public VectorMagnitude(Vector2 direction, float magnitude){
			this.direction = direction;
			this.magnitude = magnitude;
		}

		//	Override
		override public Vector2 get(){
			return direction.normalized * magnitude;
		}

	}

	//	Access the actual vector
	abstract public Vector2 get();
	static public implicit operator Vector2(InspectorVector2 vector) => vector.get();

	//	Get a default
	static public InspectorVector2 getDefault() => new Vector2D();

}