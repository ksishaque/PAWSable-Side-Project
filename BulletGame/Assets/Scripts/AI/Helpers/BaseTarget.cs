using UnityEngine;

//	Interface for classes referencing a target
public abstract class BaseTarget{

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

		//	Override
		override public Vector2 getLocation(){
			return target;
		}

	}

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
		override public Vector2 getLocation(){
			return target.transform.position;
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
		override public Vector2 getLocation(){
			return target.getReference().transform.position;
		}

	}

	//	Class targeting a specific world location
	[System.Serializable] public class SpawnPoint : BaseTarget{

		//	Variable: Object to target
		[SerializeField] private SpawnRecord record;

		//	Constructors
		public SpawnPoint(){
			record = null;
		}
		public SpawnPoint(GameObject obj){
			record = obj.GetComponent<SpawnRecord>();
		}
		public SpawnPoint(SpawnRecord record){
			this.record = record;
		}

		//	Override
		override public Vector2 getLocation(){
			return record.position;
		}

	}

	//	Modifier class that adds a static offset to a target
	[System.Serializable] public class OffsetTarget : BaseTarget{

		//	Variable: Object to target
		[SerializeReference, SubclassSelector] private BaseTarget target;
		[SerializeReference, SubclassSelector] private InspectorVector2 offset;

		//	Constructors
		public OffsetTarget(){
			target = getDefault();
			offset = InspectorVector2.getDefault();
		}
		public OffsetTarget(BaseTarget target, InspectorVector2 offset){
			this.target = target;
			this.offset = offset;
		}
		public OffsetTarget(BaseTarget target, Vector2 offset){
			this.target = target;
			this.offset = new InspectorVector2.Vector2D(offset);
		}

		//	Override
		override public Vector2 getLocation(){
			return target.getLocation() + offset.get();
		}

	}

	//	Access target
	abstract public Vector2 getLocation();
	static public implicit operator Vector2(BaseTarget target) => target.getLocation();

	//	Default
	static public BaseTarget getDefault(){
		return new Location();
	}

}