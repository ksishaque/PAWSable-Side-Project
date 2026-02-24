using UnityEngine;

//	Class that stores rotation for physics purposes
public class PhysicalRotation : MonoBehaviour, IStoreTransform{

	/*	Variables:
	rotation: Public accessor/mutator variable for `rotationInner`
	matrix: Public accessor for `matrixInner`
	rotationInner: Physical rotation
	matrixInner: Rotation matrix derived from rotation
	dirty: If `matrix` is out of date
	*/
	public float rotation{
		get => rotationInner;
		set{
			rotationInner = value;
			dirty = true;
		}
	}
	public RotationMatrix matrix{
		get{
			if(dirty) matrixInner = new RotationMatrix(rotationInner);
			return matrixInner;
		}
	}
	private float rotationInner;
	private RotationMatrix matrixInner;
	private bool dirty = true;

	//	Set up
	public void storeTransform(){
		rotation = transform.localRotation.eulerAngles.z;
	}

	//	Operands for easy access
	static public implicit operator float(PhysicalRotation rotation){
		if(rotation == null) return 0;
		return rotation.rotationInner;
	}
	
}
