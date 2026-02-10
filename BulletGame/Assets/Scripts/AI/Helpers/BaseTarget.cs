using UnityEngine;

//	Interface for classes referencing a target
public interface BaseTarget{

	//	Class targeting a specific game object
	[System.Serializable] public class Object : BaseTarget{

		//	Variable: Object to target
		[SerializeField] private GameObject target;

		//	Constructors
		public Object(){
			target = null;
		}
		public Object(GameObject target){
			this.target = target;
		}

		//	Override
		public Vector2 getLocation(){
			return target.transform.position;
		}

	}

	//	Class targeting a specific world location
	[System.Serializable] public class Location : BaseTarget{

		//	Variable: Object to target
		[SerializeField] private Vector2 target;

		//	Constructors
		public Location(){
			target = new Vector2(0, 0);
		}
		public Location(Vector2 target){
			this.target = target;
		}
		public Location(Transform target){
			this.target = target.position;
		}
		public Location(GameObject target){
			this.target = target.transform.position;
		}
		public Location(BaseTarget target){
			this.target = target.getLocation();
		}

		//	Override
		public Vector2 getLocation(){
			return target;
		}

	}

	//	Class targeting a specific game object
	[System.Serializable] public class ObjectReference : BaseTarget{

		//	Variable: Object to target
		[SerializeReference, SubclassSelector] private GlobalReferences.SelectedReference target;

		//	Constructors
		public ObjectReference(){
			target = new GlobalReferences.Player();
		}
		public ObjectReference(GlobalReferences.SelectedReference target){
			this.target = target;
		}

		//	Override
		public Vector2 getLocation(){
			return target.getReference().transform.position;
		}

	}

	//	Access target
	public Vector2 getLocation();

}