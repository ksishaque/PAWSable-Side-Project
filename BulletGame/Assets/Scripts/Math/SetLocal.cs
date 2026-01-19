using UnityEngine;

public partial class Physics{

	//	Set local position or rotation through various means depending on the presense of a physics component
	static public void setLocalPosition(GameObject obj, Rigidbody2D physics, Vector2 position){
		if(physics == null) obj.transform.localPosition = new Vector3(position.x, position.y, obj.transform.localPosition.z);
		else if(obj.transform.parent == null) physics.MovePosition(position);
		else physics.MovePosition(position + (Vector2) obj.transform.parent.position);
	}
	static public void setLocalRotation(GameObject obj, Rigidbody2D physics, float rotation){
		if(physics == null) obj.transform.localRotation = Quaternion.Euler(0, 0, rotation);
		else if(obj.transform.parent == null) physics.MoveRotation(Quaternion.Euler(0, 0, rotation));
		else physics.MoveRotation(Quaternion.Euler(0, 0, rotation + obj.transform.parent.rotation.eulerAngles.z));
	}

}