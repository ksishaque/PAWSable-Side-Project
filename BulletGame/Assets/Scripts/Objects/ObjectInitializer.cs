using System.Collections.Generic;
using UnityEngine;

//	Interface for behaviors that need to store the original rotation of the object before initialization
public interface IStoreTransform{

	//	Store rotation
	abstract public void storeTransform();

}


//	State to set upon instantiation
//	NOTE: Objects must be instantiated using the custom `instantiate()` method in order for this to run
public class ObjectInitializer : MonoBehaviour{

	/*	Variables:
	positionOffset: Local position offset to instantiate at
	positionRootLayer: Number of parent layers to move up before resetting the local position
		NOTE: If set to -1, it does not reset the layer at all
	rotationOffset: Local rotation offset to instantiate at
	rotationRootLayer: Number of parent layers to move up before resetting the local rotation
		NOTE: If set to -1, it does not reset the layer at all
	scaleOffset: Local scale offset to instantiate at
	scaleReset: If local scale should be reset
	*/
	[SerializeField] private Vector2 positionOffset = new Vector2(0, 0);
	[SerializeField] private int positionRootLayer = -1;
	[SerializeField] private float rotationOffset = 0;
	[SerializeField] private int rotationRootLayer = -1;
	[SerializeField] private Vector2 scaleOffset = new Vector2(1, 1);
	[SerializeField] private bool scaleReset = false;

	//	Variable: If this object has already been initialized, to avoid repeats
	private bool initialized = false;

	//	Instantiate
	static public GameObject instantiate(GameObject prefab, Vector2 position, float rotation = 0){
		return initialize(GameObject.Instantiate(prefab, (Vector3) position, Quaternion.Euler(0, 0, rotation)));
	}
	static public GameObject instantiate(GameObject prefab, Transform parent){
		return initialize(GameObject.Instantiate(prefab, parent));
	}

	//	Initialize
	private void Start(){
		initialize(gameObject);
	}
	static private GameObject initialize(GameObject obj){

		//	Set up each transformation storer
		foreach(IStoreTransform storer in obj.GetComponentsInChildren<IStoreTransform>()) storer.storeTransform();

		//	Set up each initializer
		foreach(ObjectInitializer initializer in obj.GetComponentsInChildren<ObjectInitializer>()) initializer.initialize();

		//	Return
		return obj;

	}

	//	Helpers
	private void initialize(){

		//	Check and set `initialized`
		if(initialized) return;
		initialized = true;

		//	Variable: Parent layer to reset to
		Transform resetLayer = getParentByLayer(transform, positionRootLayer);

		//	Set up position
		if(resetLayer == null) transform.position = new Vector3(0, 0, 0);
		else transform.position = resetLayer.position;
		gameObject.addPosition(positionOffset);

		//	Set up rotation
		resetLayer = getParentByLayer(transform, rotationRootLayer);
		if(resetLayer == null) transform.rotation = Quaternion.identity;
		else transform.rotation = resetLayer.rotation;
		gameObject.addRotation(rotationOffset);

		//	Set up scale
		if(scaleReset) gameObject.setScale(scaleOffset);
		else gameObject.addScale(scaleOffset);

	}
	static private Transform getParentByLayer(Transform transform, int layer){
		if(layer < 0 || transform == null) return transform;
		return getParentByLayer(transform.parent, layer - 1);
	}

}