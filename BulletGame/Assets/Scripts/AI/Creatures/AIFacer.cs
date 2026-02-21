using UnityEngine;

//	Class for managing AI facing behavior
public class AIFacer : MonoBehaviour, IStoreScale, IStoreRotation{


	//	Flags for different ways the object should be oriented to keep the graphics consistant in the direction it is trying to face
	[System.Flags] public enum Correction{FLIP = 1 << 0, ROTATE = 1 << 1}


	/*	Variables:
	origSca: Original scale, stored for facing
	origSca: Original rotation, stored for facing
	target: Target to look at
	correction: Correction to use after facing `target`
	*/
	private Vector2 origSca = Boundary.VECTOR_NULL;
	private float origRot = 0;
	[SerializeReference, SubclassSelector] private BaseTarget target = null;
	[SerializeField] private Correction correction = Correction.FLIP;


	//	Set up
	public void storeScale(){
		origSca = transform.localScale;
	}
	public void storeRotation(){
		origRot = transform.localRotation.eulerAngles.z;
	}


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


	//	Face a location
	public void faceMovement(Vector2 newLocation){
		if(target == null) face(newLocation);
	}
	private void face(Vector2 facedLocation){

		//	Variable: Original rotation, possibly flipped, to use
		float rot = origRot;

		//	Convert `facedLocation` to root space
		if(transform.parent != null) facedLocation = transform.parent.InverseTransformPoint(facedLocation);

		//	Flip if necessary
		if(Math.bitContains(correction, Correction.FLIP)){
			if(facedLocation.x > transform.localPosition.x){

				//	Update `rot`
				rot *= -1;

				//	Flip
				if(Math.bitContains(correction, Correction.ROTATE)) gameObject.setScale(origSca.x, -origSca.y);
				else gameObject.setScale(-origSca.x, origSca.y);

			}
			else if(facedLocation.x < transform.localPosition.x) gameObject.setScale(origSca.x, origSca.y);
		}

		//	Rotate if necessary
		if(Math.bitContains(correction, Correction.ROTATE)){

			//	Variable: Total displacement traveled
			Vector2 disp = facedLocation - (Vector2) transform.localPosition;

			//	Check for standstill and rotate
			if(disp.x != 0 || disp.y != 0) gameObject.setRotation((Mathf.Atan2(disp.y, disp.x) * Mathf.Rad2Deg) + 180 + rot);

		}
		else gameObject.setRotation(rot);

	}


}