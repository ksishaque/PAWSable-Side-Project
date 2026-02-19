using System.Collections.Generic;
using UnityEngine;

//	Interface for behaviors that need to store the original local scale of the object before initialization
public interface IStoreScale{

	//	Store scale
	abstract public void storeScale();

}

//	Interface for behaviors that need to store the original local rotation of the object before initialization
public interface IStoreRotation{

	//	Store rotation
	abstract public void storeRotation();

}


//	State to set upon instantiation
//	NOTE: Objects must be instantiated using the custom `instantiate()` method in order for this to run
public class ObjectInitializer : MonoBehaviour{

	/*	Variables:
	position: Local position to instantiate at
		NOTE: `position` is alway additively relative, so there is no "positionAsOffset". ("positionAsOffset" is always `true`.)
	rotation: Local rotation to instantiate at
	rotationAsOffset: If `rotation` is additively relative to the original rotation
	scale: Local scale to instantiate at
	scaleAsOffset: If `scale` is multiplicitively relative to the original scale
	*/
	public Vector2 position = new Vector2(0, 0);
	public float rotation = 0;
	public bool rotationAsOffset = true;
	public Vector2 scale = new Vector2(1, 1);
	public bool scaleAsOffset = true;

	//	Set up
	static public GameObject instantiate(GameObject prefab, Vector2 position, float rotation = 0){

		//	Variable: Instantiated game object
		GameObject ans = GameObject.Instantiate(prefab, (Vector3) position, Quaternion.Euler(0, 0, rotation));

		//	Set up each initializer
		foreach(IStoreScale scaleStorer in ans.GetComponentsInChildren<IStoreScale>()) scaleStorer.storeScale();
		foreach(IStoreRotation rotStorer in ans.GetComponentsInChildren<IStoreRotation>()) rotStorer.storeRotation();
		foreach(ObjectInitializer initializer in ans.GetComponentsInChildren<ObjectInitializer>()) initializer.initialize();

		//	Return
		return ans;

	}
	static public GameObject instantiate(GameObject prefab, Transform parent){

		//	Variable: Instantiated game object
		GameObject ans = GameObject.Instantiate(prefab, parent);

		//	Set up each initializer
		foreach(ObjectInitializer initializer in ans.GetComponentsInChildren<ObjectInitializer>()) initializer.initialize();

		//	Return
		return ans;

	}

	//	Helper
	private void initialize(){

		//	Set up position
		gameObject.addPosition(position);

		//	Set up rotation
		if(rotationAsOffset) gameObject.addRotation(rotation);
		else gameObject.setRotation(rotation);

		//	Set up scale
		if(scaleAsOffset) gameObject.addScale(scale);
		else gameObject.setScale(scale);

	}

}