using UnityEngine;

//	Behavior for objects that solely exist as folders for other objects, and thus will disappear when they have no more child objects
public class ObjectFolder : MonoBehaviour{
	private void Update(){
		if(transform.childCount < 1) ObjectDestroyer.destroy(gameObject);
	}
}