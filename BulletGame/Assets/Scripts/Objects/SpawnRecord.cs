using UnityEngine;

public class SpawnRecord : MonoBehaviour, IStoreTransform{

	/*	Variables:
	position: Original local position of the object
	rotation: Original local rotation of the object
	scale: Original local scale of the object
	*/
	[HideInInspector] public Vector2 position{
		get;
		private set;
	}
	[HideInInspector] public float rotation{
		get;
		private set;
	}
	[HideInInspector] public Vector2 scale{
		get;
		private set;
	}


	//	Set up
	public void storeTransform(){
		position = transform.localPosition;
		rotation = transform.localRotation.eulerAngles.z;
		scale = transform.localScale;
	}
}