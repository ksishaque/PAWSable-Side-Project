using UnityEngine;

//	Interface for locatable objects — whether in preview or in game
public interface ITransformable{

	//	Interface wrapper class for Unity `GameObject`s
	public class GameObject : ITransformable{

		//	Variable: Game Object to wrap
		private UnityEngine.GameObject obj;

		//	Constructor
		public GameObject(UnityEngine.GameObject obj) => this.obj = obj;

		//	Accessors
		public Vector2 getPosition(){
			return obj.transform.position;
		}
		public float getRotation(){
			return obj.transform.rotation.eulerAngles.z;
		}

	}

	//	Accessors
	public Vector2 getPosition();
	public float getRotation();

}