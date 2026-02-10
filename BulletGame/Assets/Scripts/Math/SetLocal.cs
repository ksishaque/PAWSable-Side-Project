using UnityEngine;

static public partial class Physics{

	//	Set local position, rotation or scale
	static public void setPosition(this GameObject obj, Vector2 position){
		obj.transform.localPosition = new Vector3(position.x, position.y, obj.transform.localPosition.z);
	}
	static public void setPosition(this GameObject obj, float x, float y){
		obj.transform.localPosition = new Vector3(x, y, obj.transform.localPosition.z);
	}
	static public void setRotation(this GameObject obj, float rotation){
		obj.transform.localRotation = Quaternion.Euler(0, 0, rotation);
	}
	static public void setScale(this GameObject obj, Vector2 scale){
		obj.transform.localScale = new Vector3(scale.x, scale.y, 1);
	}
	static public void setScale(this GameObject obj, float x, float y){
		obj.transform.localScale = new Vector3(x, y, 1);
	}
	static public void addPosition(this GameObject obj, Vector2 position){
		obj.transform.localPosition += (Vector3) position;
	}
	static public void addPosition(this GameObject obj, float x, float y){
		obj.transform.localPosition += new Vector3(x, y, 0);
	}
	static public void addRotation(this GameObject obj, float rotation){
		obj.transform.localRotation *= Quaternion.Euler(0, 0, rotation);
	}
	static public void addScale(this GameObject obj, Vector2 scale){
		obj.transform.localScale = new Vector3(obj.transform.localScale.x * scale.x, obj.transform.localScale.y * scale.y, 1);
	}
	static public void addScale(this GameObject obj, float x, float y){
		obj.transform.localScale = new Vector3(obj.transform.localScale.x * x, obj.transform.localScale.y * y, 1);
	}
	static public void addScale(this GameObject obj, float scale){
		obj.transform.localScale *= scale;
	}

}