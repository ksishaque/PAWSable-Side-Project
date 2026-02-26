using UnityEngine;

//	Class for managing AI facing behavior
public class AIFacer : MonoBehaviour, IInitialize{


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
	public void onInitialize(){
		origRot = transform.localRotation.eulerAngles.z;
		origSca = transform.localScale;
	}


	//	Face `target`
	private void Update(){
		if(target != null){
			if(transform.parent == null) face(target - (Vector2) transform.localPosition);
			else face((Vector2) transform.parent.InverseTransformPoint(target.getLocation()) - (Vector2) transform.localPosition);
		}
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
	public void faceMovement(Vector2 movement){
		if(target == null) face(movement);
	}
	private void face(Vector2 direction){

		//	Variable: Original rotation, possibly flipped, to use
		float rot = origRot;

		//	Flip if necessary
		if(Math.bitContains(correction, Correction.FLIP)){
			if(direction.x > 0){

				//	Update `rot`
				rot *= -1;

				//	Flip
				if(Math.bitContains(correction, Correction.ROTATE)) transform.setLocalScale(origSca.x, -origSca.y);
				else transform.setLocalScale(-origSca.x, origSca.y);

			}
			else if(direction.x < 0) transform.setLocalScale(origSca.x, origSca.y);
		}

		//	Rotate if necessary
		if(Math.bitContains(correction, Correction.ROTATE)){
			if(direction.x != 0 || direction.y != 0) transform.setLocalRotation((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 180 + rot);
		}
		else transform.setLocalRotation(rot);

	}


}