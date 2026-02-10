using UnityEngine;

//	Class for managing AI facing behavior
public class AIFacer : MonoBehaviour, IStoreScale{


	//	Flags for different ways the object should be oriented to keep the graphics consistant in the direction it is trying to face
	[System.Flags] public enum Correction{NONE = 0, FLIP = 1 << 0, ROTATE = 1 << 1}


	/*	Variables:
	origSca: Original scale, stored for facing
	target: Target to look at
	correction: Correction to use after facing `target`
	*/
	private Vector2 origSca = Boundary.VECTOR_NULL;
	[SerializeReference, SubclassSelector] private BaseTarget target = null;
	[SerializeField] private AIFacer.Correction correction = AIFacer.Correction.FLIP;


	//	Face `target`
	private void Update(){
		if(target != null) face(target.getLocation());
	}


	//	Mutators
	public void setTarget(BaseTarget target){
		this.target = target;
	}
	public void setTarget(GameObject target){
		this.target = new BaseTarget.Object(target);
	}
	public void lockTarget(){
		target = new BaseTarget.Location(target);
	}


	//	Overrides
	public void storeScale(){
		origSca = transform.localScale;
	}


	//	Face a location
	public void faceMovement(Vector2 newLocation){
		if(target == null) face(newLocation);
	}
	private void face(Vector2 facedLocation){

		//*

		//	Convert `facedLocation` to root space
		if(transform.parent != null) facedLocation = transform.parent.InverseTransformPoint(facedLocation);

		//	Flip if necessary
		if((correction & Correction.FLIP) != Correction.NONE){
			if(facedLocation.x > transform.localPosition.x){
				if((correction & Correction.ROTATE) == Correction.NONE) gameObject.setScale(-origSca.x, origSca.y);
				else gameObject.setScale(origSca.x, -origSca.y);
			}
			else if(facedLocation.x < transform.localPosition.x) gameObject.setScale(origSca.x, origSca.y);
		}

		//	Rotate if necessary
		if((correction & Correction.ROTATE) != Correction.NONE){

			//	Variable: Total displacement traveled
			Vector2 disp = facedLocation - (Vector2) transform.position;

			//	Check for standstill and rotate
			if(disp.x != 0 || disp.y != 0) gameObject.setRotation((Mathf.Atan2(disp.y, disp.x) * Mathf.Rad2Deg) + 180);

		}
		/*/

		//	Convert `facedLocation` to local space
		facedLocation = transform.InverseTransformPoint(facedLocation);

		//	Rotate if necessary
		if((correction & Correction.ROTATE) != Correction.NONE && (facedLocation.x != 0 || facedLocation.y != 0)){
			gameObject.addRotation((Mathf.Atan2(facedLocation.y, facedLocation.x) * Mathf.Rad2Deg) + 180);

			//	Flip if necessary
			if((correction & Correction.FLIP) != Correction.NONE && transform.localRotation.eulerAngles.z < 90 && transform.localRotation.eulerAngles.z >= -90) gameObject.addScale(-1, 1);

		}

		//	Flip if necessary
		else if((correction & Correction.FLIP) != Correction.NONE && facedLocation.x > 0) gameObject.addScale(-1, 1);
		//*/

	}


}