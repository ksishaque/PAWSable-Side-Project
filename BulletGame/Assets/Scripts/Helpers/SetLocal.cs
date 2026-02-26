using UnityEngine;

static public partial class Physics{

	//	Set position
	static public void setLocalPosition(this Transform trans, float x, float y){
		trans.localPosition = new Vector3(x, y, trans.localPosition.z);
	}
	static public void setLocalPosition(this Transform trans, Vector2 position){
		trans.setLocalPosition(position.x, position.y);
	}
	static public void setLocalPosition(this GameObject obj, float x, float y){
		obj.transform.setLocalPosition(x, y);
	}
	static public void setLocalPosition(this GameObject obj, Vector2 position){
		obj.transform.setLocalPosition(position);
	}

	//	Add position
	static public void addLocalPosition(this Transform trans, float x, float y){
		trans.localPosition += new Vector3(x, y, 0);
	}
	static public void addLocalPosition(this Transform trans, Vector2 position){
		trans.addLocalPosition(position.x, position.y);
	}
	static public void addLocalPosition(this GameObject obj, float x, float y){
		obj.transform.addLocalPosition(x, y);
	}
	static public void addLocalPosition(this GameObject obj, Vector2 position){
		obj.transform.addLocalPosition(position);
	}

	//	Set rotation
	static public void setLocalRotation(this Transform trans, float rotation){
		trans.localRotation = Quaternion.Euler(0, 0, rotation);
	}
	static public void setLocalRotation(this GameObject obj, float rotation){
		obj.transform.setLocalRotation(rotation);
	}

	//	Add rotation
	static public void addLocalRotation(this Transform trans, float rotation){
		trans.localRotation *= Quaternion.Euler(0, 0, rotation);
	}
	static public void addLocalRotation(this GameObject obj, float rotation){
		obj.transform.addLocalRotation(rotation);
	}

	//	Set scale
	static public void setLocalScale(this Transform trans, float x, float y){
		trans.localScale = new Vector3(x, y, 1);
	}
	static public void setLocalScale(this Transform trans, Vector2 scale){
		trans.setLocalScale(scale.x, scale.y);
	}
	static public void setLocalScale(this GameObject obj, float x, float y){
		obj.transform.setLocalScale(x, y);
	}
	static public void setLocalScale(this GameObject obj, Vector2 scale){
		obj.transform.setLocalScale(scale);
	}

	//	Add scale
	static public void addLocalScale(this Transform trans, float x, float y){
		trans.localScale = new Vector3(trans.localScale.x * x, trans.localScale.y * y, 1);
	}
	static public void addLocalScale(this Transform trans, Vector2 scale){
		trans.addLocalScale(scale.x, scale.y);
	}
	static public void addLocalScale(this Transform trans, float scale){
		trans.addLocalScale(scale, scale);
	}
	static public void addLocalScale(this GameObject obj, float x, float y){
		obj.transform.addLocalScale(x, y);
	}
	static public void addLocalScale(this GameObject obj, Vector2 scale){
		obj.transform.addLocalScale(scale);
	}
	static public void addLocalScale(this GameObject obj, float scale){
		obj.transform.addLocalScale(scale);
	}

}